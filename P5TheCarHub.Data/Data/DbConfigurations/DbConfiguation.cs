using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Infrastructure.Data.DbConfigurations
{
    public class DbConfiguation
    {
        public void ConfigureVehicle(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey("Id");

            builder.Property(v => v.VIN)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(v => v.Mileage)
                .IsRequired(false);
            builder.Property(v => v.Year)
               .IsRequired(true);
            builder.Property(v => v.Make)
               .HasMaxLength(15)
               .IsRequired(true);
            builder.Property(v => v.Model)
               .HasMaxLength(15)
               .IsRequired(true);
            builder.Property(v => v.Trim)
               .HasMaxLength(10)
               .IsRequired(false);
            builder.Property(v => v.Color)
                .HasMaxLength(15)
                .IsRequired(false);
            builder.Property(v => v.LastUpdated)
                .IsRequired(true);

            builder.Property(v => v.SalePrice)
               .IsRequired(true)
               .HasColumnType("decimal(18,2)");
            builder.Property(v => v.PurchasePrice)
               .IsRequired(true)
               .HasColumnType("decimal(18,2)");
            builder.Property(v => v.PurchaseDate)
                .IsRequired(true);
            builder.Property(v => v.LotDate)
                .IsRequired(true);
            builder.Property(v => v.SaleDate)
                .IsRequired(false);

        }

        public void ConfigureRepair(EntityTypeBuilder<Repair> builder)
        {
            builder.HasKey("Id");
            builder.Property(r => r.Description)
                .HasMaxLength(200)
                .IsRequired(true);
            builder.Property(r => r.Details)
               .HasMaxLength(3000);
            builder.Property(r => r.Cost)
               .IsRequired(true)
               .HasColumnType("decimal(18,2)");
            builder.Property(r => r.RepairDate)
                .IsRequired(false);
            builder.Property(r => r.LastUpdated)
                .IsRequired(true);
        }

        public void ConfigurePhoto(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey("Id");
            builder.Property(r => r.ImageUrl)
                .HasMaxLength(1000)
                .IsRequired(true);
            builder.Property(r => r.Description)
                .IsRequired(false)
               .HasMaxLength(500);
            builder.Property(r => r.IsMain)
               .HasDefaultValue(false);
            builder.Property(r => r.LastUpdated)
                .IsRequired(true);

        }

        
        public void ConfigureInvoice(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey("Id");

            builder.Property(r => r.CustomerName)
               .HasMaxLength(200)
               .IsRequired(false);

            builder.Property(r => r.InvoiceNumber)
                .HasMaxLength(255)
                .IsRequired(true);
            builder.Property(r => r.PriceSold)
               .IsRequired(true)
               .HasColumnType("decimal(18,2)");
            builder.Property(i => i.DateSold)
                 .IsRequired(false);

        }
    }
}
