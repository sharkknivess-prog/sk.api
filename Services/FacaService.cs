using Microsoft.EntityFrameworkCore;
using SharkKnives.API.Data;
using SharkKnives.API.Models;

namespace SharkKnives.API.Services
{
    public class FacaService : IFacaService
    {
        private readonly AppDbContext _context;

        public FacaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Faca>> GetAllFacasAsync()
        {
            return await _context.Facas
                .Where(f => f.Ativo)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<Faca?> GetFacaByIdAsync(int id)
        {
            return await _context.Facas
                .FirstOrDefaultAsync(f => f.Id == id && f.Ativo);
        }

        public async Task<Faca> CreateFacaAsync(Faca faca)
        {
            faca.CreatedAt = DateTime.UtcNow;
            faca.UpdatedAt = DateTime.UtcNow;

            _context.Facas.Add(faca);
            await _context.SaveChangesAsync();

            return faca;
        }

        public async Task<Faca> UpdateFacaAsync(int id, Faca faca)
        {
            var existingFaca = await _context.Facas.FindAsync(id);
            if (existingFaca == null)
                throw new ArgumentException("Faca não encontrada");

            // Atualizar propriedades
            existingFaca.Modelo = faca.Modelo;
            existingFaca.Material = faca.Material;
            existingFaca.Camadas = faca.Camadas;
            existingFaca.Pegada = faca.Pegada;
            existingFaca.Cabo = faca.Cabo;
            existingFaca.Dimensoes = faca.Dimensoes;
            existingFaca.Referencia = faca.Referencia;
            existingFaca.FotoUrl1 = faca.FotoUrl1;
            existingFaca.FotoUrl2 = faca.FotoUrl2;
            existingFaca.FotoUrl3 = faca.FotoUrl3;
            existingFaca.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingFaca;
        }

        public async Task<bool> DeleteFacaAsync(int id)
        {
            var faca = await _context.Facas.FindAsync(id);
            if (faca == null)
                return false;

            // Soft delete
            faca.Ativo = false;
            faca.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}