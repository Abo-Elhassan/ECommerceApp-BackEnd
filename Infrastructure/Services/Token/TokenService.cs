using Core.Entities;
using Infrastructure.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        public static  IConfiguration Configuration;
       
        public readonly SymmetricSecurityKey Key;
        private readonly UserManager<Customer> _userManager;

        public TokenService(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }
        public static SymmetricSecurityKey GetKey()
        { 
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey")));
        }

        public async Task<TokenDto> CreateToken(Customer user)
        {
            var claims =  await _userManager.GetClaimsAsync(user);


            var keyWithAlgorithm = new SigningCredentials(TokenService.GetKey(), SecurityAlgorithms.HmacSha256);


            var expDate = DateTime.Now.AddHours(1);
            var myJWT = new JwtSecurityToken(
                claims: claims,
                signingCredentials: keyWithAlgorithm,
                expires: expDate);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = new TokenDto
                        {

                            Value = tokenHandler.WriteToken(myJWT),
                            ExpireDate = expDate
                        };

            return token;
        }



    }
}
