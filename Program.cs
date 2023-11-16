
using Microsoft.EntityFrameworkCore;
using ProductManagerAPI.Data;

namespace ProductManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                     builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();

                });
            });
        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin() // Till�ter alla dom�ner att anropa API:et
                .AllowAnyHeader() // Till�ter alla headers i anropet
                .AllowAnyMethod(); // Till�ter alla metoder i anropet
            });

            app.MapControllers();

            app.Run();
        }
    }
}