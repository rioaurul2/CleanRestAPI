using Serilog;
using Serilog.Events;
using Tutorial.Middlewares;
using TutorialApplication.Extensions;
using TutorialInfrastructure.Extensions;
using TutorialInfrastructure.Seeders;

namespace Tutorial
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Host.UseSerilog((context, configuration) => 
            configuration
                .ReadFrom.Configuration(context.Configuration));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<TotalLoggingMiddleware>();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeders = scope.ServiceProvider.GetRequiredService<IRestaurantSeeders>();
            await seeders.Seed();

            //Middlewares

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<TotalLoggingMiddleware>();

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
    }
}
