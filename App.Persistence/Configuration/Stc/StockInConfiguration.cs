using App.Domain.Entity.stc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Stc
{
    public class StockInConfiguration : IEntityTypeConfiguration<StockIn>
    {
        public void Configure(EntityTypeBuilder<StockIn> entity)
        {
            entity.ToTable("StockIn", "stc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportDurationId).HasColumnName("PassportDurationID");

            entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
        }
    }
}
