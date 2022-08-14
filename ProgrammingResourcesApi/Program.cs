using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Repositories;
using ProgrammingResourcesLibrary.Repositories.Interfaces;
using AutoMapper;
using ProgrammingResourcesApi.DTOs;

namespace ProgrammingResourcesApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IDataAccess, SqlDataAccess>();
        builder.Services.AddScoped<IResourceRepo, ResourceRepo>();
        builder.Services.AddScoped<ITagRepo, TagRepo>();
        builder.Services.AddScoped<IResourceTagRepo, ResourceTagRepo>();

        builder.Services.AddAutoMapper(typeof(ResourceDto));

        var app = builder.Build();

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
