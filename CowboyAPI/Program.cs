using AutoMapper;
using Cowboy.API.DTO;
using Cowboy.API.Middleware;
using Cowboy.Repository;
using Cowboy.Repository.Models;
using Cowboy.Services;
using Cowboy.Services.Clients;
using Cowboy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CowboyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Register the AppSettings configuration section
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            builder.Services.AddControllers()
                .AddNewtonsoftJson();

            // Add in-memory DbContext
            builder.Services.AddDbContext<CowboyDbContext>(options => options.UseInMemoryDatabase(databaseName: "Cowboys"));

            // Provide CowboyRepository
            builder.Services.AddScoped<ICowboyRepository, CowboyRepository>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Register Data Services
            builder.Services.AddScoped<ICowboyService, CowboyService>();
            builder.Services.AddScoped<IFirearmService, FirearmService>();
            builder.Services.AddScoped<ICombatService, CombatService>();

            // Register CowboyClient
            builder.Services.AddScoped<ICowboyClient, CowboyClient>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Use API Key authorization
            app.UseMiddleware<ApiKeyMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}