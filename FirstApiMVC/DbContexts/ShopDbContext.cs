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
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PM9DUD3;Initial Catalog=Shop;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.Property(e => e.PartnerId).ValueGeneratedNever();
            entity.Property(e => e.PartnerName).IsFixedLength();
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.Property(e => e.PartnerTypeId).ValueGeneratedNever();
            entity.Property(e => e.PartnerTypeName).IsFixedLength();
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.Property(e => e.PurchaseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.Property(e => e.DetailsId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.SalesId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.Property(e => e.DetailsId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
