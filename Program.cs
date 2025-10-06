var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "API FUNCIONANDO!");
app.MapGet("/health", () => "OK");
app.MapGet("/test", () => new { message = "Teste OK", time = DateTime.Now });
// Database Setup
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    Console.WriteLine("?? Creating database...");
    var created = await context.Database.EnsureCreatedAsync();
    Console.WriteLine(created ? "? Database created!" : "? Database already exists");

    // ⬇️⬇️⬇️ ADICIONE AQUI ⬇️⬇️⬇️
    Console.WriteLine("🎯 API RODANDO - URL: http://0.0.0.0:8080");
    Console.WriteLine("🎯 Health check: http://0.0.0.0:8080/health");
    // ⬆️⬆️⬆️ FIM DAS LINHAS ⬆️⬆️⬆️

    Console.WriteLine("?? Seeding data...");
    DbInitializer.Initialize(context);
    Console.WriteLine("? Data seeded!");
}
catch (Exception ex)
{
    Console.WriteLine($"? Database error: {ex.Message}");
}

app.Run();

