using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class PassportApplicationConfiguration : IEntityTypeConfiguration<PassportApplication>
    {
        public void Configure(EntityTypeBuilder<PassportApplication> entity)
        {
            entity.ToTable("PassportApplication", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ActiveAddressId).HasColumnName("ActiveAddressID");

            entity.Property(e => e.ActiveBioDataId).HasColumnName("ActiveBioDataID");

            entity.Property(e => e.ActiveJobId).HasColumnName("ActiveJobID");

            entity.Property(e => e.BankId).HasColumnName("BankID");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.CurProcessId).HasColumnName("CurProcessID");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportDurationId).HasColumnName("PassportDurationID");

            entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

            entity.Property(e => e.PaymentCategoryId).HasColumnName("PaymentCategoryID");

            entity.Property(e => e.PaymentDate).HasColumnType("date");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

            entity.Property(e => e.PaymentPenaltyId).HasColumnName("PaymentPenaltyID");

            entity.Property(e => e.PhotoPath).HasColumnType("character varying");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.ReceiptNumer).HasMaxLength(50);

            entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

            entity.Property(e => e.RequestTypeId).HasColumnName("RequestTypeID");

            entity.Property(e => e.SignaturePath).HasColumnType("character varying");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.ActiveAddress)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.ActiveAddressId)
                .HasConstraintName("passportapplication_aadr_fk");

            entity.HasOne(d => d.ActiveBioData)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.ActiveBioDataId)
                .HasConstraintName("passportapplication_abid_fk");

            entity.HasOne(d => d.ActiveJob)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.ActiveJobId)
                .HasConstraintName("passportapplication_acjb_fk_1");

            entity.HasOne(d => d.Bank)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("passportapplication_bank_fk");

            entity.HasOne(d => d.Discount)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("passportapplication_discount_fk");

            entity.HasOne(d => d.PassportDuration)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.PassportDurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_pd_fk");

            entity.HasOne(d => d.PassportType)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.PassportTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_fk");

            entity.HasOne(d => d.PaymentCategory)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.PaymentCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_pycat_fk");

            entity.HasOne(d => d.PaymentMethod)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_pymtd_fk");

            entity.HasOne(d => d.PaymentPenalty)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.PaymentPenaltyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_pypen_fk");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_prf_fk");

            entity.HasOne(d => d.RequestType)
                .WithMany(p => p.PassportApplication)
                .HasForeignKey(d => d.RequestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("passportapplication_reqtp_fk");
        }
    }
}
