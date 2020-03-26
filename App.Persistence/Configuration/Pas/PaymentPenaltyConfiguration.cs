using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class PaymentPenaltyConfiguration : IEntityTypeConfiguration<PaymentPenalty>
    {
        public void Configure(EntityTypeBuilder<PaymentPenalty> entity)
        {
            entity.ToTable("PaymentPenalty", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");

            entity.HasOne(d => d.Office)
                .WithMany(p => p.PaymentPenalty)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("paymentpenalty_fk");
        }
    }
}
