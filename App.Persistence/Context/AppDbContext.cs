
using App.Domain.Entity.look;
using App.Domain.Entity.prf;
using App.Domain.Entity.prf.view;
using Clean.Common;
using Clean.Domain.Entity.look;
using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Context
{
    public class AppDbContext : BaseContext
    {
        public static readonly ILoggerFactory DbLogger = LoggerFactory.Create(ex => ex.AddConsole());

        public AppDbContext(DbContextOptions<AppDbContext> options, UserManager<AppUser> manager) : base(options, manager)
        {

        }

        public virtual DbSet<Clean.Domain.Entity.look.Country> Countries { get; set; }
        
        public virtual DbSet<Clean.Domain.Entity.look.District> Districts { get; set; }
        
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<Domain.Entity.look.Office> Offices { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Profile>  Profiles { get; set; }

        public virtual DbSet<Application> Applications { get; set; }

        public virtual DbSet<ProfileProcess> ProfileProcesses { get; set; }


        public virtual DbSet<App.Domain.Entity.look.Location> Locations { get; set; }
        
        public virtual DbSet<App.Domain.Entity.look.Ethnicity> Ethnicities { get; set; }

        public virtual DbSet<App.Domain.Entity.look.Religion> Religions { get; set; }

        
        public virtual DbSet<Clean.Domain.Entity.look.Province> Provinces { get; set; }
        public virtual DbSet<App.Domain.Entity.look.Gender> Genders { get; set; }

        public virtual DbSet<App.Domain.Entity.look.BloodGroup> BloodGroups { get; set; }

        public virtual DbSet<App.Domain.Entity.look.MaritalStatus> MaritalStatus { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseLoggerFactory(DbLogger);
                options.EnableSensitiveDataLogging(true);
                options.UseNpgsql(AppConfig.BaseConnectionString, (opts) =>
                {
                    
                });
            }
            base.OnConfiguring(options);
        }
    }
}
