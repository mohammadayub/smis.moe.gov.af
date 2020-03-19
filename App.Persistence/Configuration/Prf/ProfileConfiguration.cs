using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> entity)
        {
            entity.ToTable("Profile", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BirthCountryId).HasColumnName("BirthCountryID");

            entity.Property(e => e.BirthProvinceId).HasColumnName("BirthProvinceID");

            entity.Property(e => e.Code).HasColumnType("character varying");

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            entity.Property(e => e.EyeColorId).HasColumnName("EyeColorID");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");

            entity.Property(e => e.HairColorId).HasColumnName("HairColorID");

            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

            entity.Property(e => e.OtherNationalityId).HasColumnName("OtherNationalityID");

            entity.Property(e => e.TitleId).HasColumnName("TitleID");

            entity.Property(e => e.NationalId)
                .IsRequired()
                .HasColumnName("NationalID")
                .HasColumnType("character varying");

            entity.Property(e => e.OtherDetail).HasColumnType("character varying");

            entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

            entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

            entity.HasOne(d => d.BirthCountry)
                .WithMany()
                .HasForeignKey(d => d.BirthCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_1");

            entity.HasOne(d => d.BirthProvince)
                .WithMany()
                .HasForeignKey(d => d.BirthProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_6");

            entity.HasOne(d => d.DocumentType)
                .WithMany()
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_7");

            entity.HasOne(d => d.EyeColor)
                .WithMany(p => p.ProfileEyeColor)
                .HasForeignKey(d => d.EyeColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_3");

            entity.HasOne(d => d.Gender)
                .WithMany(p => p.Profile)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_4");

            entity.HasOne(d => d.HairColor)
                .WithMany(p => p.ProfileHairColor)
                .HasForeignKey(d => d.HairColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_2");

            entity.HasOne(d => d.MaritalStatus)
                .WithMany(p => p.Profile)
                .HasForeignKey(d => d.MaritalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk_5");

            entity.HasOne(d => d.ResidenceCountry)
                .WithMany()
                .HasForeignKey(d => d.ResidenceCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_fk");

            entity.HasOne(d => d.Title)
                .WithMany()
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("profile_pt_fk");

            entity.HasOne(d => d.OtherNationality)
                    .WithMany()
                    .HasForeignKey(d => d.OtherNationalityId)
                    .HasConstraintName("profile_otn_fk");
        }
    }
}
