using API_de_Gestión_de_Archivos.Helper;
using API_de_Gestion_de_Archivos.Middleware;
using API_de_Gestión_de_Archivos.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<IArchivoHelper, ArchivoHelper>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

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
