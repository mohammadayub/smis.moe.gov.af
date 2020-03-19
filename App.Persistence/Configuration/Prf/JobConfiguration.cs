using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> entity)
        {
            entity.ToTable("Job", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.Employer).HasColumnType("character varying");

            entity.Property(e => e.EmployerAddress).HasColumnType("character varying");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.OccupationId).HasColumnName("OccupationID");

            entity.Property(e => e.PrevEmployer).HasColumnType("character varying");

            entity.Property(e => e.PrevEmployerAddress).HasColumnType("character varying");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Occupation)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.OccupationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_occ_fk");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_fk");
        }
    }
}
