using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace API.Extentions
{
    public static class UserManagerExtentions
    {
    //    public static async Task<Customer> FindUserEmailAddress(this UserManager<Customer> input, ClaimsPrincipal user)
    //    {

    //        var email = user?.FindFirstValue(ClaimTypes.Email);
    //        return await input.Users.Include(a => a.Address).SingleOrDefaultAsync(e => e.Email == email);
    //    }
    //}
}
