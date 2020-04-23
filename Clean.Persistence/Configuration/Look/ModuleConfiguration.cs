using Clean.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Look
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> entity)
        {
            entity.ToTable("Module", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

        }
    }
}
