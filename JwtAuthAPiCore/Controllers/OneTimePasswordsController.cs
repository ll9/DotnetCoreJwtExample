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

namespace JwtAuthAPiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneTimePasswordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OneTimePasswordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OneTimePasswords
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

            var oneTimePassword = new OneTimePassword(password, mobileUser);
            _context.OneTimePasswords.Add(oneTimePassword);

            return Ok(oneTimePassword.Password);
        }

        private bool OneTimePasswordExists(string id)
        {
            return _context.OneTimePasswords.Any(e => e.Id == id);
        }
    }
}