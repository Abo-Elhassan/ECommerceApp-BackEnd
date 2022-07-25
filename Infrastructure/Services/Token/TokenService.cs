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
    public class TokenService 
    {
        public static  IConfiguration Configuration;
       
        public readonly SymmetricSecurityKey Key;


        public static SymmetricSecurityKey GetKey()
        { 
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey")));
        }

        
       
    }
}
