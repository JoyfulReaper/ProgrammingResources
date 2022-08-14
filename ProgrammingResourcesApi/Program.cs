using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Repositories;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

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
        builder.Services.AddScoped<IResourceRepo, IResourceRepo>();
        builder.Services.AddScoped<ITagRepo, ITagRepo>();
        builder.Services.AddScoped<IResourceTagRepo, ResourceTagRepo>();

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
