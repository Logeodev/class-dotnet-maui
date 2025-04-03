
using colors_api.Services;
using ColorsApi.Configurations;

namespace colors_api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.ConfigureTelemetry();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            // Enregistrement des services
            builder.Services.AddSingleton<IPaletteStorageService, InMemoryPaletteStorageService>();
            builder.Services.AddScoped<PaletteGeneratorService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
