var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "API FUNCIONANDO!");
app.MapGet("/health", () => "OK");
app.MapGet("/test", () => new { message = "Teste OK", time = DateTime.Now });

app.Run();
Console.WriteLine("🎯 API RODANDO - ACESSE: http://localhost:8080");
