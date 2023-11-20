using Microsoft.EntityFrameworkCore;
using Moq;
using SkiServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests.MockData
{
    internal class MockServiceData
    {

        public static List<Service> Items()
        {
            return new List<Service>
            {
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
            };
        }
    }
}
