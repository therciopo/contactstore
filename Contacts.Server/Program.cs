using Contacts.Application.Queries;
using Contacts.Infrastructure.Database;
using Contacts.Server.Endpoints;
using Microsoft.EntityFrameworkCore;
using Contacts.Infrastructure.ServiceCollectionExtensions;
using FluentValidation;
using static Contacts.Application.Commands.AddContact;
using Microsoft.AspNetCore.Diagnostics;

namespace Contacts.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllContacts).Assembly));
        builder.Services.AddValidatorsFromAssemblyContaining<CreateAddContactValidator>();
        //builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ContactsWebApiDb"));

        builder.Services.AddInfrastructure();


        var app = builder.Build();

        app.UseExceptionHandler(exceptionHandlerApp
            => exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if(contextFeature is not null)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        context.Response.StatusCode,
                        Message = "Internal Server Error"
                    });
                }

                await Results.Problem()
                .ExecuteAsync(context);
            
            }));

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.MapContactEndpoints();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}