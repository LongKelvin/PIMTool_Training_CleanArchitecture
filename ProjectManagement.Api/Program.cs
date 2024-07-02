using Microsoft.EntityFrameworkCore;

using ProjectManagement.Application.Commands.Projects;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Infrastructure.Data.DataContext;
using ProjectManagement.Infrastructure.Data.SampleData;
using ProjectManagement.Infrastructure.Repositories;

namespace ProjectManagement.Api
{
    public class Program
    {
        private static IConfiguration? _configuration;
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static void Main(string[] args)
        {
            ConfigureConfiguration();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = _configuration!.GetConnectionString("PIMToolDbConnection")!;
            var useInMemoryDatabase = bool.Parse(_configuration!.GetSection("UseInMemoryDatabase").Value!);

            if (useInMemoryDatabase)
            {
                builder.Services.AddDbContext<PIMToolDbContext>(options =>
                    options.UseInMemoryDatabase("PIMTool_InMemoryDb"));
            }
            else
            {
                builder.Services.AddDbContext<PIMToolDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString($"{connectionString}")));
            }

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

            var app = builder.Build();

            // Seed data after initializing the PIMToolDbContext
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                ServiceProvider = services;
                try
                {
                    var dbContext = services.GetRequiredService<PIMToolDbContext>();

                    if (useInMemoryDatabase)
                    {
                        // For in memory database only
                        dbContext.Database.EnsureCreated();
                    }
                    else
                    {
                        dbContext.Database.Migrate();
                    }

                    // Seed data after database initialization
                    SampleData.InitializeData(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void ConfigureConfiguration()
        {
            //Build configuration
            _configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();
        }
    }
}