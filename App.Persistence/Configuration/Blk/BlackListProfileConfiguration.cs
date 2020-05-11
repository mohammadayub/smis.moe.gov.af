using App.Domain.Entity.blk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class BlackListProfileConfiguration : IEntityTypeConfiguration<BlackListProfile>
    {
        public void Configure(EntityTypeBuilder<BlackListProfile> entity)
        {
            entity.ToTable("BlackListProfile", "blk");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BirthCountryId).HasColumnName("BirthCountryID");

            entity.Property(e => e.BirthProvinceId).HasColumnName("BirthProvinceID");

            entity.Property(e => e.Code).HasColumnType("character varying");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            entity.Property(e => e.EyeColorId).HasColumnName("EyeColorID");

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

            entity.Property(e => e.GenderId).HasColumnName("GenderID");

            entity.Property(e => e.GrandFatherName)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.GrandFatherNameEn)
                .IsRequired()
                .HasColumnName("GrandFatherNameEN")
                .HasColumnType("character varying");

            entity.Property(e => e.HairColorId).HasColumnName("HairColorID");

            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.NameEn)
                .IsRequired()
                .HasColumnName("NameEN")
                .HasColumnType("character varying");

            entity.Property(e => e.NationalId)
                .IsRequired()
                .HasColumnName("NationalID")
                .HasColumnType("character varying");

            entity.Property(e => e.OtherDetail).HasColumnType("character varying");

            entity.Property(e => e.OtherNationalityId).HasColumnName("OtherNationalityID");

            entity.Property(e => e.PhotoPath).HasColumnType("character varying");

            entity.Property(e => e.ProfileId)
                    .HasColumnName("ProfileID")
                    .HasColumnType("character varying");

            entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

            entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.TitleId).HasColumnName("TitleID");

            entity.HasOne(d => d.BirthCountry)
                .WithMany()
                .HasForeignKey(d => d.BirthCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_1");

            entity.HasOne(d => d.BirthProvince)
                .WithMany()
                .HasForeignKey(d => d.BirthProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_6");

            entity.HasOne(d => d.DocumentType)
                .WithMany()
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_7");

            entity.HasOne(d => d.EyeColor)
                .WithMany()
                .HasForeignKey(d => d.EyeColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_3");

            entity.HasOne(d => d.Gender)
                .WithMany()
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_4");

            entity.HasOne(d => d.HairColor)
                .WithMany()
                .HasForeignKey(d => d.HairColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_2");

            entity.HasOne(d => d.MaritalStatus)
                .WithMany()
                .HasForeignKey(d => d.MaritalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk_5");

            entity.HasOne(d => d.OtherNationality)
                .WithMany()
                .HasForeignKey(d => d.OtherNationalityId)
                .HasConstraintName("blacklistprofile_otn_fk");

            entity.HasOne(d => d.ResidenceCountry)
                .WithMany()
                .HasForeignKey(d => d.ResidenceCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_fk");

            entity.HasOne(d => d.Title)
                .WithMany()
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklistprofile_pt_fk");
        }
    }
}
