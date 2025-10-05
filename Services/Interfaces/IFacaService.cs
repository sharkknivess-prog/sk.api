using SharkKnives.API.Models;

namespace SharkKnives.API.Services
{
    public interface IFacaService
    {
        Task<IEnumerable<Faca>> GetAllFacasAsync();
        Task<Faca?> GetFacaByIdAsync(int id);
        Task<Faca> CreateFacaAsync(Faca faca);
        Task<Faca> UpdateFacaAsync(int id, Faca faca);
        Task<bool> DeleteFacaAsync(int id);
    }
}