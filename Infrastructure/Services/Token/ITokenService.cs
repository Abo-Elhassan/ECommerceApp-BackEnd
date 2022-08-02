using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.DTOs.Account;
namespace Infrastructure.Services.Token
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(Customer user);
    }
}
