using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtAuthAPiCore.Data;
using JwtAuthAPiCore.Models;

namespace JwtAuthAPiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MobileUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MobileUsers
        [HttpGet]
        public IEnumerable<MobileUser> GetMobileUsers()
        {
            return _context.MobileUsers;
        }

        // GET: api/MobileUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMobileUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mobileUser = await _context.MobileUsers.FindAsync(id);

            if (mobileUser == null)
            {
                return NotFound();
            }

            return Ok(mobileUser);
        }

        // PUT: api/MobileUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMobileUser([FromRoute] string id, [FromBody] MobileUser mobileUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mobileUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(mobileUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobileUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MobileUsers
        [HttpPost]
        public async Task<IActionResult> PostMobileUser([FromBody] MobileUser mobileUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MobileUsers.Add(mobileUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMobileUser", new { id = mobileUser.Id }, mobileUser);
        }

        // DELETE: api/MobileUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMobileUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mobileUser = await _context.MobileUsers.FindAsync(id);
            if (mobileUser == null)
            {
                return NotFound();
            }

            _context.MobileUsers.Remove(mobileUser);
            await _context.SaveChangesAsync();

            return Ok(mobileUser);
        }

        private bool MobileUserExists(string id)
        {
            return _context.MobileUsers.Any(e => e.Id == id);
        }
    }
}