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
    [Migration("20230519031728_DelVMs")]
    partial class DelVMs
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

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.ToTable("datacenters");
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

            modelBuilder.Entity("infrastracture_api.Models.Host", b =>
                {
                    b.HasOne("infrastracture_api.Models.HostType", "Type")
                        .WithMany("Hosts")
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("infrastracture_api.Models.HostType", b =>
                {
                    b.Navigation("Hosts");
                });
#pragma warning restore 612, 618
        }
    }
}
