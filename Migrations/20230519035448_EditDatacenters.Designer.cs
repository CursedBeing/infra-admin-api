﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using infrastracture_api;

#nullable disable

namespace infrastracture_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230519035448_EditDatacenters")]
    partial class EditDatacenters
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("infrastracture_api.Models.Datacenter.Datacenter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ContactEmail")
                        .HasColumnType("text");

                    b.Property<string>("ContactName")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("text");

                    b.Property<string>("ContactSite")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsExternal")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("datacenters");
                });

            modelBuilder.Entity("infrastracture_api.Models.Datacenter.HvHostDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DatacenterId")
                        .HasColumnType("bigint");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("text");

                    b.Property<string>("MgmtIpAddress")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<string>("OsType")
                        .HasColumnType("text");

                    b.Property<string>("OsVersion")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DatacenterId");

                    b.ToTable("hypervisors");
                });

            modelBuilder.Entity("infrastracture_api.Models.Datacenter.NetworkDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DatacenterId")
                        .HasColumnType("bigint");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("text");

                    b.Property<string>("MgmtIpAddress")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<int?>("PortCount")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DatacenterId");

                    b.ToTable("netdevices");
                });

            modelBuilder.Entity("infrastracture_api.Models.Host", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Domain")
                        .HasColumnType("text");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<bool>("MonitorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("TelegrafPort")
                        .HasColumnType("text");

                    b.Property<long?>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Hosts");
                });

            modelBuilder.Entity("infrastracture_api.Models.HostType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HostTypes");
                });

            modelBuilder.Entity("infrastracture_api.Models.Datacenter.HvHostDevice", b =>
                {
                    b.HasOne("infrastracture_api.Models.Datacenter.Datacenter", "Datacenter")
                        .WithMany("Hypervisors")
                        .HasForeignKey("DatacenterId");

                    b.Navigation("Datacenter");
                });

            modelBuilder.Entity("infrastracture_api.Models.Datacenter.NetworkDevice", b =>
                {
                    b.HasOne("infrastracture_api.Models.Datacenter.Datacenter", "Datacenter")
                        .WithMany("NetDevices")
                        .HasForeignKey("DatacenterId");

                    b.Navigation("Datacenter");
                });

            modelBuilder.Entity("infrastracture_api.Models.Host", b =>
                {
                    b.HasOne("infrastracture_api.Models.HostType", "Type")
                        .WithMany("Hosts")
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("infrastracture_api.Models.Datacenter.Datacenter", b =>
                {
                    b.Navigation("Hypervisors");

                    b.Navigation("NetDevices");
                });

            modelBuilder.Entity("infrastracture_api.Models.HostType", b =>
                {
                    b.Navigation("Hosts");
                });
#pragma warning restore 612, 618
        }
    }
}
