using kol.Middlewares;
using kol.DAL;
using kol.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace kol;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<BatchDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        });

        builder.Services.AddScoped<INurseriesService, NurseriesService>();
        
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "PurchasesApi",
                Description = "api for managing purchases",
                Contact = new OpenApiContact
                {
                    Name="Api Support",
                    Email="apiSupport@gmail.com",
                    Url = new Uri("https://github.com/apiSupport")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });

        var app = builder.Build();

        app.UseGlobalExceptionHandling();
        
        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "webApi");
            
            c.DocExpansion(DocExpansion.List);
            c.DefaultModelExpandDepth(0);
            c.DisplayRequestDuration();
            c.EnableFilter();
            
        });
        

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}