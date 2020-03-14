using Clean.Common;
using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Context
{
    public class AppDbContext : BaseContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, UserManager<AppUser> manager) : base(options, manager)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql(AppConfig.BaseConnectionString, (opts) =>
                {
                    opts.EnableRetryOnFailure(2);
                });
            }
            base.OnConfiguring(options);
        }
    }
}
