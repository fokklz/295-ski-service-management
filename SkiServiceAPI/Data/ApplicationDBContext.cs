using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Data
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Username as unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Convert role enum to string
            modelBuilder
                .Entity<User>()
                .Property(e => e.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Kleiner Service",
                    Description = "Belag-Vorschliff und Belag-Strukturschliff für Ski, einschließlich Plan-Schliff, Diamant-Steinschliff und Wachsen & Polieren für optimale Gleitfähigkeit und Steuerung auf verschiedenen Schneebedingungen.",
                    Price = 49
                },
                new Service
                {
                    Id = 2,
                    Name = "Grosser Service",
                    Description = "Planschleifen für präzise ebene Ski-Kanten und Belag, maschinelles Kanten-Schleifen für scharfe Kanten, Belagaufbesserung, Belag-Vorschliff, Belag-Strukturschliff und Entgraten zur Optimierung von Gleitverhalten, Lenkung und Haltbarkeit der Ski.",
                    Price = 69
                },
                new Service
                {
                    Id = 3,
                    Name = "Rennski-Service",
                    Description = "Planschleifen, maschinellem Kanten-Schleifen und Belagaufbesserung für maximale Geschwindigkeit und Präzision. Fortschrittliches Weltcup-Wachs für höchste Gleiteigenschaften und Langlebigkeit. Entgraten und Handfinish für optimale Skioberfläche und Wettkampfqualität.",
                    Price = 99
                },
                new Service
                {
                    Id = 4,
                    Name = "Bindung montieren und einstellen",
                    Description = "Professionelle Montage und Einstellung von Ski-Bindungen durch Expertenteam für maximale Sicherheit und Performance. Präzise Montage und individuelle Einstellung für optimalen Halt, schnelles Ansprechen bei Stürzen und Anpassung an den Fahrstil.",
                    Price = 39
                },
                new Service
                {
                    Id = 5,
                    Name = "Fell zuschneiden pro Paar",
                    Description = "Maßgeschneiderte Skitouren-Felle, angepasst durch unser Fachteam für optimalen Grip und geschmeidiges Gleiten. Sorgfältiges Zuschneiden gewährleistet Effizienz und Komfort bei Skitouren.",
                    Price = 25
                },
                new Service
                {
                    Id = 6,
                    Name = "Heisswachsen",
                    Description = "bietet tiefe Wachsimprägnierung für herausragendes Gleiterlebnis. Verfügbar als eigenständige Dienstleistung zur optimalen Vorbereitung der Skier für maximale Performance bei jedem Abfahrtslauf.",
                    Price = 18
                }
            );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "Tief", Days = 12 },
                new Priority { Id = 2, Name = "Standard", Days = 7 },
                new Priority { Id = 3, Name = "Express", Days = 5 }
            );

            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Offen" },
                new State { Id = 2, Name = "InArbeit" },
                new State { Id = 3, Name = "Abgeschlossen" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    ServiceId = 3,
                    PriorityId = 1,
                    StateId = 2,
                    Name = "Alice Johnson",
                    Email = "alice.johnson@example.com",
                    Phone = "+15703464001",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 2,
                    ServiceId = 1,
                    PriorityId = 2,
                    StateId = 3,
                    Name = "Bob Smith",
                    Email = "bob.smith@example.com",
                    Phone = "+15703464002",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 3,
                    ServiceId = 5,
                    PriorityId = 3,
                    StateId = 1,
                    Name = "Carol White",
                    Email = "carol.white@example.com",
                    Phone = "+15703464003",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 4,
                    ServiceId = 2,
                    PriorityId = 1,
                    StateId = 2,
                    Name = "David Green",
                    Email = "david.green@example.com",
                    Phone = "+15703464004",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 5,
                    ServiceId = 4,
                    PriorityId = 2,
                    StateId = 3,
                    Name = "Evelyn Harris",
                    Email = "evelyn.harris@example.com",
                    Phone = "+15703464005",
                    Note = "Check for additional details",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 6,
                    ServiceId = 6,
                    PriorityId = 3,
                    StateId = 1,
                    Name = "Frank Miller",
                    Email = "frank.miller@example.com",
                    Phone = "+15703464006",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 7,
                    ServiceId = 1,
                    PriorityId = 1,
                    StateId = 2,
                    Name = "Grace Lee",
                    Email = "grace.lee@example.com",
                    Phone = "+15703464007",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 8,
                    ServiceId = 3,
                    PriorityId = 2,
                    StateId = 3,
                    Name = "Henry Wilson",
                    Email = "henry.wilson@example.com",
                    Phone = "+15703464008",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 9,
                    ServiceId = 5,
                    PriorityId = 3,
                    StateId = 1,
                    Name = "Irene Taylor",
                    Email = "irene.taylor@example.com",
                    Phone = "+15703464009",
                    Note = "Requires immediate attention",
                    Created = DateTime.Now,
                    IsDeleted = false
                },
                new Order
                {
                    Id = 10,
                    ServiceId = 2,
                    PriorityId = 1,
                    StateId = 2,
                    Name = "Jason Brown",
                    Email = "jason.brown@example.com",
                    Phone = "+15703464010",
                    Created = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}

