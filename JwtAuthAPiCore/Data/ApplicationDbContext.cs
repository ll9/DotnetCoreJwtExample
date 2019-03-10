using System;
using System.Collections.Generic;
using System.Text;
using JwtAuthAPiCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAPiCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tenant>()
               .HasIndex(u => u.Code)
               .IsUnique();

            builder.Entity<Tenant>()
                .HasIndex(u => u.DomainName)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
