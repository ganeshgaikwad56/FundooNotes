using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.FundooContext
{
    public class FundooContextDB : DbContext
    {
        public FundooContextDB(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }

        //region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
        //region Ending
    }
}
