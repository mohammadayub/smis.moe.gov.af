using App.Domain.Entity.doc;
using App.Domain.Entity.look;
using App.Domain.Entity.pas;
using App.Domain.Entity.prc;
using App.Domain.Entity.prf;
using App.Domain.Entity.prt;
using App.Domain.Entity.qc;
using App.Domain.Entity.stc;
using Clean.Common;
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

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<AuthorizationQueue> AuthorizationQueues { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BioData> BioDatas { get; set; }
        public virtual DbSet<Biometric> Biometrics { get; set; }
        public virtual DbSet<BiometricDetail> BiometricDetails{ get; set; }
        public virtual DbSet<BiometricType> BiometricType { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Clean.Domain.Entity.look.Country> Countries { get; set; }
        public virtual DbSet<CrimeType> CrimeTypes { get; set; }
        public virtual DbSet<CriminalRecord> CriminalRecords { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DiscountType> DiscountTypes { get; set; }
        public virtual DbSet<Discounts> Discounts { get; set; }
        public virtual DbSet<Clean.Domain.Entity.look.District> Districts { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<DisabledReason> DisabledReasons { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<DisabledPassport> DisabledPassports { get; set; }
        public virtual DbSet<PassportApplication> PassportApplications { get; set; }
        public virtual DbSet<PassportDuration> PassportDurations { get; set; }
        public virtual DbSet<PassportPrint> PassportPrints { get; set; }
        public virtual DbSet<PassportType> PassportTypes { get; set; }
        public virtual DbSet<Passports> Passports { get; set; }
        public virtual DbSet<PaymentCategory> PaymentCategories { get; set; }
        public virtual DbSet<PaymentConfig> PaymentConfigs { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentPenalty> PaymentPenalties { get; set; }
        public virtual DbSet<PersonTitles> PersonTitles { get; set; }
        public virtual DbSet<PrintQueue> PrintQueues { get; set; }
        public virtual DbSet<Profile>  Profiles { get; set; }
        public virtual DbSet<ProfileHash>  ProfileHashes { get; set; }
        public virtual DbSet<Clean.Domain.Entity.look.Province> Provinces { get; set; }
        public virtual DbSet<QualityControl> QualityControls { get; set; }
        public virtual DbSet<ResearchQueue> ResearchQueues { get; set; }
        public virtual DbSet<RequestType> RequestType { get; set; }
        public virtual DbSet<StockIn> StockIns { get; set; }
        public virtual DbSet<UserOfficePrinter> UserOfficePrinters { get; set; }

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
