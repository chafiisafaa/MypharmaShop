using Planning;
using Repository.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using EnqueteManagement;
using Repository.UnitOfWork;

[assembly: FunctionsStartup(typeof(Program))]
namespace EnqueteManagement
{
    public class Program: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(
              options => options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Please insert a valid connection string")));

            builder.Services.AddMvcCore().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



        }
    }
}
