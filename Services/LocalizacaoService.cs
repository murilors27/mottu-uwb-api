using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Data;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Services
{
    public class LocalizacaoService
    {
        private readonly AppDbContext _context;

        public LocalizacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Localizacao>> GetAllAsync()
        {
            return await _context.Localizacoes.AsNoTracking().ToListAsync();
        }

        public async Task<Localizacao?> GetByIdAsync(int id)
        {
            return await _context.Localizacoes.FindAsync(id);
        }

        public async Task<Localizacao?> CreateAsync(Localizacao localizacao)
        {
            var motoExists = await _context.Motos.AnyAsync(m => m.Id == localizacao.MotoId);
            var sensorExists = await _context.Sensores.AnyAsync(s => s.Id == localizacao.SensorId);

            if (!motoExists || !sensorExists)
                return null;

            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();
            return localizacao;
        }

        public async Task<bool> UpdateAsync(int id, Localizacao localizacao)
        {
            if (id != localizacao.Id)
                return false;

            var exists = await _context.Localizacoes.AsNoTracking().AnyAsync(l => l.Id == id);
            if (!exists)
                return false;

            _context.Localizacoes.Update(localizacao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
                return false;

            _context.Localizacoes.Remove(localizacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
