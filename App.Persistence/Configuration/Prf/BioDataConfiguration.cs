using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class BioDataConfiguration : IEntityTypeConfiguration<BioData>
    {
        public void Configure(EntityTypeBuilder<BioData> entity)
        {
            entity.ToTable("BioData", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp(6) with time zone");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");

            entity.Property(e => e.Email).HasColumnType("character varying");

            entity.Property(e => e.FamilyName)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.FamilyNameEn)
                .IsRequired()
                .HasColumnName("FamilyNameEN")
                .HasColumnType("character varying");

            entity.Property(e => e.FatherName)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.FatherNameEn)
                .IsRequired()
                .HasColumnName("FatherNameEN")
                .HasColumnType("character varying");

            entity.Property(e => e.GrandFatherName)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.GrandFatherNameEn)
                .IsRequired()
                .HasColumnName("GrandFatherNameEN")
                .HasColumnType("character varying");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.NameEn)
                .IsRequired()
                .HasColumnName("NameEN")
                .HasColumnType("character varying");

            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.BioData)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("biodata_fk");
        }
    }
}
