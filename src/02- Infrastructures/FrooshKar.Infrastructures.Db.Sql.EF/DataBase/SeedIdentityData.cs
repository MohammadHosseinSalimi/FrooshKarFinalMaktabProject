using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructures.Db.Sql.EF.DataBase
{
    public class SeedIdentityData
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public SeedIdentityData(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        public async Task Initialize(CancellationToken cancellationToken)
        {
            if (!_userManager.Users.Any())
            {
	            await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
	            await _roleManager.CreateAsync(new IdentityRole<int>("Customer"));
	            await _roleManager.CreateAsync(new IdentityRole<int>("Vendor"));

				var user = new AppUser()
                {
                    UserName = "m.h.salimi74@gmail.com",
                    Email = "m.h.salimi74@gmail.com",
                    PhoneNumber = "09128130911",
                    NormalizedUserName = "Administrator",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                await _userManager.CreateAsync(user, "13741381");
                await _userManager.AddToRoleAsync(user, "Admin");
            }



        }




    }
}
