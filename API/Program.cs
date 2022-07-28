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
using Infrastructure.Services.Token;
using StackExchange.Redis;
using Core.Repositories.OrderRepository;
using Core.Repositories.UnitOfWork;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
TokenService.Configuration = builder.Configuration;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Auth Bearer Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
    c.AddSecurityRequirement(securityRequirement);
});
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
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var Configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(Configuration);
});



//builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = configuration["RedisCacheUrl"]; });

#endregion

#region Reposatories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
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

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtAuth";
    options.DefaultChallengeScheme = "JwtAuth";
})
 .AddJwtBearer("JwtAuth", options =>
 {
 

     options.TokenValidationParameters = new TokenValidationParameters
     {
         IssuerSigningKey = TokenService.GetKey(),
         ValidateIssuer = false,
         ValidateAudience = false
     }; 
 });

#endregion

#region Autorization

builder.Services.AddAuthorization(options =>

    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"))
    
);
#endregion



var app = builder.Build();

#region Middlewares
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
};

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run(); 
#endregion
