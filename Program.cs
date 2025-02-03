using ImageUploadMS.Services;
using ImageUploadMS.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddScoped<IImageUpload, ImageUploadServices>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar Swagger con soporte para archivos
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Image Upload Microservice",
        Version = "v1",
        Description = "API para subir imágenes a un servidor."
    });
});

var app = builder.Build();

app.UseStaticFiles(); // Asegura que wwwroot/ se sirva correctamente


// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Image Upload Microservice v1");
    c.RoutePrefix = string.Empty; // Cargar Swagger en la raíz
});

// Manejo de errores en modo Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Seguridad y configuración base
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
