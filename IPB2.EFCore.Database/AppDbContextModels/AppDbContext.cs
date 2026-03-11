using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblAttendance> TblAttendances { get; set; }

    public virtual DbSet<TblAttendanceLog> TblAttendanceLogs { get; set; }

    public virtual DbSet<TblBurmeseRecipe> TblBurmeseRecipes { get; set; }

    public virtual DbSet<TblClass> TblClasses { get; set; }

    public virtual DbSet<TblGrade> TblGrades { get; set; }

    public virtual DbSet<TblLeave> TblLeaves { get; set; }

    public virtual DbSet<TblMyanmarMonth> TblMyanmarMonths { get; set; }

    public virtual DbSet<TblMyanmarMonthsImg> TblMyanmarMonthsImgs { get; set; }

    public virtual DbSet<TblSchedule> TblSchedules { get; set; }

    public virtual DbSet<TblStudentEnroll> TblStudentEnrolls { get; set; }

    public virtual DbSet<TblTeacher> TblTeachers { get; set; }

    public virtual DbSet<TblTransactionRecord> TblTransactionRecords { get; set; }

    public virtual DbSet<TblUserType> TblUserTypes { get; set; }

    public virtual DbSet<TblWallet> TblWallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IPB2;User ID=sa;Password=system;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answer__D482500408B54D69");

            entity.ToTable("Answer");

            entity.Property(e => e.AnswerImageUrl).HasMaxLength(500);
            entity.Property(e => e.AnswerName).HasMaxLength(255);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC04E4BC85");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionName).HasMaxLength(255);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__Recipes__A2B5777C5AEE82B9");

            entity.Property(e => e.Guid).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC2707F6335A");

            entity.ToTable("Tbl_Account");

            entity.HasIndex(e => e.MobileNo, "UQ__Account__D6D73A860AD2A005").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("ID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<TblAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Atte__3214EC073E1D39E1");

            entity.ToTable("Tbl_Attendance");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.ClassId).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Late).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentEnrollId).HasMaxLength(100);
            entity.Property(e => e.TimeIn).HasMaxLength(20);
            entity.Property(e => e.TimeOut).HasMaxLength(20);
        });

        modelBuilder.Entity<TblAttendanceLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Atte__3214EC073A4CA8FD");

            entity.ToTable("Tbl_AttendanceLog");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.ClassId).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.StudentEnrollId).HasMaxLength(100);
            entity.Property(e => e.TimeIn).HasMaxLength(20);
            entity.Property(e => e.TimeOut).HasMaxLength(20);
        });

        modelBuilder.Entity<TblBurmeseRecipe>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__Tbl_Burm__A2B5777C01142BA1");

            entity.ToTable("Tbl_Burmese_Recipe");

            entity.Property(e => e.Guid).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<TblClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Clas__3214EC07367C1819");

            entity.ToTable("Tbl_Class");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.ClassName).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.ScheduleId)
                .HasMaxLength(100)
                .HasColumnName("ScheduleID");
            entity.Property(e => e.TeacherId)
                .HasMaxLength(100)
                .HasColumnName("TeacherID");
        });

        modelBuilder.Entity<TblGrade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Grad__3214EC072B0A656D");

            entity.ToTable("Tbl_Grade");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.GradeName).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
        });

        modelBuilder.Entity<TblLeave>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Leav__3214EC0741EDCAC5");

            entity.ToTable("Tbl_Leave");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.ClassId).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.StudentEnrollId).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMyanmarMonth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Myan__3214EC072A4B4B5E");

            entity.ToTable("Tbl_MyanmarMonths");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FestivalEn).HasMaxLength(200);
            entity.Property(e => e.FestivalMm).HasMaxLength(200);
            entity.Property(e => e.MonthEn).HasMaxLength(100);
            entity.Property(e => e.MonthMm).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMyanmarMonthsImg>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_MyanmarMonthsImg");

            entity.Property(e => e.ImgName).HasMaxLength(50);
            entity.Property(e => e.ImgUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<TblSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Sche__3214EC072EDAF651");

            entity.ToTable("Tbl_Schedule");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.EndTime).HasMaxLength(20);
            entity.Property(e => e.ScheduleDays).HasMaxLength(100);
            entity.Property(e => e.ScheduleName).HasMaxLength(100);
            entity.Property(e => e.StartTime).HasMaxLength(20);
        });

        modelBuilder.Entity<TblStudentEnroll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Stud__3214EC0732AB8735");

            entity.ToTable("Tbl_StudentEnroll");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.ClassId).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.StudentName).HasMaxLength(100);
            entity.Property(e => e.StudentPhoneno).HasMaxLength(20);
        });

        modelBuilder.Entity<TblTeacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Teac__3214EC07236943A5");

            entity.ToTable("Tbl_Teacher");

            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.TeacherAddress).HasMaxLength(100);
            entity.Property(e => e.TeacherName).HasMaxLength(100);
            entity.Property(e => e.TeacherPhoneno).HasMaxLength(20);
        });

        modelBuilder.Entity<TblTransactionRecord>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Tbl_Tran__55433A6B267ABA7A");

            entity.ToTable("Tbl_TransactionRecord");

            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FromMobileNo).HasMaxLength(20);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.ToMobileNo).HasMaxLength(20);
            entity.Property(e => e.TxnId).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUserType>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tbl_User__1788CC4C7D439ABD");

            entity.ToTable("Tbl_User_Type");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.UserCode).HasMaxLength(50);
            entity.Property(e => e.UserEngType).HasMaxLength(100);
            entity.Property(e => e.UserMmtype)
                .HasMaxLength(100)
                .HasColumnName("UserMMType");
        });

        modelBuilder.Entity<TblWallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Tbl_Wall__84D4F92E1B0907CE");

            entity.ToTable("Tbl_Wallet");

            entity.Property(e => e.WalletId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WalletID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
