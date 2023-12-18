﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sucre_DataAccess.Data;

#nullable disable

namespace Sucre_DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231121200445_CreateDB")]
    partial class CreateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppRoleAppUser", b =>
                {
                    b.Property<Guid>("AppRolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppRolesId", "AppUsersId");

                    b.HasIndex("AppUsersId");

                    b.ToTable("AppRoleAppUser");
                });

            modelBuilder.Entity("CanalPoint", b =>
                {
                    b.Property<int>("CanalsId")
                        .HasColumnType("int");

                    b.Property<int>("PointsId")
                        .HasColumnType("int");

                    b.HasKey("CanalsId", "PointsId");

                    b.HasIndex("PointsId");

                    b.ToTable("CanalPoint");
                });

            modelBuilder.Entity("GroupUserReport", b =>
                {
                    b.Property<int>("GroupUsersId")
                        .HasColumnType("int");

                    b.Property<int>("ReportsId")
                        .HasColumnType("int");

                    b.HasKey("GroupUsersId", "ReportsId");

                    b.HasIndex("ReportsId");

                    b.ToTable("GroupUserReport");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.AsPaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("A_High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("A_HighEin")
                        .HasColumnType("bit");

                    b.Property<bool>("A_HighType")
                        .HasColumnType("bit");

                    b.Property<decimal?>("A_Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("A_LowEin")
                        .HasColumnType("bit");

                    b.Property<bool>("A_LowType")
                        .HasColumnType("bit");

                    b.Property<int?>("CanalId")
                        .HasColumnType("int");

                    b.Property<decimal?>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("W_High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("W_HighEin")
                        .HasColumnType("bit");

                    b.Property<bool>("W_HighType")
                        .HasColumnType("bit");

                    b.Property<decimal?>("W_Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("W_LowEin")
                        .HasColumnType("bit");

                    b.Property<bool>("W_LowType")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CanalId")
                        .IsUnique()
                        .HasFilter("[CanalId] IS NOT NULL");

                    b.ToTable("AsPazs");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Canal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AsPazEin")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("ParameterTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("Reader")
                        .HasColumnType("bit");

                    b.Property<int>("SourceType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParameterTypeId");

                    b.ToTable("Canals");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Cex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CexName")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Device")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Location")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Management")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("Id");

                    b.ToTable("Cexs");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Energy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EnergyName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Enegies");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.GroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ParameterType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Mnemo")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("UnitMeas")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.ToTable("ParameterTypes");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CexId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("EnergyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ServiceStaff")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CexId");

                    b.HasIndex("EnergyId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ValueDay", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Changed")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "Date");

                    b.ToTable("ValuesDay");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ValueHour", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<bool>("Changed")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "Date", "Hour");

                    b.ToTable("ValuesHour");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ValueMounth", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Changed")
                        .HasColumnType("bit");

                    b.Property<bool>("PlanFact")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "Date");

                    b.ToTable("ValuesMounth");
                });

            modelBuilder.Entity("AppRoleAppUser", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("AppRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("AppUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CanalPoint", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.Canal", null)
                        .WithMany()
                        .HasForeignKey("CanalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entities.Point", null)
                        .WithMany()
                        .HasForeignKey("PointsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupUserReport", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.GroupUser", null)
                        .WithMany()
                        .HasForeignKey("GroupUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entities.Report", null)
                        .WithMany()
                        .HasForeignKey("ReportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.AppUser", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.GroupUser", "GroupUser")
                        .WithMany("AppUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupUser");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.AsPaz", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.Canal", "Canal")
                        .WithOne("AsPaz")
                        .HasForeignKey("Sucre_DataAccess.Entities.AsPaz", "CanalId");

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Canal", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.ParameterType", "ParameterType")
                        .WithMany("Canals")
                        .HasForeignKey("ParameterTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParameterType");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Point", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.Cex", "Cex")
                        .WithMany("Points")
                        .HasForeignKey("CexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entities.Energy", "Energy")
                        .WithMany("Points")
                        .HasForeignKey("EnergyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cex");

                    b.Navigation("Energy");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ValueDay", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.Canal", "Canal")
                        .WithMany("ValueDays")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ValueHour", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.Canal", "Canal")
                        .WithMany("ValueHours")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ValueMounth", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entities.Canal", "Canal")
                        .WithMany("ValueMounths")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Canal", b =>
                {
                    b.Navigation("AsPaz");

                    b.Navigation("ValueDays");

                    b.Navigation("ValueHours");

                    b.Navigation("ValueMounths");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Cex", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.Energy", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.GroupUser", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entities.ParameterType", b =>
                {
                    b.Navigation("Canals");
                });
#pragma warning restore 612, 618
        }
    }
}
