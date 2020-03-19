using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class ProfileHashConfiguration : IEntityTypeConfiguration<ProfileHash>
    {
        public void Configure(EntityTypeBuilder<ProfileHash> entity)
        {
            entity.ToTable("ProfileHash", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.ProfileHash)
                .HasForeignKey(d => d.ProfileId)
                .HasConstraintName("_ProfileHash__FK");
        }
    }
}
