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
                .HasDefaultValueSql("nextval('\"SMIS\".profile_seq'::regclass)");

            entity.Property(e => e.BirthLocationId).HasColumnName("BirthLocationID");

            entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");

            entity.Property(e => e.CDistrict).HasColumnName("CDistrict");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CProvince).HasColumnName("CProvince");

            entity.Property(e => e.CVillage)
                .HasColumnName("CVillage")
                .HasMaxLength(100);

            entity.Property(e => e.FirstNameEng)
                .HasColumnName("FirstNameEng")
                .HasMaxLength(100);

            entity.Property(e => e.LastNameEng)
                .HasColumnName("LastNameEng")
                .HasMaxLength(100);
            entity.Property(e => e.FatherNameEng)
                .HasColumnName("FatherNameEng")
                .HasMaxLength(100);
            

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            entity.Property(e => e.EducationId).HasColumnName("EducationID");

            entity.Property(e => e.EthnicityId).HasColumnName("EthnicityID");

            entity.Property(e => e.FatherName)
                .IsRequired()
                .HasMaxLength(90);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(90);

            entity.Property(e => e.GenderId).HasColumnName("GenderID");

            entity.Property(e => e.GrandFatherName)
                .IsRequired()
                .HasMaxLength(90);

            entity.Property(e => e.LastName).HasMaxLength(90);

           
            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

            entity.Property(e => e.Mobile).HasMaxLength(500);

            entity.Property(e => e.ModifiedBy).HasMaxLength(2000);

            entity.Property(e => e.NationalId)
                .IsRequired()
                .HasColumnName("NationalID")
                .HasMaxLength(100);

            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            entity.Property(e => e.PhotoPath)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.Prefix)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.ReferenceNo).HasMaxLength(10);

            entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

            entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.BirthLocation)
                .WithMany(e => e.ProfileBirthLocations)
                .HasForeignKey(d => d.BirthLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Location");

            entity.HasOne(d => d.BloodGroup)
                .WithMany(p => p.Profile)
                .HasForeignKey(d => d.BloodGroupId)
                .HasConstraintName("FK_Profile_BloodGroup");


            entity.HasOne(d => d.DistrictNavigation)
                .WithMany(e => e.ProfileDistrictNavigations)
                .HasForeignKey(d => d.District)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Location2");

            entity.HasOne(d => d.DocumentType)
                .WithMany()
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_DocumentType");

            entity.HasOne(d => d.Education)
                .WithMany(p => p.Profile)
                .HasForeignKey(d => d.EducationId)
                .HasConstraintName("FK_Profile_Education");

            entity.HasOne(d => d.Ethnicity)
                .WithMany(e => e.Profiles)
                .HasForeignKey(d => d.EthnicityId)
                .HasConstraintName("FK_Profile_Ethnicity");

            entity.HasOne(d => d.Gender)
                .WithMany(e => e.Profiles)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Gender");

            entity.HasOne(d => d.MaritalStatus)
                .WithMany(e => e.Profiles)
                .HasForeignKey(d => d.MaritalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_MaritalStatus");

            entity.HasOne(d => d.Organization)
                .WithMany(/*e => e.Profiles*/)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Organization");

            entity.HasOne(d => d.ProvinceNavigation)
                .WithMany(e => e.ProfileProvinceNavigations)
                .HasForeignKey(d => d.Province)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Location1");

            entity.HasOne(d => d.Religion)
                .WithMany(e => e.Profiles)
                .HasForeignKey(d => d.ReligionId)
                .HasConstraintName("FK_Profile_Religion");

        }



    }
}
