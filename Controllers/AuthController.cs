using Microsoft.AspNetCore.Mvc;
using SharkKnives.API.Models;
using SharkKnives.API.Services;

namespace SharkKnives.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var token = await _authService.AuthenticateAsync(loginDto.Email, loginDto.Password);

                if (token == null)
                    return Unauthorized("Credenciais inválidas");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.RegisterAsync(
                    registerDto.Email,
                    registerDto.Password,
                    registerDto.Nome
                );

                if (!result)
                    return Conflict("Usuário já existe");

                return Ok(new { Message = "Usuário registrado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _authService.GetUserByEmailAsync(email);

                if (user == null)
                    return NotFound("Usuário não encontrado");

                // Não retornar a senha
                return Ok(new { user.Id, user.Email, user.Nome, user.CreatedAt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}