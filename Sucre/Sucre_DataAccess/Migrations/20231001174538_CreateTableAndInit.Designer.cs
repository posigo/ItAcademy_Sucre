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
    [Migration("20231001174538_CreateTableAndInit")]
    partial class CreateTableAndInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("Sucre_DataAccess.Entity.AsPaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("A_High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("A_HighEin")
                        .HasColumnType("bit");

                    b.Property<bool>("A_HighType")
                        .HasColumnType("bit");

                    b.Property<decimal>("A_Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("A_LowEin")
                        .HasColumnType("bit");

                    b.Property<bool>("A_LowType")
                        .HasColumnType("bit");

                    b.Property<int>("CanalId")
                        .HasColumnType("int");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("W_High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("W_HighEin")
                        .HasColumnType("bit");

                    b.Property<bool>("W_HighType")
                        .HasColumnType("bit");

                    b.Property<decimal>("W_Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("W_LowEin")
                        .HasColumnType("bit");

                    b.Property<bool>("W_LowType")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CanalId")
                        .IsUnique();

                    b.ToTable("AsPazs");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Canal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AsPazEin")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

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

            modelBuilder.Entity("Sucre_DataAccess.Entity.Cex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CexName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Management")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("Id");

                    b.ToTable("Cexs");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Energy", b =>
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

            modelBuilder.Entity("Sucre_DataAccess.Entity.GroupUser", b =>
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

            modelBuilder.Entity("Sucre_DataAccess.Entity.ParameterType", b =>
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

            modelBuilder.Entity("Sucre_DataAccess.Entity.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CexId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("EnergyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ServiceStaff")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CexId");

                    b.HasIndex("EnergyId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Report", b =>
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

            modelBuilder.Entity("Sucre_DataAccess.Entity.ReportDetail", b =>
                {
                    b.Property<int>("CanalId")
                        .HasColumnType("int");

                    b.Property<int>("PointId")
                        .HasColumnType("int");

                    b.Property<int>("ReportId")
                        .HasColumnType("int");

                    b.HasIndex("CanalId");

                    b.HasIndex("PointId");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportDetails");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.ValueDay", b =>
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

            modelBuilder.Entity("Sucre_DataAccess.Entity.ValueHour", b =>
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

            modelBuilder.Entity("Sucre_DataAccess.Entity.ValueMounth", b =>
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

            modelBuilder.Entity("CanalPoint", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Canal", null)
                        .WithMany()
                        .HasForeignKey("CanalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entity.Point", null)
                        .WithMany()
                        .HasForeignKey("PointsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupUserReport", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.GroupUser", null)
                        .WithMany()
                        .HasForeignKey("GroupUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entity.Report", null)
                        .WithMany()
                        .HasForeignKey("ReportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.AsPaz", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Canal", "Canal")
                        .WithOne("AsPaz")
                        .HasForeignKey("Sucre_DataAccess.Entity.AsPaz", "CanalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Canal", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.ParameterType", "ParameterType")
                        .WithMany("Canals")
                        .HasForeignKey("ParameterTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParameterType");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Point", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Cex", "Cex")
                        .WithMany("Points")
                        .HasForeignKey("CexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entity.Energy", "Energy")
                        .WithMany("Points")
                        .HasForeignKey("EnergyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cex");

                    b.Navigation("Energy");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.ReportDetail", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Canal", "Canal")
                        .WithMany()
                        .HasForeignKey("CanalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entity.Point", "Point")
                        .WithMany()
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sucre_DataAccess.Entity.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");

                    b.Navigation("Point");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.User", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.GroupUser", "GroupUser")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupUser");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.ValueDay", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Canal", "Canal")
                        .WithMany("ValueDays")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.ValueHour", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Canal", "Canal")
                        .WithMany("ValueHours")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.ValueMounth", b =>
                {
                    b.HasOne("Sucre_DataAccess.Entity.Canal", "Canal")
                        .WithMany("ValueMounths")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Canal", b =>
                {
                    b.Navigation("AsPaz");

                    b.Navigation("ValueDays");

                    b.Navigation("ValueHours");

                    b.Navigation("ValueMounths");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Cex", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.Energy", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.GroupUser", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Sucre_DataAccess.Entity.ParameterType", b =>
                {
                    b.Navigation("Canals");
                });
#pragma warning restore 612, 618
        }
    }
}
