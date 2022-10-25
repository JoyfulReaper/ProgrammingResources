using ProgrammingResources.API.Options;
using ProgrammingResources.API.ServiceSetup;

var builder = WebApplication.CreateBuilder(args);

// Options
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.Jwt));


// Services
builder.Services.AddControllers();

builder.AddSwagger();
builder.AddIdentity();
builder.AddAuthenticationAndAuthorization();
builder.AddCors();

var app = builder.Build();


// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
