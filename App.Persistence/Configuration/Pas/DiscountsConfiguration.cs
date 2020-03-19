using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class DiscountsConfiguration : IEntityTypeConfiguration<Discounts>
    {
        public void Configure(EntityTypeBuilder<Discounts> entity)
        {
            entity.ToTable("Discounts", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ActiveFrom).HasColumnType("date");

            entity.Property(e => e.ActiveTo).HasColumnType("date");

            entity.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

            entity.HasOne(d => d.DiscountType)
                .WithMany(p => p.Discounts)
                .HasForeignKey(d => d.DiscountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_Discounts__FK");

            entity.HasOne(d => d.Office)
                .WithMany(p => p.Discounts)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("discounts_office_fk");
        }
    }
}
