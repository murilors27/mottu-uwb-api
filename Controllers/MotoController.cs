using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Data;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetAll()
        {
            return await _context.Motos.Include(m => m.Sensor).ToListAsync();
        }

        // GET: api/moto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetById(int id)
        {
            var moto = await _context.Motos.Include(m => m.Sensor).FirstOrDefaultAsync(m => m.Id == id);
            if (moto == null)
                return NotFound();

            return moto;
        }

        // POST: api/moto
        [HttpPost]
        public async Task<ActionResult<Moto>> Create(Moto moto)
        {
            var lista = await _context.Motos.ToListAsync();
            var exists = lista.Any(m => m.IdentificadorUWB == moto.IdentificadorUWB);

            if (exists)
                return BadRequest($"Já existe uma moto com IdentificadorUWB '{moto.IdentificadorUWB}'.");

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }

        // PUT: api/moto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Moto moto)
        {
            if (id != moto.Id)
                return BadRequest();

            var motoExistente = await _context.Motos.FindAsync(id);
            if (motoExistente == null)
                return NotFound();

            motoExistente.Modelo = moto.Modelo;
            motoExistente.Cor = moto.Cor;
            motoExistente.IdentificadorUWB = moto.IdentificadorUWB;
            motoExistente.Status = moto.Status;
            motoExistente.SensorId = moto.SensorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/moto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

