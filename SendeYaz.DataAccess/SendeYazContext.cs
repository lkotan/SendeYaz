using Microsoft.EntityFrameworkCore;
using SendeYaz.DataAccess.Extensions;
using SendeYaz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.DataAccess
{
    public class SendeYazContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public SendeYazContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder.MapConfiguration();
            modelBuilder = modelBuilder.SetDataType();
            //modelBuilder = modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
