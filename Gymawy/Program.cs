using Domain.Common;
using Domain.Contracts;
using Domain.Seeding;
using Gymawy.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

namespace Gymawy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);           

            builder.Services.AddDbContext<GymDbContext>(
                option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<GymDbContext>();



            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddOpenApi();
            //builder.Services.AddSwaggerGen();


            builder.Services.AddAplicationServices();

            var app = builder.Build();



            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = service.GetRequiredService<GymDbContext>();
                await dbContext.Database.MigrateAsync();

                var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                await IdentitySeeding.RoleSeeding(roleManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error during database update or seeding.");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
