using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DataLayer.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DataLayer.Models
{
    public class Context : IdentityDbContext<User, Role, string>
    {
        public Context(DbContextOptions options) : base(options)
        {
        }


        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { });



        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Vice> Vices { get; set; }
        public virtual DbSet<UserVice> UserVices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            builder.Entity<Vice>().HasData(new Vice { Id = "1",Name = "Bautura" }, new Vice{Id = "2",Name = "Mancare"}, new Vice{Id = "3",Name = "Tigari"});
        }



    }
}