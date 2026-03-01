using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WorkTrack.DTO;
using WorkTrack.Models;
using System.Security.Claims;

namespace WorkTrack.Services
{
    public class AuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(SignInManager<User> signInManager,
                            UserManager<User> userManager,
                            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<(bool Success, string? Error)> LoginAsync(LoginDto dto)
        {
            // check if user exists
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return (false, "Invalid email or password");

            var result = await _signInManager.PasswordSignInAsync(
                user,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false
                );

            if (!result.Succeeded)
                return (false, "Invalid email or password");

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(SignUpDto dto)
        {
            // check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                return (false, "Email already exists");

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));

            return (true, null);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}