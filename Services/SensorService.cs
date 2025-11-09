using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Data;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Services
{
    public class SensorService
    {
        private readonly AppDbContext _context;

        public SensorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sensor>> GetAllAsync()
        {
            return await _context.Sensores.AsNoTracking().ToListAsync();
        }

        public async Task<Sensor?> GetByIdAsync(int id)
        {
            return await _context.Sensores.FindAsync(id);
        }

        public async Task<Sensor> CreateAsync(Sensor sensor)
        {
            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();
            return sensor;
        }

        public async Task<bool> UpdateAsync(int id, Sensor sensor)
        {
            if (id != sensor.Id)
                return false;

            var existing = await _context.Sensores.FindAsync(id);
            if (existing == null)
                return false;

            existing.Localizacao = sensor.Localizacao;
            existing.Patio = sensor.Patio;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sensor = await _context.Sensores.FindAsync(id);
            if (sensor == null)
                return false;

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
