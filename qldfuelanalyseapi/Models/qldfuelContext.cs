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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("price");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CollectionMethod)
                    .HasColumnName("collection_method")
                    .HasColumnType("character varying");

                entity.Property(e => e.FuelId).HasColumnName("fuel_id");

                entity.Property(e => e.SiteId).HasColumnName("site_id");

                entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");

                entity.HasOne(d => d.Fuel)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.FuelId)
                    .HasConstraintName("price_fuel_id_fkey");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("price_site_id_fkey");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbrevation)
                    .HasColumnName("abbrevation")
                    .HasColumnType("character varying");

                entity.Property(e => e.GeographicalLevel).HasColumnName("geographical_level");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.OriginalId).HasColumnName("original_id");

                entity.Property(e => e.RegionParentId).HasColumnName("region_parent_id");

                entity.HasOne(d => d.RegionParent)
                    .WithMany(p => p.InverseRegionParent)
                    .HasForeignKey(d => d.RegionParentId)
                    .HasConstraintName("region_region_parent_id_fkey");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("site");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.PostCode)
                    .HasColumnName("post_code")
                    .HasColumnType("character varying");

                entity.Property(e => e.RegionLevel1Id).HasColumnName("region_level_1_id");

                entity.Property(e => e.RegionLevel2Id).HasColumnName("region_level_2_id");

                entity.Property(e => e.RegionLevel3Id).HasColumnName("region_level_3_id");

                entity.Property(e => e.RegionLevel4Id).HasColumnName("region_level_4_id");

                entity.Property(e => e.RegionLevel5Id).HasColumnName("region_level_5_id");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Site)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("site_brand_id_fkey");

                entity.HasOne(d => d.RegionLevel1)
                    .WithMany(p => p.SiteRegionLevel1)
                    .HasForeignKey(d => d.RegionLevel1Id)
                    .HasConstraintName("site_region_level_1_id_fkey");

                entity.HasOne(d => d.RegionLevel2)
                    .WithMany(p => p.SiteRegionLevel2)
                    .HasForeignKey(d => d.RegionLevel2Id)
                    .HasConstraintName("site_region_level_2_id_fkey");

                entity.HasOne(d => d.RegionLevel3)
                    .WithMany(p => p.SiteRegionLevel3)
                    .HasForeignKey(d => d.RegionLevel3Id)
                    .HasConstraintName("site_region_level_3_id_fkey");

                entity.HasOne(d => d.RegionLevel4)
                    .WithMany(p => p.SiteRegionLevel4)
                    .HasForeignKey(d => d.RegionLevel4Id)
                    .HasConstraintName("site_region_level_4_id_fkey");

                entity.HasOne(d => d.RegionLevel5)
                    .WithMany(p => p.SiteRegionLevel5)
                    .HasForeignKey(d => d.RegionLevel5Id)
                    .HasConstraintName("site_region_level_5_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
