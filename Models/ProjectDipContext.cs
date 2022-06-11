using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectDipMVC.Models
{
    public partial class ProjectDipContext : DbContext
    {
        public ProjectDipContext()
        {
        }

        public ProjectDipContext(DbContextOptions<ProjectDipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectDescript> ProjectDescripts { get; set; } = null!;
        public virtual DbSet<SectionsProject> SectionsProjects { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //.AddJsonFile("appsettings.json")
            //.Build();
            //var cnctStr = configuration.GetConnectionString("ProjectDipDB");
            //optionsBuilder.UseSqlServer(cnctStr);
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ProjectDip;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("Project_id");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("date")
                    .HasColumnName("Date_create");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TitulFile).HasColumnName("Titul_File");

                entity.Property(e => e.TitulName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Titul_Name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_Users");
            });

            modelBuilder.Entity<ProjectDescript>(entity =>
            {
                entity.HasKey(e => e.ProjDscrptId)
                    .HasName("PK_Proj_Dscrpt");

                entity.ToTable("Project_Descript");

                entity.HasIndex(e => new { e.SectionNumber, e.ProjectId }, "UQ__Project___E1B3E8144235C593")
                    .IsUnique();

                entity.Property(e => e.ProjDscrptId).HasColumnName("Proj_Dscrpt_id");

                entity.Property(e => e.ProjectId).HasColumnName("Project_id");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Section_Name");

                entity.Property(e => e.SectionNumber).HasColumnName("Section_Number");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectDescripts)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proj_Dscrpt_Project");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectDescripts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proj_Dscrpt_Users");
            });

            modelBuilder.Entity<SectionsProject>(entity =>
            {
                entity.HasKey(e => e.SectionsId);

                entity.ToTable("Sections_Project");

                entity.HasIndex(e => new { e.NumberSections, e.ProjDscrptId }, "UQ__Sections__548CC8E751DBDC58")
                    .IsUnique();

                entity.Property(e => e.SectionsId).HasColumnName("Sections_id");

                entity.Property(e => e.FileSections).HasColumnName("File_sections");

                entity.Property(e => e.NameFileSections)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Name_File_sections");

                entity.Property(e => e.NameSections)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Name_sections");

                entity.Property(e => e.NumberSections).HasColumnName("Number_sections");

                entity.Property(e => e.ProjDscrptId).HasColumnName("Proj_Dscrpt_id");

                entity.HasOne(d => d.ProjDscrpt)
                    .WithMany(p => p.SectionsProjects)
                    .HasForeignKey(d => d.ProjDscrptId)
                    .HasConstraintName("FK_Proj_Dscrpt");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
