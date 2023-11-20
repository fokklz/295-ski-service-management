using Microsoft.EntityFrameworkCore;
using Moq;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests.MockData
{
    internal class MockStateData
    {

        public static List<State> Items()
        {
            return new List<State>
            {
                new State {
                    Id = 1,
                    Name = "Offen"
                },
                new State {
                    Id = 2,
                    Name = "InArbeit"
                },
                new State {
                    Id = 3,
                    Name = "Abgeschlossen"
                }
            };
        }

        public static CreateStateRequest NewItem()
        {
            var model = new CreateStateRequest
            {
                Name = "Test",
            };

            return model;
        }
    }
}
