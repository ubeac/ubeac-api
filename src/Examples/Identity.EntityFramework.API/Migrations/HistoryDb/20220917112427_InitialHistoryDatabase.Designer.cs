﻿// <auto-generated />
using System;
using API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Identity.EntityFramework.API.Migrations.HistoryDb
{
    [DbContext(typeof(HistoryDbContext))]
    [Migration("20220917112427_InitialHistoryDatabase")]
    partial class InitialHistoryDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("uBeac.HistoryEntity<uBeac.Identity.User>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Context")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("uBeac.HistoryEntity<uBeac.Identity.User>", b =>
                {
                    b.OwnsOne("uBeac.Identity.User", "Data", b1 =>
                        {
                            b1.Property<Guid>("HistoryEntity<User>Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("AccessFailedCount")
                                .HasColumnType("int");

                            b1.Property<string>("AuthenticatorKey")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ConcurrencyStamp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("EmailConfirmed")
                                .HasColumnType("bit");

                            b1.Property<bool>("Enabled")
                                .HasColumnType("bit");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("LastLoginAt")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("LastPasswordChangedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("LastPasswordChangedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("LastUpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("LastUpdatedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("LockoutEnabled")
                                .HasColumnType("bit");

                            b1.Property<DateTimeOffset?>("LockoutEnd")
                                .HasColumnType("datetimeoffset");

                            b1.Property<int>("LoginsCount")
                                .HasColumnType("int");

                            b1.Property<string>("NormalizedEmail")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("NormalizedUserName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PasswordHash")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("PhoneNumberConfirmed")
                                .HasColumnType("bit");

                            b1.Property<string>("SecurityStamp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("TwoFactorEnabled")
                                .HasColumnType("bit");

                            b1.Property<string>("UserName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("HistoryEntity<User>Id");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("HistoryEntity<User>Id");
                        });

                    b.Navigation("Data");
                });
#pragma warning restore 612, 618
        }
    }
}
