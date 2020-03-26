using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Persistence.NModels
{
    public partial class MPRSContext : DbContext
    {
        public MPRSContext()
        {
        }

        public MPRSContext(DbContextOptions<MPRSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AttachmentType> AttachmentType { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<AuthorizationQueue> AuthorizationQueue { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BioData> BioData { get; set; }
        public virtual DbSet<Biometric> Biometric { get; set; }
        public virtual DbSet<BiometricDetail> BiometricDetail { get; set; }
        public virtual DbSet<BiometricType> BiometricType { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CrimeType> CrimeType { get; set; }
        public virtual DbSet<CriminalRecord> CriminalRecord { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<DiscountType> DiscountType { get; set; }
        public virtual DbSet<Discounts> Discounts { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Occupation> Occupation { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<OperationType> OperationType { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<PassportApplication> PassportApplication { get; set; }
        public virtual DbSet<PassportDuration> PassportDuration { get; set; }
        public virtual DbSet<PassportPrint> PassportPrint { get; set; }
        public virtual DbSet<PassportType> PassportType { get; set; }
        public virtual DbSet<Passports> Passports { get; set; }
        public virtual DbSet<PaymentCategory> PaymentCategory { get; set; }
        public virtual DbSet<PaymentConfig> PaymentConfig { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<PaymentPenalty> PaymentPenalty { get; set; }
        public virtual DbSet<PersonTitles> PersonTitles { get; set; }
        public virtual DbSet<PrintQueue> PrintQueue { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessConnection> ProcessConnection { get; set; }
        public virtual DbSet<ProcessTracking> ProcessTracking { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileHash> ProfileHash { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<QualityControl> QualityControl { get; set; }
        public virtual DbSet<RequestType> RequestType { get; set; }
        public virtual DbSet<ResearchQueue> ResearchQueue { get; set; }
        public virtual DbSet<RoleScreen> RoleScreen { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<ScreenDocument> ScreenDocument { get; set; }
        public virtual DbSet<StockIn> StockIn { get; set; }
        public virtual DbSet<SystemStatus> SystemStatus { get; set; }
        public virtual DbSet<UserOfficePrinter> UserOfficePrinter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost; Database=MPRS; Username=postgres; Password=qwer;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
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

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_fk");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_dist_fk");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_fk_2");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_fk_1");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("NameEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.HasIndex(e => e.OfficeId);

                entity.HasIndex(e => e.OrganizationId);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.OfficeId);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.OrganizationId);
            });

            modelBuilder.Entity<AttachmentType>(entity =>
            {
                entity.ToTable("AttachmentType", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code).HasColumnType("character varying");

                entity.Property(e => e.Title).HasColumnType("character varying");
            });

            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.ToTable("Attachments", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AttachmentTypeId).HasColumnName("AttachmentTypeID");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.DocumentDate).HasColumnType("date");

                entity.Property(e => e.DocumentNumber).HasColumnType("character varying");

                entity.Property(e => e.EncryptionKey).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.AttachmentType)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AttachmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attachments_fk_1");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attachments_fk");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DbContextObject).HasMaxLength(100);

                entity.Property(e => e.DbObjectName).HasMaxLength(100);

                entity.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValueAfter).HasColumnType("character varying");

                entity.Property(e => e.ValueBefore).HasColumnType("character varying");

                entity.HasOne(d => d.OperationType)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.OperationTypeId)
                    .HasConstraintName("audit_fk");
            });

            modelBuilder.Entity<AuthorizationQueue>(entity =>
            {
                entity.ToTable("AuthorizationQueue", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AssignedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ProcessedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AuthorizationQueue)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_AuthorizationQueue__FK");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<BioData>(entity =>
            {
                entity.ToTable("BioData", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp(6) with time zone");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasColumnType("character varying");

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.FamilyNameEn)
                    .IsRequired()
                    .HasColumnName("FamilyNameEN")
                    .HasColumnType("character varying");

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.FatherNameEn)
                    .IsRequired()
                    .HasColumnName("FatherNameEN")
                    .HasColumnType("character varying");

                entity.Property(e => e.GrandFatherName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.GrandFatherNameEn)
                    .IsRequired()
                    .HasColumnName("GrandFatherNameEN")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("NameEN")
                    .HasColumnType("character varying");

                entity.Property(e => e.PhoneNumber).HasColumnType("character varying");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.BioData)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("biodata_fk");
            });

            modelBuilder.Entity<Biometric>(entity =>
            {
                entity.ToTable("Biometric", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BiometricTypeId).HasColumnName("BiometricTypeID");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp(0) with time zone");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp(0) with time zone");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.HasOne(d => d.BiometricType)
                    .WithMany(p => p.Biometric)
                    .HasForeignKey(d => d.BiometricTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("biometric_bt_fk");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Biometric)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("biometric_fk");
            });

            modelBuilder.Entity<BiometricDetail>(entity =>
            {
                entity.ToTable("BiometricDetail", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BiometricCode)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.BiometricId).HasColumnName("BiometricID");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Biometric)
                    .WithMany(p => p.BiometricDetail)
                    .HasForeignKey(d => d.BiometricId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("biometricdetail_fk");
            });

            modelBuilder.Entity<BiometricType>(entity =>
            {
                entity.ToTable("BiometricType", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ColorType)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("NameEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<CrimeType>(entity =>
            {
                entity.ToTable("CrimeType", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<CriminalRecord>(entity =>
            {
                entity.ToTable("CriminalRecord", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.CrimeTypeId).HasColumnName("CrimeTypeID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

                entity.HasOne(d => d.CrimeType)
                    .WithMany(p => p.CriminalRecord)
                    .HasForeignKey(d => d.CrimeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("criminalrecord_ct_fk");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.CriminalRecord)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("criminalrecord_fk");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<DiscountType>(entity =>
            {
                entity.ToTable("DiscountType", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Discounts>(entity =>
            {
                entity.ToTable("Discounts", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ActiveFrom).HasColumnType("date");

                entity.Property(e => e.ActiveTo).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.HasOne(d => d.DiscountType)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.DiscountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_Discounts__FK");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discounts_office_fk");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_District__FK");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.ToTable("Documents", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DocumentDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.DocumentNumber).HasMaxLength(100);

                entity.Property(e => e.DocumentSource).HasMaxLength(100);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.EncryptionKey).HasMaxLength(500);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastDownloadDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectName).HasMaxLength(100);

                entity.Property(e => e.ObjectSchema).HasMaxLength(100);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Root).HasMaxLength(200);

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UploadDate).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_Documents__FK");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
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

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

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

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_job_organization");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_fk");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("MaritalStatus", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("Occupation", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Occupation)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("occupation_fk");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Office", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.OfficeTypeId).HasColumnName("OfficeTypeID");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Office)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("office_fk");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Office)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("office_fk_2");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Office)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("office_fk_1");
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.ToTable("OperationType", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperationTypeName).HasColumnType("character varying");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization", "Look");
            });

            modelBuilder.Entity<PassportApplication>(entity =>
            {
                entity.ToTable("PassportApplication", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ActiveAddressId).HasColumnName("ActiveAddressID");

                entity.Property(e => e.ActiveBioDataId).HasColumnName("ActiveBioDataID");

                entity.Property(e => e.ActiveJobId).HasColumnName("ActiveJobID");

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.CurProcessId).HasColumnName("CurProcessID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.PassportDurationId).HasColumnName("PassportDurationID");

                entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

                entity.Property(e => e.PaymentCategoryId).HasColumnName("PaymentCategoryID");

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.PaymentPenaltyId).HasColumnName("PaymentPenaltyID");

                entity.Property(e => e.PhotoPath).HasColumnType("character varying");

                entity.Property(e => e.Prefix).HasColumnType("character varying");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.ReceiptNumer).HasMaxLength(50);

                entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

                entity.Property(e => e.RequestTypeId).HasColumnName("RequestTypeID");

                entity.Property(e => e.SignaturePath).HasColumnType("character varying");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.ActiveAddress)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.ActiveAddressId)
                    .HasConstraintName("passportapplication_aadr_fk");

                entity.HasOne(d => d.ActiveBioData)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.ActiveBioDataId)
                    .HasConstraintName("passportapplication_abid_fk");

                entity.HasOne(d => d.ActiveJob)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.ActiveJobId)
                    .HasConstraintName("passportapplication_acjb_fk_1");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("passportapplication_bank_fk");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("passportapplication_discount_fk");

                entity.HasOne(d => d.PassportDuration)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.PassportDurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_pd_fk");

                entity.HasOne(d => d.PassportType)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.PassportTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_fk");

                entity.HasOne(d => d.PaymentCategory)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.PaymentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_pycat_fk");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_pymtd_fk");

                entity.HasOne(d => d.PaymentPenalty)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.PaymentPenaltyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_pypen_fk");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_prf_fk");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.PassportApplication)
                    .HasForeignKey(d => d.RequestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passportapplication_reqtp_fk");
            });

            modelBuilder.Entity<PassportDuration>(entity =>
            {
                entity.ToTable("PassportDuration", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

                entity.HasOne(d => d.PassportType)
                    .WithMany(p => p.PassportDuration)
                    .HasForeignKey(d => d.PassportTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PassportDuration__FK");
            });

            modelBuilder.Entity<PassportPrint>(entity =>
            {
                entity.ToTable("PassportPrint", "prt");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedOn)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PassportId).HasColumnName("PassportID");

                entity.Property(e => e.PrintQueueId).HasColumnName("PrintQueueID");

                entity.Property(e => e.PrintedDate).HasColumnType("date");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.Passport)
                    .WithMany(p => p.PassportPrint)
                    .HasForeignKey(d => d.PassportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PassportPrint__FK_1");

                entity.HasOne(d => d.PrintQueue)
                    .WithMany(p => p.PassportPrint)
                    .HasForeignKey(d => d.PrintQueueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PassportPrint__FK");
            });

            modelBuilder.Entity<PassportType>(entity =>
            {
                entity.ToTable("PassportType", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Passports>(entity =>
            {
                entity.ToTable("Passports", "stc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.PassportNumber).HasColumnType("character varying");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StockInId).HasColumnName("StockInID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.Passports)
                    .HasForeignKey(d => d.StockInId)
                    .HasConstraintName("_Passports__FK");
            });

            modelBuilder.Entity<PaymentCategory>(entity =>
            {
                entity.ToTable("PaymentCategory", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PaymentConfig>(entity =>
            {
                entity.ToTable("PaymentConfig", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.PassportDurationId).HasColumnName("PassportDurationID");

                entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

                entity.Property(e => e.PaymentCategoryId).HasColumnName("PaymentCategoryID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.PassportDuration)
                    .WithMany(p => p.PaymentConfig)
                    .HasForeignKey(d => d.PassportDurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PaymentConfig__FK_1");

                entity.HasOne(d => d.PassportType)
                    .WithMany(p => p.PaymentConfig)
                    .HasForeignKey(d => d.PassportTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PaymentConfig__FK");

                entity.HasOne(d => d.PaymentCategory)
                    .WithMany(p => p.PaymentConfig)
                    .HasForeignKey(d => d.PaymentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PaymentConfig__FK_2");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PaymentPenalty>(entity =>
            {
                entity.ToTable("PaymentPenalty", "pas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.PaymentPenalty)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paymentpenalty_fk");
            });

            modelBuilder.Entity<PersonTitles>(entity =>
            {
                entity.ToTable("PersonTitles", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("NameEN")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PrintQueue>(entity =>
            {
                entity.ToTable("PrintQueue", "prt");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ProcessedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.PrintQueue)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_PrintQueue__FK");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Process", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.Property(e => e.Sorter).HasMaxLength(10);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.Process)
                    .HasForeignKey(d => d.ScreenId)
                    .HasConstraintName("_Process__FK");
            });

            modelBuilder.Entity<ProcessConnection>(entity =>
            {
                entity.ToTable("ProcessConnection", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.HasOne(d => d.ConnectedToNavigation)
                    .WithMany(p => p.ProcessConnectionConnectedToNavigation)
                    .HasForeignKey(d => d.ConnectedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessConnection__FK_1");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessConnectionProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessConnection__FK");
            });

            modelBuilder.Entity<ProcessTracking>(entity =>
            {
                entity.ToTable("ProcessTracking", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ReferedProcessId).HasColumnName("ReferedProcessID");

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK_2");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile", "prf");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BirthCountryId).HasColumnName("BirthCountryID");

                entity.Property(e => e.BirthProvinceId).HasColumnName("BirthProvinceID");

                entity.Property(e => e.Code).HasColumnType("character varying");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.EyeColorId).HasColumnName("EyeColorID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.HairColorId).HasColumnName("HairColorID");

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasColumnName("NationalID")
                    .HasColumnType("character varying");

                entity.Property(e => e.OtherDetail).HasColumnType("character varying");

                entity.Property(e => e.OtherNationalityId).HasColumnName("OtherNationalityID");

                entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

                entity.Property(e => e.ResidenceCountryId).HasColumnName("ResidenceCountryID");

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.ProfileBirthCountry)
                    .HasForeignKey(d => d.BirthCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_1");

                entity.HasOne(d => d.BirthProvince)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.BirthProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_6");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_7");

                entity.HasOne(d => d.EyeColor)
                    .WithMany(p => p.ProfileEyeColor)
                    .HasForeignKey(d => d.EyeColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_3");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_4");

                entity.HasOne(d => d.HairColor)
                    .WithMany(p => p.ProfileHairColor)
                    .HasForeignKey(d => d.HairColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_2");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk_5");

                entity.HasOne(d => d.OtherNationality)
                    .WithMany(p => p.ProfileOtherNationality)
                    .HasForeignKey(d => d.OtherNationalityId)
                    .HasConstraintName("profile_otn_fk");

                entity.HasOne(d => d.ResidenceCountry)
                    .WithMany(p => p.ProfileResidenceCountry)
                    .HasForeignKey(d => d.ResidenceCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_fk");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profile_pt_fk");
            });

            modelBuilder.Entity<ProfileHash>(entity =>
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
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province", "Look");

                entity.HasIndex(e => e.CountryId);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Province)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("province_fk");
            });

            modelBuilder.Entity<QualityControl>(entity =>
            {
                entity.ToTable("QualityControl", "qc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.PassportPrintId).HasColumnName("PassportPrintID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.PassportPrint)
                    .WithMany(p => p.QualityControl)
                    .HasForeignKey(d => d.PassportPrintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("qualitycontrol_fk");
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.ToTable("RequestType", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<ResearchQueue>(entity =>
            {
                entity.ToTable("ResearchQueue", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AssignedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ProcessedDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ResearchQueue)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ResearchQueue__FK");
            });

            modelBuilder.Entity<RoleScreen>(entity =>
            {
                entity.ToTable("RoleScreen", "Look");

                entity.HasIndex(e => e.ScreenId);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.RoleScreen)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_RoleScreen__FK");
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.ToTable("Screen", "Look");

                entity.HasIndex(e => e.ModuleId);

                entity.HasIndex(e => e.ParentId);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.DirectoryPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Screen)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("screen_fk");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("screen_parent_fk");
            });

            modelBuilder.Entity<ScreenDocument>(entity =>
            {
                entity.ToTable("ScreenDocument", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.ScreenDocument)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("_ScreenDocument__FK");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ScreenDocument)
                    .HasForeignKey(d => d.ScreenId)
                    .HasConstraintName("_ScreenDocument__FK_1");
            });

            modelBuilder.Entity<StockIn>(entity =>
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
            });

            modelBuilder.Entity<SystemStatus>(entity =>
            {
                entity.ToTable("SystemStatus", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Sorter)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StatusType)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<UserOfficePrinter>(entity =>
            {
                entity.ToTable("UserOfficePrinter", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.UserOfficePrinter)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_UserOfficePrinter__FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
