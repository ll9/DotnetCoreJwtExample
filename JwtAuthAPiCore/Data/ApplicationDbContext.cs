using System;
using System.Collections.Generic;
using System.Text;
using JwtAuthAPiCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAPiCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MobileUser> MobileUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MobileUser>()
                .HasIndex(nameof(MobileUser.IdentityUserId), nameof(MobileUser.Name))
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
