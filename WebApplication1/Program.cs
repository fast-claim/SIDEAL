using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// Cargar las variables de entorno desde el archivo .env antes de configurar la aplicaci�n
DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

// Acceder a la configuraci�n de ApiKey
var apiKey = builder.Configuration["AppSettings:ApiKey"];

// Add services to the container.
builder.Services.AddControllers();

// Habilitar CORS para permitir cualquier origen, m�todo y encabezado
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Agregar Swagger para la documentaci�n de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configuraci�n del middleware de CORS, debe estar antes de Authorization
app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();
