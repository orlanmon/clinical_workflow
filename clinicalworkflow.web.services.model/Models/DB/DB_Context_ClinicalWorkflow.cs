using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace clinicalworkflow.web.services.model.Models.DB
{

    



    public partial class DB_Context_ClinicalWorkflow : DbContext
    {

        string _DataBaseConnection = "";

        
        public DB_Context_ClinicalWorkflow(string DataBaseConnection)
        {
            _DataBaseConnection = DataBaseConnection;
        }
        

        public DB_Context_ClinicalWorkflow()
        {

        }

        public DB_Context_ClinicalWorkflow(DbContextOptions<DB_Context_ClinicalWorkflow> options)
            : base(options)
        {
        }

        public virtual DbSet<ClinicalDataFieldFourLookup> ClinicalDataFieldFourLookups { get; set; }
        public virtual DbSet<ClinicalDataFieldSixLookup> ClinicalDataFieldSixLookups { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientClinicalData> PatientClinicalData { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_DataBaseConnection);
                optionsBuilder.UseSqlServer("Server = HAL9000\\ORLANMON; Database = ClinicalWorkflow; User ID = sa; Password = GoWestYoungMan_1973;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ClinicalDataFieldFourLookup>(entity =>
            {
                entity.HasKey(e => e.ClinicalDataFieldFourId);

                entity.ToTable("clinical_data_field_four_lookup");

                entity.Property(e => e.ClinicalDataFieldFourId).HasColumnName("clinical_data_field_four_id");

                entity.Property(e => e.ClinicalDataFieldFourValue)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("clinical_data_field_four_value");
            });

            modelBuilder.Entity<ClinicalDataFieldSixLookup>(entity =>
            {
                entity.HasKey(e => e.ClinicalDataFieldSixId);

                entity.ToTable("clinical_data_field_six_lookup");

                entity.Property(e => e.ClinicalDataFieldSixId).HasColumnName("clinical_data_field_six_id");

                entity.Property(e => e.ClinicalDataFieldSixValue)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("clinical_data_field_six_value");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.AddressOne)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address_one");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("state");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("zip");
            });

            modelBuilder.Entity<PatientClinicalData>(entity =>
            {
               
                entity.HasKey(e => e.PatientClinicalDataId);

                entity.ToTable("Patient_Clinical_Data");

                entity.Property(e => e.ClinicalDataFieldFive)
                    .HasMaxLength(10)
                    .HasColumnName("clinical_data_field_five");

                entity.Property(e => e.ClinicalDataFieldFour).HasColumnName("clinical_data_field_four");

                entity.Property(e => e.ClinicalDataFieldOne)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("clinical_data_field_one");

                entity.Property(e => e.ClinicalDataFieldSeven).HasColumnName("clinical_data_field_seven");

                entity.Property(e => e.ClinicalDataFieldSix).HasColumnName("clinical_data_field_six");

                entity.Property(e => e.ClinicalDataFieldThree)
                    .HasMaxLength(10)
                    .HasColumnName("clinical_data_field_three")
                    .IsFixedLength(true);

                entity.Property(e => e.ClinicalDataFieldTwo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("clinical_data_field_two");

                entity.Property(e => e.PatientClinicalDataId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("patient_clinical_data_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.ClinicalDataFieldFourNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ClinicalDataFieldFour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Clinical_Data_clinical_data_field_four_lookup");

                entity.HasOne(d => d.ClinicalDataFieldSixNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ClinicalDataFieldSix)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Clinical_Data_clinical_data_field_six_lookup");

                entity.HasOne(d => d.Patient)
                    .WithMany()
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Clinical_Data_Patient");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("User_Login");

                entity.Property(e => e.UserLoginId).HasColumnName("user_login_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
