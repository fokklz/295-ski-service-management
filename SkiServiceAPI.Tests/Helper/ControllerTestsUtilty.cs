using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SkiServiceAPI.Common;
using SkiServiceAPI.Controllers;
using SkiServiceAPI.Data;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Services;
using SkiServiceAPI.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests.Helper
{
    public class ControllerTestsUtilty<TC, TS> 
        where TC : ControllerBase
        where TS : class
    {
        protected Mock<ClaimsPrincipal> mockUser;
        protected IMapper mapper;

        public ControllerTestsUtilty()
        {
            // Setup AutoMapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Helper to simplify the creation of a TestContext as well as reducing the amount of code needed for each test.
        /// </summary>
        /// <param name="auth">Set as if a user is logged in</param>
        /// <param name="admin">Set user as admin, only possible if auth</param>
        /// <returns>the test context</returns>
        protected (ApplicationDBContext, TC) CreateTestContext(bool admin = false)
        {

            // Configure in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;
            var dbContext = new ApplicationDBContext(options);
            SeedDatabase(dbContext);

            // Setup User for Authorization
            mockUser = new Mock<ClaimsPrincipal>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

            if (admin)
            {
                // Setup an authenticated user
                mockUser.Setup(u => u.IsInRole(RoleNames.SuperAdmin.ToString())).Returns(true);
                mockUser.Setup(u => u.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, "1"));

                mockHttpContext.Setup(c => c.User).Returns(mockUser.Object);
            }

            // Setup OrderService & Controller
            var service = typeof(TS).GetConstructor(new Type[] { typeof(ApplicationDBContext), typeof(IMapper), typeof(IHttpContextAccessor) })
                            .Invoke(new object[] { dbContext, mapper, mockHttpContextAccessor.Object });

            // very ugly, but we need to check if the controller needs a token service
            TC controller = null;
            if (typeof(TC) == typeof(UsersController))
            {
                var mockTokenService = new Mock<ITokenService>();
                controller = typeof(TC).GetConstructor(new Type[] { typeof(TS), typeof(ITokenService) })
                            .Invoke(new object[] { service, mockTokenService.Object }) as TC;
            }
            else
            {
                controller = typeof(TC).GetConstructor(new Type[] { typeof(TS) })
                            .Invoke(new object[] { service }) as TC;
            }
            

            return (dbContext, controller);
        }

        protected void SeedDatabase(ApplicationDBContext dbContext)
        {
            dbContext.Users.AddRange(MockUserData.Items());
            dbContext.States.AddRange(MockStateData.Items());
            dbContext.Services.AddRange(MockServiceData.Items());
            dbContext.Priorities.AddRange(MockPriorityData.Items());
            dbContext.Orders.AddRange(MockOrderData.Items());
            dbContext.SaveChanges();
        }
    }
}
