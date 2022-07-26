using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Context;
using Core.Repositories.CustomerRepository;
using Core.Repositories.ProductsRepository;
using Infrastructure.AutoMapperProfile;
using API.Errors;
using API.Middleware;
using Core.Repositories.BasketRepository;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


#region Default
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        List<string> errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToList();

        ApiValidiationErrorResponse errorResponse = new ApiValidiationErrorResponse
        {
            Errors = errors
        };
        return new BadRequestObjectResult(errorResponse);
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Cors 

//var allowAll = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });

});
#endregion


#region Database
var connectionString = builder.Configuration.GetConnectionString("StoreDb");
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString));
#endregion

#region Redis Connection
builder.Services.AddSingleton<IConnectionMultiplexer>(c => { 
    var Configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
   return ConnectionMultiplexer.Connect(Configuration);
});



#endregion

#region Reposatories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IBasketRepository, BasketRepository>();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtAuth";
    options.DefaultChallengeScheme = "JwtAuth";
})
 .AddJwtBearer("JwtAuth", options =>
 {
     var secretKey = builder.Configuration.GetValue<string>("SecretKey");
     var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
     var key = new SymmetricSecurityKey(keyInBytes);

     options.TokenValidationParameters = new TokenValidationParameters
     {
         IssuerSigningKey = key,
         ValidateIssuer = false,
         ValidateAudience = false
     };
 });

#endregion

#region ASP.Net Identity 

builder.Services.AddIdentity<Customer, IdentityRole<Guid>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
})
.AddEntityFrameworkStores<StoreContext>();
#endregion

var app = builder.Build();

#region Middlewares
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run(); 
#endregion
