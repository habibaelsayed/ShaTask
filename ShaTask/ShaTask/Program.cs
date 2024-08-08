
using Microsoft.EntityFrameworkCore;
using ShaTask.Interfaces;
using ShaTask.Models;
using ShaTask.Repositories;
using ShaTask.Services;

namespace ShaTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ShaTaskContext>(options =>
                 options.UseLazyLoadingProxies()
                 .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
             );
            builder.Services.AddScoped<IInvoice, InvoiceRepo>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<ICashierService, CashierService>();
            builder.Services.AddScoped<ICashier, CashierRepo>();
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

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5500", "http://127.0.0.1:5500"));

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
