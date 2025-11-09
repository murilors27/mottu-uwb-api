using Microsoft.AspNetCore.Mvc;
using Mottu.Uwb.Api.Models;
using Mottu.Uwb.Api.Services;

namespace Mottu.Uwb.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localizacao>>> GetAll()
        {
            var localizacoes = await _service.GetAllAsync();
            return Ok(localizacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> GetById(int id)
        {
            var localizacao = await _service.GetByIdAsync(id);
            if (localizacao == null)
                return NotFound("Localização não encontrada.");
            return Ok(localizacao);
        }

        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao)
        {
            var created = await _service.CreateAsync(localizacao);
            if (created == null)
                return BadRequest("MotoId ou SensorId inválido.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Localizacao localizacao)
        {
            var updated = await _service.UpdateAsync(id, localizacao);
            if (!updated)
                return NotFound("Localização não encontrada ou ID inválido.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound("Localização não encontrada.");

            return NoContent();
        }
    }
}
