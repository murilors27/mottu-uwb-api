using Microsoft.AspNetCore.Mvc;
using Mottu.Uwb.Api.Models;
using Mottu.Uwb.Api.Services;

namespace Mottu.Uwb.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _service;

        public MotoController(MotoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetAll()
        {
            var motos = await _service.GetAllAsync();
            return Ok(motos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetById(int id)
        {
            var moto = await _service.GetByIdAsync(id);
            if (moto == null)
                return NotFound("Moto não encontrada.");
            return Ok(moto);
        }

        [HttpPost]
        public async Task<ActionResult<Moto>> Post(Moto moto)
        {
            var created = await _service.CreateAsync(moto);
            if (created == null)
                return BadRequest("Já existe uma moto com esse IdentificadorUWB.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Moto moto)
        {
            var updated = await _service.UpdateAsync(id, moto);
            if (!updated)
                return NotFound("Moto não encontrada ou ID inválido.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound("Moto não encontrada.");

            return NoContent();
        }
    }
}
