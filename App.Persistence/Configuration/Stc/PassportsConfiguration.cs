using App.Domain.Entity.stc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Stc
{
    public class PassportsConfiguration : IEntityTypeConfiguration<Passports>
    {
        public void Configure(EntityTypeBuilder<Passports> entity)
        {
            entity.ToTable("Passports", "stc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportNumber).HasColumnType("character varying");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.StockInId).HasColumnName("StockInID");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.StockIn)
                .WithMany(p => p.Passports)
                .HasForeignKey(d => d.StockInId)
                .HasConstraintName("_Passports__FK");
        }
    }
}
