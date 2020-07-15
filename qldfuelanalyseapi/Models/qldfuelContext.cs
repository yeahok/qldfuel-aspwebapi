using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace qldfuelanalyseapi.Models
{
    public partial class qldfuelContext : DbContext
    {
        public qldfuelContext()
        {
        }

        public qldfuelContext(DbContextOptions<qldfuelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Fuel> Fuel { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<SiteFuel> SiteFuel { get; set; }
        public virtual DbSet<SiteRegion> SiteRegion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Fuel>(entity =>
            {
                entity.ToTable("fuel");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("price");

                entity.HasIndex(e => new { e.SiteId, e.FuelId, e.TransactionDate })
                    .HasName("price_site_id_fuel_id_transaction_date_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CollectionMethod)
                    .HasColumnName("collection_method")
                    .HasColumnType("character varying");

                entity.Property(e => e.FuelId).HasColumnName("fuel_id");

                entity.Property(e => e.SiteId).HasColumnName("site_id");

                entity.Property(e => e.TransactionDate)
                    .HasColumnName("transaction_date")
                    .HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Fuel)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.FuelId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("price_fuel_id_fkey");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("price_site_id_fkey");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.HasIndex(e => new { e.OriginalId, e.GeographicalLevel })
                    .HasName("region_original_id_geographical_level_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbreviation)
                    .HasColumnName("abbreviation")
                    .HasColumnType("character varying");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.GeographicalLevel).HasColumnName("geographical_level");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.OriginalId).HasColumnName("original_id");

                entity.Property(e => e.RegionParentId).HasColumnName("region_parent_id");

                entity.HasOne(d => d.RegionParent)
                    .WithMany(p => p.InverseRegionParent)
                    .HasForeignKey(d => d.RegionParentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("region_region_parent_id_fkey");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("site");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("character varying");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("numeric");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("numeric");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.PostCode)
                    .HasColumnName("post_code")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Site)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("site_brand_id_fkey");
            });

            modelBuilder.Entity<SiteFuel>(entity =>
            {
                entity.ToTable("site_fuel");

                entity.HasIndex(e => new { e.SiteId, e.FuelId })
                    .HasName("site_fuel_site_id_fuel_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.FuelId).HasColumnName("fuel_id");

                entity.Property(e => e.SiteId).HasColumnName("site_id");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SiteFuel)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("site_fuel_site_id_fkey");
            });

            modelBuilder.Entity<SiteRegion>(entity =>
            {
                entity.ToTable("site_region");

                entity.HasIndex(e => new { e.SiteId, e.RegionId })
                    .HasName("site_region_site_id_region_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.Property(e => e.SiteId).HasColumnName("site_id");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.SiteRegion)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("site_region_region_id_fkey");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SiteRegion)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("site_region_site_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
