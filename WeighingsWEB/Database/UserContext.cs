using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Database;

namespace Database
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UserContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            DatabaseConfiguration configuration = new DatabaseConfiguration("mssql-connection.cfg");
            optionsBuilder.UseSqlServer(configuration.BuildConnectionString());    
        }

    }
}
