using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> entity)
        {
            entity.ToTable("DiscountType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}
