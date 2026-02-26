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

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblTransactionRecord> TblTransactionRecords { get; set; }

    public virtual DbSet<TblWallet> TblWallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IPB2;User ID=sa;Password=system;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
