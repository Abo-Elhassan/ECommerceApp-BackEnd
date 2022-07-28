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
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Services.Token;

using API.Extensions;
using Infrastructure.DTOs.Order;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;

        public AccountController( UserManager<Customer> userManager, IMapper mapper)
        {
     
            _userManager = userManager;
            _mapper = mapper;
        }

       [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserReadDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<UserReadDto>(user);
        }

        
        [Authorize]
        [HttpGet]
        [Route("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {

            var user = await _userManager.FindUserByEmailIncludeAddress(HttpContext.User);
            return _mapper.Map<AddressDto>(user.Address); ;
        }


        [Authorize]
        [HttpPut]
        [Route("address")]
        public async Task<ActionResult<Address>> UpdateCurrentUserAddress(AddressDto address)
        {

            var user = await _userManager.FindUserByEmailIncludeAddress(HttpContext.User);
            user.Address = _mapper.Map<Address>(address);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(_mapper.Map<AddressDto>(user.Address));
            }
            else
            {
                return BadRequest();
            }
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
                new Claim (ClaimTypes.Email, newAdmin.Email),
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
                new Claim (ClaimTypes.Email, newUser.Email),
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

           
            var keyWithAlgorithm = new SigningCredentials(TokenService.GetKey(), SecurityAlgorithms.HmacSha256);


            var expDate = DateTime.Now.AddHours(1);
            var myJWT = new JwtSecurityToken(
                claims: claims,
                signingCredentials: keyWithAlgorithm,
                expires: expDate);

            var tokenHandler = new JwtSecurityTokenHandler();
            return Ok(
                new TokenDto
                {
                    
                    Email = loggingUser.Email,
                    Token = tokenHandler.WriteToken(myJWT),
                    ExpireDate = expDate
                });
           

        }
    }
}

