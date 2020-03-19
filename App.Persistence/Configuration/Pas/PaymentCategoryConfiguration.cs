using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class PaymentCategoryConfiguration : IEntityTypeConfiguration<PaymentCategory>
    {
        public void Configure(EntityTypeBuilder<PaymentCategory> entity)
        {
            entity.ToTable("PaymentCategory", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}
