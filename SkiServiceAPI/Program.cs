
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SkiServiceAPI.Data;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Services;
using System.Text;

namespace SkiServiceAPI
{
    public class Program
    {
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

            // Connect to default configured database
            builder.Services.AddDbContext<IApplicationDBContext, ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
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
            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Add Auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            SeedUsers(app.Services).Wait();

            app.Run();
        }

        static async Task SeedUsers(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<IApplicationDBContext>();
            var usermanager = scopedServices.GetRequiredService<IUserService>();

            if (!dbContext.Users.Any())
            {
                usermanager.CreateUser("Superadmin", "super", RoleNames.SuperAdmin);
                usermanager.CreateUser("Mitarbeiter 1", "m1");
                usermanager.CreateUser("Mitarbeiter 2", "m2");
                usermanager.CreateUser("Mitarbeiter 3", "m3");
                usermanager.CreateUser("Mitarbeiter 4", "m4");
                usermanager.CreateUser("Mitarbeiter 5", "m5");
                usermanager.CreateUser("Mitarbeiter 6", "m6");
                usermanager.CreateUser("Mitarbeiter 7", "m7");
                usermanager.CreateUser("Mitarbeiter 8", "m8");
                usermanager.CreateUser("Mitarbeiter 9", "m9");
                usermanager.CreateUser("Mitarbeiter 10", "m10");
            }
        }
    }
}