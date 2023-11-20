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
    internal class MockOrderData
    {
        public static List<Order> Items()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    ServiceId = 3,
                    PriorityId = 1,
                    StateId = 2,
                    UserId = 7,
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
                    UserId = 4,
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
                    UserId = 9,
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
                    UserId = 2,
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
            };
        }
    
        /// <summary>
        /// Helper to create a new OrderRequest
        /// </summary>
        /// <param name="userid">The user ID</param>
        /// <param name="note">The Note</param>
        /// <returns>a Create request model</returns>
        public static CreateOrderRequest NewItem(int? userid = null, string? note = null)
        {
            var model = new CreateOrderRequest
            {
                ServiceId = 1,
                PriorityId = 1,
                StateId = 1,
                Email = "example@gmail.com",
                Name = "John Doe",
                Phone = "+15703464001"
            };

            if (userid != null)
            {
                model.UserId = userid;
            }

            if (note != null)
            {
                model.Note = note;
            }


            return model;
        }
    }
}
