
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SkiServiceAPI.Common;
using SkiServiceAPI.Common.OpenAPI;
using SkiServiceAPI.Data;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Services;
using SkiServiceModels.Enums;
using System.Text;

namespace SkiServiceAPI
{
    public class Program
    {
        public static TokenValidationParameters TokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                ValidAudience = configuration["JWT:Audience"],
                ValidIssuer = configuration["JWT:Issuer"],
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Setup Custom logging using Serilog
            // will rotate the logfile
            var LoggerFromSettings = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(LoggerFromSettings);

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddHttpContextAccessor();

            // Connect to default configured database
            builder.Services.AddDbContext<IApplicationDBContext, ApplicationDBContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }).UseLazyLoadingProxies());

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped(typeof(GenericService<,,,,>));
            builder.Services.AddScoped(typeof(IBaseService<,,,,>), typeof(GenericService<,,,,>));

            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var allowedOrigins = builder.Configuration["CORS:AllowedOrigins"]?.Split(',') ?? new string[0];
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Setup swagger to allow Authorization
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiService Management API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                c.OperationFilter<GenericResponseOperationFilter>();
                c.DocumentFilter<SortPathsByLengthDocumentFilter>();
            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = TokenValidationParameters(builder.Configuration);
                });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseCors("CorsPolicy");

            app.UseExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // Add Auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            SeedUsers(app.Services).Wait();

            app.Run();
        }

        /// <summary>
        /// Create a intial collection of Users to ensure the Application can be propperly used
        /// The "Superadmin" login should be provided to the Customer
        /// Also migrates the database to the latest version
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        static async Task SeedUsers(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<IApplicationDBContext>();
            await dbContext.MigrateAsync();

            var usermanager = scopedServices.GetRequiredService<IUserService>();
            var ordersManager = scopedServices.GetRequiredService<IOrderService>();

            if (!dbContext.Users.Any())
            {
                await usermanager.CreateSeed("Superadmin", "super", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 1", "m1", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 2", "m2", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 3", "m3", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 4", "m4", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 5", "m5", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 6", "m6", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 7", "m7", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 8", "m8", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 9", "m9", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 10", "m10", RoleNames.SuperAdmin);
            }

            if (!dbContext.Orders.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    await ordersManager.CreateSeed();
                }
                await dbContext.SaveChangesAsync();
            }
        }

    }
}