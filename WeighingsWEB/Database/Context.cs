using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
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

        /* public DbSet<DatabaseSchemaVersion> DatabaseSchemaVersion { get; set; } */
        /* public DbSet<OptimisticConcurrencyTokens> OptimisticConcurrencyTokens { get; set; } */
        /* public DbSet<RegisterValues> RegisterValues { get; set; } */
        /* public DbSet<VehicleDataValues> VehicleDataValues { get; set; } */
        /* public DbSet<WeighingLogValues> WeighingLogValues { get; set; } */
        /* public DbSet<WeighingReferences> WeighingReferences { get; set; } */


        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-20IU3S6\\SQLEXPRESS;Initial Catalog=weighings;Integrated Security=True");
        }
    }
}
