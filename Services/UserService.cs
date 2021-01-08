using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public interface IUserService : IBaseService
    {
        Task<IdentityResult> RegisterAccountAsync(User user, string password);
        Task<(string email, string AccessToken)> LoginAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);
    }
    public class UserService : BaseService, IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly RoleManager<Role> _roleManager;
        public UserService(IUnitOfWork unitOfWork, ITokenService tokenService,
            RoleManager<Role> roleManager) : base(unitOfWork)
        {
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<(string email, string AccessToken)> LoginAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            var result = await UnitOfWork.Users.PasswordSignInAsync(email, password);
            if (result.Succeeded)
            {
                var token = await _tokenService.GenerateAccessToken(email);

                return (email, token);
            }

            return (null, null);
        }

        public async Task<IdentityResult> RegisterAccountAsync(User user, string password)
        {
            var result = await UnitOfWork.Users.RegisterAccountAsync(user, password);
            user.EmailConfirmed = true;
            if (result.Succeeded)
            {
                await UnitOfWork.Users.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                await UnitOfWork.Users.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.UserName));
                await UnitOfWork.Users.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.FirstName));
                await UnitOfWork.Users.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));
                await UnitOfWork.Users.AddClaimAsync(user, new Claim(ClaimTypes.SerialNumber, user.Id));
                var roleExists = await _roleManager.RoleExistsAsync(Role.User); // check role from db
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new Role { Name = Role.User });
                }
                await UnitOfWork.Users.AddToRoleAsync(user, Role.User);
            }
            await UnitOfWork.SaveChangesAsync();
            return result;
        }


        public async Task<bool> EmailExistsAsync(string email)
        {
            return await UnitOfWork.Users.CheckEmailExists(email);
        }

        private async Task<User> GetUserByEmailAsync(string email)
        {
            return await UnitOfWork.Users.GetUserByEmail(email);
        }
    }
}
