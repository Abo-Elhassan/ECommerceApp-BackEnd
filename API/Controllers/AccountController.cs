using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Infrastructure.DTOs.Account;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;

        public AccountController(IConfiguration configuration, UserManager<Customer> userManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("admin/register")]
        public async Task<ActionResult> RegisterAdmin(RegisterDto registerDTO)
        {

       
            var newAdmin = _mapper.Map<Customer>(registerDTO);
           

            var result = await _userManager.CreateAsync(newAdmin, registerDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddClaimsAsync(newAdmin, new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, newAdmin.Id.ToString()),
                new Claim (ClaimTypes.Name, newAdmin.UserName),
                new Claim (ClaimTypes.Role, "Admin"),

            });

            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpPost]
        [Route("user/register")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDTO)
        {


            var newUser = _mapper.Map<Customer>(registerDTO);


            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddClaimsAsync(newUser, new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, newUser.Id.ToString()),
                new Claim (ClaimTypes.Name, newUser.UserName),
                new Claim (ClaimTypes.Role, "User"),

            });

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto credentials)
        {
            var loggingUser = await _userManager.FindByEmailAsync(credentials.Email);

            if (loggingUser is null)
            {
                return NotFound();
            }

            var isAuthorized = await _userManager.CheckPasswordAsync(loggingUser, credentials.Password);

            if (!isAuthorized)
            {
                return Unauthorized();
            }

            var claims = await _userManager.GetClaimsAsync(loggingUser);

            var secretKey = _configuration.GetValue<string>("SecretKey");
            var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyInBytes);
            var keyWithAlgorithm = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var expDate = DateTime.Now.AddHours(1);
            var myJWT = new JwtSecurityToken(
                claims: claims,
                signingCredentials: keyWithAlgorithm,
                expires: expDate);

            var tokenHandler = new JwtSecurityTokenHandler();
            return Ok(
                new TokenDto
                {
                    Token = tokenHandler.WriteToken(myJWT),
                    Exp = expDate
                });
        }
    }
}
