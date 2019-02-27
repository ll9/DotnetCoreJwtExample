using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthAPiCore.Data;
using JwtAuthAPiCore.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthAPiCore.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _Context;
        private readonly TokenGenerator _tokenGenerator;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext _context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _Context = _context;
            _tokenGenerator = new TokenGenerator(_configuration);
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return _tokenGenerator.GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            _Context.SaveChanges();

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return _tokenGenerator.GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}