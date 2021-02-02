using System.Collections.Generic;
using System.Linq;
using DataLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAccountAsync(User user);
        Task<SignInResult> PasswordSignInAsync(string email, string password);
        Task<IdentityResult> RegisterAccountAsync(User user, string password);
        Task<bool> CheckEmailExists(string email);
        Task<User> GetUserByEmail(string email);
        Task<IdentityResult> AddClaimAsync(User user, Claim claim);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<List<User>> GetAllAsync();
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpper());
        }

        public async Task<IdentityResult> CreateAccountAsync(User user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, true, false);
        }

        public async Task<IdentityResult> RegisterAccountAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> AddClaimAsync(User user, Claim claim)
        {
            return await _userManager.AddClaimAsync(user, claim);
        }
        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users.Include(u => u.UserVices)
                .ToListAsync();
        }
    }
}
