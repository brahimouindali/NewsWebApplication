using Data.Access.Library.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Access.Library
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            
            builder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            builder.Entity<Comment>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            builder.Entity<Publisher>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            builder.Entity<AppUser>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            builder.Entity<Rating>().Property(x => x.Id).HasDefaultValueSql("NewId()");
        }
    }
}
