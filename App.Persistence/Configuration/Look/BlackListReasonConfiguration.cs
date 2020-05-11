using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class BlackListReasonConfiguration : IEntityTypeConfiguration<BlackListReason>
    {
        public void Configure(EntityTypeBuilder<BlackListReason> entity)
        {
            entity.ToTable("BlackListReason", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.TitleEn)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}
