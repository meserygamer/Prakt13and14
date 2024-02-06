using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging;

public partial class Vorobiew2Context : DbContext
{
    public Vorobiew2Context()
    {
    }

    public Vorobiew2Context(DbContextOptions<Vorobiew2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cource> Cources { get; set; }

    public virtual DbSet<TeacherCource> TeacherCources { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    public virtual DbSet<UserGender> UserGenders { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AvaloniaPrakt13DB;Username=postgres;Password=k08760365");
    //optionsBuilder.UseNpgsql("Host=edu.pg.ngknn.local;Port=5432;Database=Vorobiew2;Username=43P;Password=444444");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cource>(entity =>
        {
            entity.HasKey(e => e.IdCource).HasName("cource_pk");

            entity.ToTable("cource", "DB");

            entity.Property(e => e.IdCource)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_cource");
            entity.Property(e => e.Cource1).HasColumnName("cource");
            entity.Property(e => e.HoursNumber).HasColumnName("hours_number");
        });

        modelBuilder.Entity<TeacherCource>(entity =>
        {
            entity.HasKey(e => e.IdRecord).HasName("teacher_cource_pk");

            entity.ToTable("teacher_cource", "DB");

            entity.Property(e => e.IdRecord)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_record");
            entity.Property(e => e.IdCource).HasColumnName("id_cource");
            entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");

            entity.HasOne(d => d.IdCourceNavigation).WithMany(p => p.TeacherCources)
                .HasForeignKey(d => d.IdCource)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_cource_fk");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.TeacherCources)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_cource_fk_1");
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("user_data_pk");

            entity.ToTable("user_data", "DB");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.Birthdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthdate");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.IdGender).HasColumnName("id_gender");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
            entity.Property(e => e.Worktime).HasColumnName("worktime");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.UserData)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_data_fk2");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.UserDatum)
                .HasForeignKey<UserDatum>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_data_fk");
        });

        modelBuilder.Entity<UserGender>(entity =>
        {
            entity.HasKey(e => e.IdGender).HasName("user_gender_pk");

            entity.ToTable("user_gender", "DB");

            entity.Property(e => e.IdGender)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_gender");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("user_login_pk");

            entity.ToTable("user_login", "DB");

            entity.Property(e => e.IdUser)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_user");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_login_fk");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("user_roles_pk");

            entity.ToTable("user_roles", "DB");

            entity.Property(e => e.IdRole)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_role");
            entity.Property(e => e.RoleName)
                .HasColumnType("character varying")
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
