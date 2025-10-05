using Microsoft.EntityFrameworkCore;
using SharkKnives.API.Models;

namespace SharkKnives.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Faca> Facas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela Facas
            modelBuilder.Entity<Faca>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Material).HasMaxLength(255);
                entity.Property(e => e.Camadas).HasMaxLength(100);
                entity.Property(e => e.Pegada).HasMaxLength(100);
                entity.Property(e => e.Cabo).HasMaxLength(100);
                entity.Property(e => e.Dimensoes).HasMaxLength(100);
                entity.Property(e => e.Referencia).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");

                // Index para melhor performance
                entity.HasIndex(e => e.Ativo);
                entity.HasIndex(e => e.CreatedAt);
            });

            // Configuração da tabela Usuarios
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.SenhaHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");

                // Email único
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}