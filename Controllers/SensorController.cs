using Microsoft.AspNetCore.Mvc;
using Mottu.Uwb.Api.Models;
using Mottu.Uwb.Api.Services;

namespace Mottu.Uwb.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _service;

        public SensorController(SensorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetAll()
        {
            var sensores = await _service.GetAllAsync();
            return Ok(sensores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetById(int id)
        {
            var sensor = await _service.GetByIdAsync(id);
            if (sensor == null)
                return NotFound("Sensor não encontrado.");
            return Ok(sensor);
        }

        [HttpPost]
        public async Task<ActionResult<Sensor>> Create(Sensor sensor)
        {
            var created = await _service.CreateAsync(sensor);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sensor sensor)
        {
            var updated = await _service.UpdateAsync(id, sensor);
            if (!updated)
                return NotFound("Sensor não encontrado ou ID inválido.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound("Sensor não encontrado.");

            return NoContent();
        }
    }
}
