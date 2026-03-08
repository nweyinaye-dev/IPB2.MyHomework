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

    public virtual DbSet<BlogDetail> BlogDetails { get; set; }

    public virtual DbSet<BlogHeader> BlogHeaders { get; set; }

    public virtual DbSet<Snake> Snakes { get; set; }

    public virtual DbSet<TblA> TblAs { get; set; }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblB> TblBs { get; set; }

    public virtual DbSet<TblBatch> TblBatches { get; set; }

    public virtual DbSet<TblBook> TblBooks { get; set; }

    public virtual DbSet<TblMyanmarMonth> TblMyanmarMonths { get; set; }

    public virtual DbSet<TblMyanmarMonthsImg> TblMyanmarMonthsImgs { get; set; }

    public virtual DbSet<TblSnake> TblSnakes { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblTransactionRecord> TblTransactionRecords { get; set; }

    public virtual DbSet<TblWallet> TblWallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IPB2;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogDetail>(entity =>
        {
            entity.HasKey(e => e.BlogDetailId).HasName("PK__BlogDeta__2383E83EDAC19244");

            entity.ToTable("BlogDetail");

            entity.Property(e => e.BlogDetailId).ValueGeneratedNever();
        });

        modelBuilder.Entity<BlogHeader>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BlogHead__54379E30488F651F");

            entity.ToTable("BlogHeader");

            entity.Property(e => e.BlogId).ValueGeneratedNever();
            entity.Property(e => e.BlogTitle).HasMaxLength(255);
        });

        modelBuilder.Entity<Snake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Snakes__3214EC072DD3FD8B");

            entity.Property(e => e.EngName).HasMaxLength(200);
            entity.Property(e => e.Mmname)
                .HasMaxLength(200)
                .HasColumnName("MMName");
        });

        modelBuilder.Entity<TblA>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_A");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.ToTable("Tbl_Account");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblB>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_B");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TblBatch>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Batch");

            entity.Property(e => e.BatchId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BatchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FromDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.ToDate)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBook>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Tbl_Book__3DE0C207C9654C3A");

            entity.ToTable("Tbl_Book");

            entity.Property(e => e.BookId).ValueGeneratedNever();
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TblMyanmarMonth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Myan__3214EC074E1E1884");

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

        modelBuilder.Entity<TblSnake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Snak__3214EC073157ADDE");

            entity.ToTable("Tbl_Snake");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EngName).HasMaxLength(200);
            entity.Property(e => e.IsDanger).HasMaxLength(10);
            entity.Property(e => e.IsPoison).HasMaxLength(10);
            entity.Property(e => e.Mmname)
                .HasMaxLength(200)
                .HasColumnName("MMName");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("Tbl_Student");

            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClassNo)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.ParentName).HasMaxLength(50);
            entity.Property(e => e.StudentName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTransactionRecord>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Tbl_Tran__55433A6B7B513514");

            entity.ToTable("Tbl_TransactionRecord");

            entity.Property(e => e.TransactionId).HasMaxLength(100);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FromMobileNo).HasMaxLength(20);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ToMobileNo).HasMaxLength(20);
            entity.Property(e => e.TxnId).HasMaxLength(100);
        });

        modelBuilder.Entity<TblWallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Tbl_Wall__84D4F92EFB1409BA");

            entity.ToTable("Tbl_Wallet");

            entity.Property(e => e.WalletId)
                .HasMaxLength(100)
                .HasColumnName("WalletID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
