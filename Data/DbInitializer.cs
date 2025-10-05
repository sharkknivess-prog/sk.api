using SharkKnives.API.Models;

namespace SharkKnives.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            Console.WriteLine("🎯 Starting fresh database initialization...");

            // Criar usuário admin padrão
            var usuario = new Usuario
            {
                Email = "admin@sharkknives.com",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Nome = "Administrador",
                CreatedAt = DateTime.UtcNow
            };

            context.Usuarios.Add(usuario);

            // Criar facas de exemplo
            var facas = new List<Faca>
            {
                new Faca
                {
                    Modelo = "Shark Premium V2",
                    Material = "Aço Inox 440C Premium",
                    Camadas = "3 Camadas",
                    Pegada = "Ergonômica Pro",
                    Cabo = "Madeira de Carvalho Natural",
                    Dimensoes = "25cm total - 12cm lâmina",
                    Referencia = "SHK-P001-V2",
                    Ativo = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Faca
                {
                    Modelo = "Tubarão Profissional Plus",
                    Material = "Aço Damasco Japonês",
                    Camadas = "67 Camadas",
                    Pegada = "Anti-derrapante Térmica",
                    Cabo = "Fibra de Carbono Aeronáutico",
                    Dimensoes = "30cm total - 15cm lâmina",
                    Referencia = "SHK-P002-PLUS",
                    Ativo = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Faca
                {
                    Modelo = "Lâmina Oceânica Master",
                    Material = "Aço VG-10 Core",
                    Camadas = "32 Camadas San Mai",
                    Pegada = "Balanceada Naval",
                    Cabo = "Micarta Verde Marinho",
                    Dimensoes = "28cm total - 14cm lâmina",
                    Referencia = "SHK-OCEAN-MASTER",
                    Ativo = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Facas.AddRange(facas);
            context.SaveChanges();

            Console.WriteLine($"✅ Fresh start complete!");
            Console.WriteLine($"✅ Added admin user: admin@sharkknives.com / admin123");
            Console.WriteLine($"✅ Added {facas.Count} sample knives");
        }
    }
}