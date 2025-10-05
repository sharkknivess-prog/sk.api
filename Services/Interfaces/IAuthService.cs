using SharkKnives.API.Models;

namespace SharkKnives.API.Services
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string nome);
        Task<Usuario?> GetUserByEmailAsync(string email);
    }
}