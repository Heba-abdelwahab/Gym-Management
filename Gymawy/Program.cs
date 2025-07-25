using Domain.Contracts;
using Domain.Entities;
using Gymawy.Extensions;
using Gymawy.Factories;
using Gymawy.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using Shared.Cloudinary;
using Shared.Jwt;
using System.Text;
namespace Gymawy;


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

        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddSignalR();
        builder.Services.AddSingleton<PresenceTracker>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("myCore", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();

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

              config.Events = new JwtBearerEvents
              {
                  OnMessageReceived = context =>
                  {
                      var accessToken = context.Request.Query["access_token"];
                      var path = context.Request.Path;
                      if (!string.IsNullOrWhiteSpace(path) && path.StartsWithSegments("/hubs"))
                      {
                          context.Token = accessToken;
                      }
                      return Task.CompletedTask;
                  }
              };

          });

        builder.Services.AddScoped<IDbInitializer, DbInitializer>();


        #endregion

        var app = builder.Build();

        //using (var scoped = app.Services.CreateScope())
        //{
        //    using (var dbContext = scoped.ServiceProvider.GetRequiredService<GymDbContext>())
        //    {
        //        if (dbContext.Database.GetPendingMigrations().Any())
        //            await dbContext.Database.MigrateAsync();
        //    }
        //}
        await app.SeedDbAsync();

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
        app.MapHub<PresenceHub>("hubs/presence");
        app.MapHub<MessageHub>("hubs/message");
        //app.MapFallbackToController("Index", "Fallback");
        #endregion

        app.Run();
    }
}
