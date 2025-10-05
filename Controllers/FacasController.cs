using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharkKnives.API.Models;
using SharkKnives.API.Services;

namespace SharkKnives.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacasController : ControllerBase
    {
        private readonly IFacaService _facaService;

        public FacasController(IFacaService facaService)
        {
            _facaService = facaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faca>>> GetFacas()
        {
            try
            {
                var facas = await _facaService.GetAllFacasAsync();
                return Ok(facas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faca>> GetFaca(int id)
        {
            try
            {
                var faca = await _facaService.GetFacaByIdAsync(id);

                if (faca == null)
                    return NotFound($"Faca com ID {id} não encontrada");

                return Ok(faca);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Faca>> CreateFaca(Faca faca)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdFaca = await _facaService.CreateFacaAsync(faca);
                return CreatedAtAction(nameof(GetFaca), new { id = createdFaca.Id }, createdFaca);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Faca>> UpdateFaca(int id, Faca faca)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != faca.Id)
                    return BadRequest("ID da URL não corresponde ao ID do corpo");

                var updatedFaca = await _facaService.UpdateFacaAsync(id, faca);
                return Ok(updatedFaca);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFaca(int id)
        {
            try
            {
                var result = await _facaService.DeleteFacaAsync(id);

                if (!result)
                    return NotFound($"Faca com ID {id} não encontrada");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}