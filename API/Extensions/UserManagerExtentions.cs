using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace API.Extensions
{
    public static class UserManagerExtentions
    {
        public static async Task<Customer> FindUserByEmailIncludeAddress(this UserManager<Customer> userManager, ClaimsPrincipal user)
        {

            var email = user?.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(a => a.Address).SingleOrDefaultAsync(e => e.Email == email);
        }
    }

}
