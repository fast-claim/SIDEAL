using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// Cargar las variables de entorno desde el archivo .env antes de configurar la aplicación
DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

// Acceder a la configuración de ApiKey
var apiKey = builder.Configuration["AppSettings:ApiKey"];

// Add services to the container.
builder.Services.AddControllers();

// Habilitar CORS para permitir cualquier origen, método y encabezado
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Agregar Swagger para la documentación de la API
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

// Configuración del middleware de CORS, debe estar antes de Authorization
app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();
