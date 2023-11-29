
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
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

            builder.Services.AddSingleton<ITokenService, TokenService>();
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
#pragma warning disable CS8604 // M�gliches Nullverweisargument.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
#pragma warning restore CS8604 // M�gliches Nullverweisargument.
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // This should defently be hidden in a production enviroment, for this case we leave it to have it inside docker
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            
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

            if (!dbContext.Users.Any())
            {
                await usermanager.CreateSeed("Superadmin", "super", RoleNames.SuperAdmin);
                await usermanager.CreateSeed("Mitarbeiter 1", "m1");
                await usermanager.CreateSeed("Mitarbeiter 2", "m2");
                await usermanager.CreateSeed("Mitarbeiter 3", "m3");
                await usermanager.CreateSeed("Mitarbeiter 4", "m4");
                await usermanager.CreateSeed("Mitarbeiter 5", "m5");
                await usermanager.CreateSeed("Mitarbeiter 6", "m6");
                await usermanager.CreateSeed("Mitarbeiter 7", "m7");
                await usermanager.CreateSeed("Mitarbeiter 8", "m8");
                await usermanager.CreateSeed("Mitarbeiter 9", "m9");
                await usermanager.CreateSeed("Mitarbeiter 10", "m10");
            }
        }

    }
}