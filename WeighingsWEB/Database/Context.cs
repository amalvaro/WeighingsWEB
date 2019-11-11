﻿using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Database
{
    class Context : DbContext
    {
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<RegisterRecords> RegisterRecords { get; set; }
        public DbSet<UserRolePermissions> UserRolePermissions { get; set; }
        public DbSet<UserRoleReferences> UserRoleReferences { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<VehicleDataRecords> VehicleDataRecords { get; set; }
        public DbSet<VehiclePlateStencils> VehiclePlateStencils { get; set; }
        public DbSet<WeighingConditions> WeighingConditions { get; set; }
        public DbSet<WeighingImages> WeighingImages { get; set; }
        public DbSet<WeighingLog> WeighingLog { get; set; }


        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   

            optionsBuilder.UseSqlServer(
                File.ReadAllText("mssql-connection.cfg"));
                
        }
    }
}
