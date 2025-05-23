using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Data;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/sensor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetAll()
        {
            return await _context.Sensores.ToListAsync();
        }

        // GET: api/sensor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetById(int id)
        {
            var sensor = await _context.Sensores.FindAsync(id);
            return sensor is null ? NotFound() : Ok(sensor);
        }

        // POST: api/sensor
        [HttpPost]
        public async Task<ActionResult<Sensor>> Create(Sensor sensor)
        {
            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, sensor);
        }

        // PUT: api/sensor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sensor sensor)
        {
            if (id != sensor.Id) return BadRequest();

            var existing = await _context.Sensores.FindAsync(id);
            if (existing is null) return NotFound();

            existing.Localizacao = sensor.Localizacao;
            existing.Patio = sensor.Patio;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/sensor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await _context.Sensores.FindAsync(id);
            if (sensor is null) return NotFound();

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
