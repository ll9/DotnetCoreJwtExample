using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtAuthAPiCore.Data;
using JwtAuthAPiCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using JwtAuthAPiCore.Utils;

namespace JwtAuthAPiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneTimePasswordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public OneTimePasswordsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _tokenGenerator = new TokenGenerator(_configuration);
        }

        // POST: api/OneTimePasswords
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public IActionResult GetOneTimePassword([FromBody] MobileUser mobileUser)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string password;

            do
            {
                password = OneTimePassword.GeneratePassword();

            } while (_context.OneTimePasswords.Any(p => p.Password.Equals(password)));

            var oneTimePassword = new OneTimePassword(password, mobileUser.Id);
            _context.OneTimePasswords.Add(oneTimePassword);
            _context.SaveChanges();

            return Ok(oneTimePassword.Password);
        }

        // POST: api/OneTimePasswords/consume
        [HttpPost("consume/{password}")]
        public async Task<IActionResult> GetOneTimePassword(string password)
        {
            var oneTimePassword = await _context.OneTimePasswords
                .Include(p => p.MobileUser)
                .ThenInclude(m => m.User)
                .SingleOrDefaultAsync(p => p.Password.Equals(password));

            if (oneTimePassword == null)
            {
                return BadRequest("One Time Password not found");
            }
            else if (DateTime.Now > oneTimePassword.Expires)
            {
                return BadRequest("Password expired");
            }
            else if (oneTimePassword.IsConsumed)
            {
                return BadRequest("Password already used");
            }

            var identityUser = oneTimePassword.MobileUser.User;
            var token = _tokenGenerator.GenerateJwtToken(identityUser.UserName, identityUser);

            var jwtToken = new JwtToken(token, oneTimePassword.MobileUser);
            oneTimePassword.IsConsumed = true;

            await _context.JwtTokens.AddAsync(jwtToken);
            await _context.SaveChangesAsync();

            return Ok(token);
        }

        private bool OneTimePasswordExists(string id)
        {
            return _context.OneTimePasswords.Any(e => e.Id == id);
        }
    }
}