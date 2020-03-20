using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class HrisDbContext : DbContext
    {
        public virtual DbSet<EmpLeaveWorkDateEntity> EmpLeaveWorkDateEntities { get; set; }

        public HrisDbContext()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=10.16.15.10;Initial Catalog=HRIS;User ID=WAMSU;Password=P@ss1234;");
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpLeaveWorkDateEntity>(entity =>
            {
                entity.HasKey(e => e.EmpID);
                //entity.HasNoKey();
            });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
