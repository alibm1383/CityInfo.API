using CityInfo.API.DbContexts;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;


namespace CityInfo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().
                WriteTo.File("logs/cityInfo.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();
            builder.Services.AddControllers().AddNewtonsoftJson().AddXmlSerializerFormatters();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
            builder.Services.AddTransient<ISmsService,SmsService>();
            builder.Services.AddTransient<IEmailService, GmailService>();
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDb"));
            });
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
