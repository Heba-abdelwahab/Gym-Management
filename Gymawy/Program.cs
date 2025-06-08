using Domain.Contracts;
using Domain.Entities;
using Gymawy.Extensions;
using Gymawy.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using Shared;
using Shared.Cloudinary;
using System.Text;
namespace Gymawy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region DI Services
            builder.Services.AddControllers()
              .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<GymDbContext>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<GymDbContext>();


            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.Configure<CloudinarySettings>
                (builder.Configuration.GetSection(nameof(CloudinarySettings)));

            builder.Services.Configure<JwtOptions>
                (builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);




            builder.Services.Configure<ApiBehaviorOptions>(options =>
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse
            );


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myCore", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(config =>
              {
                  var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
                  var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.SecretKey!));

                  config.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      IssuerSigningKey = key

                  };

              });

            #endregion

            var app = builder.Build();

            using (var scoped = app.Services.CreateScope())
            {
                using (var dbContext = scoped.ServiceProvider.GetRequiredService<GymDbContext>())
                {
                    if (dbContext.Database.GetPendingMigrations().Any())
                        await dbContext.Database.MigrateAsync();
                }
            }


            #region PipeLines

            //app.UseCustomExceptionMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("myCore");

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.MapControllers();
            //app.MapFallbackToController("Index", "Fallback");
            #endregion

            app.Run();
        }
    }
}
