using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Education.Domains.School.Entities;
namespace Education.Domains.School
{
    public class SchoolDBContext : DbContext
    {
        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }

        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistrationRequest>().ToTable("RegistrationRequest");
            modelBuilder.Entity<RegistrationRequest>().HasKey(p => p.Id);
            modelBuilder.Entity<RegistrationRequest>().Property(p => p.Id).UseSqlServerIdentityColumn();
            base.OnModelCreating(modelBuilder);
        }
    }
}
