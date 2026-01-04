using System.Reflection;
using bookapi_minimal.AppContext;
using bookapi_minimal.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using bookapi_minimal.Services;
using bookapi_minimal.Exceptions;
namespace bookapi_minimal.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (builder.Configuration == null) throw new ArgumentNullException(nameof(builder.Configuration));

            // Adding the database context
            builder.Services.AddDbContext<ApplicationContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
            });

            // Adding validators from the current assembly
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

        }
    }
}