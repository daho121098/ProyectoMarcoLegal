var builder = WebApplication.CreateBuilder(args);

// Agregar solo controladores (sin vistas)
builder.Services.AddControllers();

// Agrega servicios como Swagger (opcional pero útil en APIs)
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// Mapear controladores como API REST
app.MapControllers();

app.Run();
