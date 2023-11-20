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
    internal class MockPriorityData
    {

        public static List<Priority> Items()
        {
            return new List<Priority>
            {
                new Priority { 
                    Id = 1, 
                    Name = "Tief", 
                    Days = 12 
                },
                new Priority { 
                    Id = 2, 
                    Name = "Standard", 
                    Days = 7 
                },
                new Priority { 
                    Id = 3, 
                    Name = "Express", 
                    Days = 5
                }
            };
        }
    }
}
