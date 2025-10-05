using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharkKnives.API.Data;
using SharkKnives.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharkKnives.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.SenhaHash))
                return null;

            // Gerar token JWT
            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(string email, string password, string nome)
        {
            // Verificar se usuário já existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
                return false;

            var user = new Usuario
            {
                Email = email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(password),
                Nome = nome,
                CreatedAt = DateTime.UtcNow
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> GetUserByEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Nome)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"] ?? "60")),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}