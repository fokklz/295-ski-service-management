﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkiServiceAPI.Data;

#nullable disable

namespace SkiServiceAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231121193850_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkiServiceAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StateId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7410),
                            Email = "alice.johnson@example.com",
                            IsDeleted = false,
                            Name = "Alice Johnson",
                            Phone = "+15703464001",
                            PriorityId = 1,
                            ServiceId = 3,
                            StateId = 2
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7414),
                            Email = "bob.smith@example.com",
                            IsDeleted = false,
                            Name = "Bob Smith",
                            Phone = "+15703464002",
                            PriorityId = 2,
                            ServiceId = 1,
                            StateId = 3
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7418),
                            Email = "carol.white@example.com",
                            IsDeleted = false,
                            Name = "Carol White",
                            Phone = "+15703464003",
                            PriorityId = 3,
                            ServiceId = 5,
                            StateId = 1
                        },
                        new
                        {
                            Id = 4,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7421),
                            Email = "david.green@example.com",
                            IsDeleted = false,
                            Name = "David Green",
                            Phone = "+15703464004",
                            PriorityId = 1,
                            ServiceId = 2,
                            StateId = 2
                        },
                        new
                        {
                            Id = 5,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7425),
                            Email = "evelyn.harris@example.com",
                            IsDeleted = false,
                            Name = "Evelyn Harris",
                            Note = "Check for additional details",
                            Phone = "+15703464005",
                            PriorityId = 2,
                            ServiceId = 4,
                            StateId = 3
                        },
                        new
                        {
                            Id = 6,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7428),
                            Email = "frank.miller@example.com",
                            IsDeleted = false,
                            Name = "Frank Miller",
                            Phone = "+15703464006",
                            PriorityId = 3,
                            ServiceId = 6,
                            StateId = 1
                        },
                        new
                        {
                            Id = 7,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7431),
                            Email = "grace.lee@example.com",
                            IsDeleted = false,
                            Name = "Grace Lee",
                            Phone = "+15703464007",
                            PriorityId = 1,
                            ServiceId = 1,
                            StateId = 2
                        },
                        new
                        {
                            Id = 8,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7435),
                            Email = "henry.wilson@example.com",
                            IsDeleted = false,
                            Name = "Henry Wilson",
                            Phone = "+15703464008",
                            PriorityId = 2,
                            ServiceId = 3,
                            StateId = 3
                        },
                        new
                        {
                            Id = 9,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7439),
                            Email = "irene.taylor@example.com",
                            IsDeleted = false,
                            Name = "Irene Taylor",
                            Note = "Requires immediate attention",
                            Phone = "+15703464009",
                            PriorityId = 3,
                            ServiceId = 5,
                            StateId = 1
                        },
                        new
                        {
                            Id = 10,
                            Created = new DateTime(2023, 11, 21, 20, 38, 50, 450, DateTimeKind.Local).AddTicks(7442),
                            Email = "jason.brown@example.com",
                            IsDeleted = false,
                            Name = "Jason Brown",
                            Phone = "+15703464010",
                            PriorityId = 1,
                            ServiceId = 2,
                            StateId = 2
                        });
                });

            modelBuilder.Entity("SkiServiceAPI.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Priorities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Days = 12,
                            IsDeleted = false,
                            Name = "Tief"
                        },
                        new
                        {
                            Id = 2,
                            Days = 7,
                            IsDeleted = false,
                            Name = "Standard"
                        },
                        new
                        {
                            Id = 3,
                            Days = 5,
                            IsDeleted = false,
                            Name = "Express"
                        });
                });

            modelBuilder.Entity("SkiServiceAPI.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Belag-Vorschliff und Belag-Strukturschliff für Ski, einschließlich Plan-Schliff, Diamant-Steinschliff und Wachsen & Polieren für optimale Gleitfähigkeit und Steuerung auf verschiedenen Schneebedingungen.",
                            IsDeleted = false,
                            Name = "Kleiner Service",
                            Price = 49
                        },
                        new
                        {
                            Id = 2,
                            Description = "Planschleifen für präzise ebene Ski-Kanten und Belag, maschinelles Kanten-Schleifen für scharfe Kanten, Belagaufbesserung, Belag-Vorschliff, Belag-Strukturschliff und Entgraten zur Optimierung von Gleitverhalten, Lenkung und Haltbarkeit der Ski.",
                            IsDeleted = false,
                            Name = "Grosser Service",
                            Price = 69
                        },
                        new
                        {
                            Id = 3,
                            Description = "Planschleifen, maschinellem Kanten-Schleifen und Belagaufbesserung für maximale Geschwindigkeit und Präzision. Fortschrittliches Weltcup-Wachs für höchste Gleiteigenschaften und Langlebigkeit. Entgraten und Handfinish für optimale Skioberfläche und Wettkampfqualität.",
                            IsDeleted = false,
                            Name = "Rennski-Service",
                            Price = 99
                        },
                        new
                        {
                            Id = 4,
                            Description = "Professionelle Montage und Einstellung von Ski-Bindungen durch Expertenteam für maximale Sicherheit und Performance. Präzise Montage und individuelle Einstellung für optimalen Halt, schnelles Ansprechen bei Stürzen und Anpassung an den Fahrstil.",
                            IsDeleted = false,
                            Name = "Bindung montieren und einstellen",
                            Price = 39
                        },
                        new
                        {
                            Id = 5,
                            Description = "Maßgeschneiderte Skitouren-Felle, angepasst durch unser Fachteam für optimalen Grip und geschmeidiges Gleiten. Sorgfältiges Zuschneiden gewährleistet Effizienz und Komfort bei Skitouren.",
                            IsDeleted = false,
                            Name = "Fell zuschneiden pro Paar",
                            Price = 25
                        },
                        new
                        {
                            Id = 6,
                            Description = "bietet tiefe Wachsimprägnierung für herausragendes Gleiterlebnis. Verfügbar als eigenständige Dienstleistung zur optimalen Vorbereitung der Skier für maximale Performance bei jedem Abfahrtslauf.",
                            IsDeleted = false,
                            Name = "Heisswachsen",
                            Price = 18
                        });
                });

            modelBuilder.Entity("SkiServiceAPI.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Offen"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "InArbeit"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Abgeschlossen"
                        });
                });

            modelBuilder.Entity("SkiServiceAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<int>("LoginAttempts")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SkiServiceAPI.Models.Order", b =>
                {
                    b.HasOne("SkiServiceAPI.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkiServiceAPI.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkiServiceAPI.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkiServiceAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Priority");

                    b.Navigation("Service");

                    b.Navigation("State");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
