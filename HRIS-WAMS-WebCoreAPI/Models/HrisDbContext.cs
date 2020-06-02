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

        #region "SP usage for Duty"
        // 首頁待填報列表
        public virtual DbSet<AlterbyEmpIDEntity> AlterbyEmpIDEntitys { get; set; }

        // 抓取員工單日假單、加班單&判斷員工單日工時
        public virtual DbSet<EmpLeavebyWorkDateEntity> EmpLeavebyWorkDateEntitys { get; set; }

        // 員工單日填報統計資訊
        public virtual DbSet<EmpWorkdateEntity> EmpWorkdateEntitys { get; set; }


        // 輸入工時明細：回傳訊息
        public virtual DbSet<InsertWorkingHoursDetailReturnEntity> InsertWorkingHoursDetailReturnEntitys { get; set; }



        // 抓取員工首頁資訊
        public virtual DbSet<HomeInfoByEmpEntity> HomeInfoByEmpEntitys { get; set; }



        // 員工萬年曆狀態列表
        public virtual DbSet<WorkingDateEntity> WorkingDateEntitys { get; set; }

        // 員工週間每日狀態顯示
        public virtual DbSet<EmpWorkingDateStatusList> EmpWorkingDateStatusLists { get; set; }

        // 員工單日工時紀錄列表
        public virtual DbSet<WorkingDateDetailListEntity> WorkingDateAllDetailEntitys { get; set; }

        // 員工單筆工時明細
        public virtual DbSet<WorkingHoursDetailEntity> WorkingHoursDetailEntitys { get; set; }

        public virtual DbSet<EmpLeavebyWorkDateEntity> EmpWorkingHoursDetailEntitys { get; set; }

        // 更新填報工時：回傳訊息
        public virtual DbSet<UpdateWorkingHoursDetailReturnEntity> UpdateWorkingHoursDetailReturnEntitys { get; set; }



        // 抓取待批表單列表
        public virtual DbSet<WaitApproveEntity> WaitApproveEntitys { get; set; }

        #endregion



        #region "SP usage for Job"
        // =============================================================================
        // 員工單日可填報項目
        public virtual DbSet<WorkingDateJobCodebyEmpIDEntity> WorkingDateJobCodebyEmpIDEntitys { get; set; }

        #endregion



        #region "SP usage for Sign"
        //批核工時-待批月曆及單日批核總計狀態
        public virtual DbSet<WaitApproveDayStaticEntity> WaitApproveDayStaticEntitys { get; set; }

        // 主管週間工時待批列表
        public virtual DbSet<WaitApproveDetailEntity> WaitApproveDetailEntitys { get; set; }
        #endregion






        #region "Table"
        public virtual DbSet<WorkingHoursEntity> TB_WorkingHours { get; set; }

        public virtual DbSet<ProcessStatusEntity> TB_ProcessStatusEntitys { get; set; }

        public virtual DbSet<ProcessStatusDetailEntity> TB_ProcessStatusDetailEntitys { get; set; }

        public virtual DbSet<EmployeeEntity> TB_Employees { get; set; }
        #endregion



        #region "View"
        public virtual DbSet<vwWorkingHoursDetailEntity> VW_vwWorkingHoursDetailEntitys { get; set; }

        public virtual DbSet<vwJobCodeEntity> VW_vwJobCodeEntitys { get; set; }
        #endregion



        public HrisDbContext()
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <remarks>
        /// optionsBuilder.UseSqlServer(@"Data Source=localhost\sqlexpress2019;Initial Catalog=HRIS;Persist Security Info=True;Integrated Security=True;");
        /// optionsBuilder.UseSqlServer(@"Data Source=T450S\MSSQLSERVER2012;Initial Catalog=HRIS;Persist Security Info=True;Integrated Security=True;");
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //optionsBuilder.UseSqlServer(@"Data Source=10.16.15.10;Initial Catalog=HRIS;User ID=WAMSU;Password=P@ss1234;");
            //optionsBuilder.UseSqlServer(@"Data Source=10.160.35.172;Initial Catalog=HRM;User ID=HRIS_WHSU;Password=1qaz@WSX3edc");
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-J342AH8\SQLEXPRESS;Initial Catalog=HRIS;User ID=HRIS_WHSU;Password=1qaz@WSX3edc");
            base.OnConfiguring(optionsBuilder);
        }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Duty start======================================================================
            // 首頁待填報列表
            modelBuilder.Entity<AlterbyEmpIDEntity>(entity =>
            {
                entity.HasKey(e => new { e.EmpID, e.WorkingDate });
            });


            // 抓取員工單日假單、加班單&判斷員工單日工時
            modelBuilder.Entity<EmpLeavebyWorkDateEntity>(entity =>
            {
                entity.HasKey(e => new { e.EmpID, e.WorkDate });
                entity.Ignore(e => e.WorkDateString);
            });


            // 員工單日工時紀錄列表
            modelBuilder.Entity<EmpWorkdateEntity>(entity =>
            {
                entity.Ignore(e => e.WorkingDateString);
            });

            // 員工週間每日狀態顯示
            modelBuilder.Entity<EmpWorkingDateStatusList>(entity =>
            {
                entity.Ignore(e => e.WorkingDateString);
                entity.Ignore(e => e.SingRemark);
            });


            // 抓取員工首頁資訊
            modelBuilder.Entity<HomeInfoByEmpEntity>(entity =>
            {
                
            });


           
            


            // 員工萬年曆狀態列表
            modelBuilder.Entity<WorkingDateEntity>(entity =>
            {
                entity.HasKey(e => new { e.EmpID, e.WorkingDate });
                entity.Ignore(e => e.WorkDateString);
            });


            // 員工單日工時紀錄列表
            modelBuilder.Entity<WorkingDateDetailListEntity>(entity =>
            {
                entity.HasKey(e => new { e.RowUnid, e.JobCode });
            });

            // 員工單日可填報項目
            modelBuilder.Entity<WorkingHoursDetailEntity>(entity =>
            {
                entity.HasKey(e => new { e.RowUnid, e.TypeCode, e.JobCode });
            });

            // 抓取待批表單列表
            modelBuilder.Entity<WaitApproveEntity>(entity =>
            {

            });


            // 員工單日可填報項目
            modelBuilder.Entity<WorkingDateJobCodebyEmpIDEntity>(entity =>
            {
                entity.HasKey(e => new { e.TypeCode, e.JobCode});
            });



            // 輸入工時明細：回傳訊息
            modelBuilder.Entity<InsertWorkingHoursDetailReturnEntity>(entity =>
            {
                entity.HasKey(e => e.message);
            });


            // 更新工時明細：回傳訊息
            modelBuilder.Entity<UpdateWorkingHoursDetailReturnEntity>(entity =>
            {
                entity.HasKey(e => e.message);
            });
            // Duty end======================================================================






            // Sign start======================================================================
            // 批核工時-待批月曆及單日批核總計狀態
            modelBuilder.Entity<WaitApproveDayStaticEntity>(entity =>
            {
                entity.HasKey(e => new { e.FlowID, e.WorkingDate });
                entity.Ignore(e => e.WorkDateString);
            });

            modelBuilder.Entity<WaitApproveDetailEntity>(entity =>
            {
                
            });
            // Sign end======================================================================







            // Table based start=================================================================
            modelBuilder.Entity<WorkingHoursEntity>(entity =>
            {
                //entity.HasKey(e => e.RowUnid);
                //entity.ToTable("WorkingHours","whs");
            });


            modelBuilder.Entity<ProcessStatusEntity>(entity =>
            {
                
            });

            modelBuilder.Entity<ProcessStatusDetailEntity>(entity =>
            {
                entity.HasKey(e => new { e.RowUnid, e.SerialNo });
            });


            modelBuilder.Entity<EmployeeEntity>(entity =>
            {

            });
            // Table based end=================================================================





            // View based end=================================================================
            modelBuilder.Entity<vwWorkingHoursDetailEntity>(entity =>
            {
                entity.HasKey(e => new { e.RowUnid, e.TypeCode, e.JobCode });
            });


            modelBuilder.Entity<vwJobCodeEntity>(entity =>
            {

            });
            // View based end=================================================================

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
