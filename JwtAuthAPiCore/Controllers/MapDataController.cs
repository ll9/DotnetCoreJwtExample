﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtAuthAPiCore.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JwtAuthAPiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MapDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MapData
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<MapData> GetMapData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _context.MapData.Where(u => u.UserId == userId);
        }

        // GET: api/MapData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMapData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapData = await _context.MapData.FindAsync(id);

            if (mapData == null)
            {
                return NotFound();
            }

            return Ok(mapData);
        }

        // PUT: api/MapData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapData([FromRoute] int id, [FromBody] MapData mapData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mapData.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapDataExists(id))
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

        // POST: api/MapData
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostMapData([FromBody] MapData mapData)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mapData.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MapData.Add(mapData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapData", new { id = mapData.Id }, mapData);
        }

        // DELETE: api/MapData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapData = await _context.MapData.FindAsync(id);
            if (mapData == null)
            {
                return NotFound();
            }

            _context.MapData.Remove(mapData);
            await _context.SaveChangesAsync();

            return Ok(mapData);
        }

        private bool MapDataExists(int id)
        {
            return _context.MapData.Any(e => e.Id == id);
        }
    }
}