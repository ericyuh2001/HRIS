using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HRIS_WAMS_WebCoreAPI.Models;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class HrisDbContext : DbContext
    {
        // 首頁待填報列表
        public virtual DbSet<AlterbyEmpIDEntity> AlterbyEmpIDEntitys { get; set; }

        // 抓取員工單日假單、加班單&判斷員工單日工時
        public virtual DbSet<EmpLeavebyWorkDateEntity> EmpLeavebyWorkDateEntitys { get; set; }

        

        // 抓取員工首頁資訊
        public virtual DbSet<HomeInfoByEmpEntity> HomeInfoByEmpEntitys { get; set; }

        
       
        // 員工萬年曆狀態列表
        public virtual DbSet<WorkingDateEntity> WorkingDateEntitys { get; set; }

        // 員工單日工時紀錄列表
        public virtual DbSet<WorkingDateAllDetailEntity> WorkingDateAllDetailEntitys { get; set; }

        // 員工單日可填報項目
        public virtual DbSet<WorkingHoursDetailEntity> WorkingHoursDetailEntitys { get; set; }

        public virtual DbSet<EmpLeavebyWorkDateEntity> EmpWorkingHoursDetailEntitys { get; set; }

        // 抓取待批表單列表
        public virtual DbSet<WaitApproveEntity> WaitApproveEntitys { get; set; }



        #region "Job"
        // =============================================================================
        // 員工單日可填報項目
        public virtual DbSet<WorkingDateJobCodebyEmpIDEndity> WorkingDateJobCodebyEmpIDEnditys { get; set; }

        #endregion





        public HrisDbContext()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <remarks>
        /// optionsBuilder.UseSqlServer(@"Data Source=T450S\MSSQLSERVER2012;Initial Catalog=HRIS;Persist Security Info=True;Integrated Security=True;");
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //optionsBuilder.UseSqlServer(@"Data Source=10.16.15.10;Initial Catalog=HRIS;User ID=WAMSU;Password=P@ss1234;");
            optionsBuilder.UseSqlServer(@"Data Source=10.160.35.172;Initial Catalog=HRM;User ID=HRIS_WHSU;Password=1qaz@WSX3edc");
            base.OnConfiguring(optionsBuilder);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 首頁待填報列表
            modelBuilder.Entity<AlterbyEmpIDEntity>(entity =>
            {
                entity.HasKey(e => e.EmpID);
            });

            // 抓取員工單日假單、加班單&判斷員工單日工時
            modelBuilder.Entity<EmpLeavebyWorkDateEntity>(entity =>
            {
                entity.HasKey(e => e.EmpID);
            });

            // 抓取員工首頁資訊
            modelBuilder.Entity<HomeInfoByEmpEntity>(entity =>
            {
                entity.HasKey(e => e.EmpID);
            });


           
            


            //modelBuilder.Entity<EmpWorkingHoursDetailEntity>(entity =>
            //{
            //    entity.HasKey(e => e.RowUnid);
            //});

            // 員工萬年曆狀態列表
            modelBuilder.Entity<WorkingDateEntity>(entity =>
            {
                entity.HasKey(e => e.EmpID);
            });


            // 員工單日工時紀錄列表
            modelBuilder.Entity<WorkingDateAllDetailEntity>(entity =>
            {
                entity.HasKey(e => e.RowUnid);
            });

            // 員工單日可填報項目
            modelBuilder.Entity<WorkingHoursDetailEntity>(entity =>
            {
                entity.HasKey(e => e.RowUnid);
            });

            // 抓取待批表單列表
            modelBuilder.Entity<WaitApproveEntity>(entity =>
            {
                entity.HasKey(e => e.EmpName);
            });



            // =============================================================
            // 員工單日可填報項目
            modelBuilder.Entity<WorkingDateJobCodebyEmpIDEndity>(entity =>
            {
                entity.HasKey(e => e.JobCode);
            });


            base.OnModelCreating(modelBuilder);
        }

        
    }
}
