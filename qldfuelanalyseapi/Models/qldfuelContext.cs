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

        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Sites> Sites { get; set; }
        public virtual DbSet<UpdatesPerStation> UpdatesPerStation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prices>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("prices_pkey");

                entity.ToTable("prices");

                entity.HasIndex(e => e.SiteId)
                    .HasName("fki_sites");

                entity.Property(e => e.FuelType)
                    .IsRequired()
                    .HasColumnName("Fuel_Type")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sites");
            });

            modelBuilder.Entity<Sites>(entity =>
            {
                entity.HasKey(e => e.SiteId)
                    .HasName("sites_pkey");

                entity.ToTable("sites");

                entity.Property(e => e.SiteId).ValueGeneratedNever();

                entity.Property(e => e.SiteBrand)
                    .HasColumnName("Site_Brand")
                    .HasColumnType("character varying");

                entity.Property(e => e.SiteLatitude)
                    .HasColumnName("Site_Latitude")
                    .HasColumnType("numeric");

                entity.Property(e => e.SiteLongitude)
                    .HasColumnName("Site_Longitude")
                    .HasColumnType("numeric");

                entity.Property(e => e.SiteName)
                    .HasColumnName("Site_Name")
                    .HasColumnType("character varying");

                entity.Property(e => e.SitePostCode).HasColumnName("Site_Post_Code");

                entity.Property(e => e.SiteState)
                    .HasColumnName("Site_State")
                    .HasColumnType("character varying");

                entity.Property(e => e.SiteSuburb)
                    .HasColumnName("Site_Suburb")
                    .HasColumnType("character varying");

                entity.Property(e => e.SitesAddressLine1)
                    .HasColumnName("Sites_Address_Line_1")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<UpdatesPerStation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("updates_per_station");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.SiteName)
                    .HasColumnName("Site_Name")
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
