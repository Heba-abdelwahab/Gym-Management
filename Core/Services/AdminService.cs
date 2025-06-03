using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public record AmindRegisterDto(string UserName , string Password , string Role);
    record UserRegisterDto(string UserName, string Password, string Role);

    public class AdminService
    {
        private readonly AuthenticationService authenticationService;

        public AdminService(IUnitOfWork unitOfWork,AuthenticationService authenticationService )
        {
            UnitOfWork = unitOfWork;
            this.authenticationService = authenticationService;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async void CreateAdmin(AmindRegisterDto dto)
        {
            //validation by usermanager like unique username
            AppUser appUser = new AppUser()
            {
                UserName = dto.UserName,

            };
            bool isSuccess = await authenticationService.Register(appUser,dto.Password,Roles.Admin);
            if (isSuccess) {
                Admin admin = new Admin()
                {
                    AppUser = appUser,
                    Hamada = "Ddd",
                    AppUserId = appUser.Id  
                };
                UnitOfWork.GetRepositories<Admin, int>().Insert(admin);
            }

        }
    }

    public class AuthenticationService
    {
        private readonly UserManager<AppUser> userManager;

        public AuthenticationService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Register(AppUser appUser , string password,string role)
        {
            var res= await userManager.CreateAsync(appUser , password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(appUser, role);
                return true;
            }
            else
            {
                string errors = string.Join(", ",res.Errors.Select(e=>e.Description));
                return false;
            }
        }

    }
}
