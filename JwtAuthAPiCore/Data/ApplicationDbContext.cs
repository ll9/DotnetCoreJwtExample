using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAPiCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MapData> MapData { get; set; }
        public DbSet<UpdateTracker> UpdateTracker { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
