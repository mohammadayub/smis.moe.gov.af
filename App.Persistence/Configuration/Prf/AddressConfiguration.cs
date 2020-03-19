using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("Address", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

            entity.Property(e => e.City).HasColumnType("character varying");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");

            entity.Property(e => e.Detail).HasColumnType("character varying");

            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

            entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.Village).HasColumnType("character varying");

            entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_adtp_fk");

            entity.HasOne(d => d.District)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_dist_fk");

            entity.HasOne(d => d.Country)
                .WithMany()
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_fk");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.Address)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_fk_2");

            entity.HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_fk_1");
        }
    }
}
