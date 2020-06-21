using App.Domain.Entity.prf.view;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{


    public class vProfileProcessConfiguration : IEntityTypeConfiguration<ProfileProcess>
    {
        public void Configure(EntityTypeBuilder<ProfileProcess> entity)
        {
            entity.ToTable("vProfileProcess", "prf");
            entity.Property(e => e.ApplicationID).HasColumnName("ApplicationID");
        }

    }
}
