using System;
using System.Collections.Generic;
using FirstApiMVC.DBContexts.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DbContexts;

public partial class ShopDbContext : DbContext
{
    public ShopDbContext()
    {
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesDetail> SalesDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PM9DUD3;Initial Catalog=Shop;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.Property(e => e.FileUrl)
                .HasMaxLength(250)
                .IsFixedLength();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("imageURL");
            entity.Property(e => e.ItemName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.ToTable("Partner");

            entity.Property(e => e.PartnerName)
                .HasMaxLength(250)
                .IsFixedLength();
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.ToTable("PartnerType");

            entity.Property(e => e.PartnerTypeId).HasColumnName("PartnerTypeID");
            entity.Property(e => e.PartnerTypeName)
                .HasMaxLength(250)
                .IsFixedLength();
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.ToTable("Purchase");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.DetailsId);

            entity.Property(e => e.ItemQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesId);

            entity.Property(e => e.SalesDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.HasKey(e => e.DetailsId);

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
