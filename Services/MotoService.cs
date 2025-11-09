using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Data;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Services
{
    public class MotoService
    {
        private readonly AppDbContext _context;

        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moto>> GetAllAsync()
        {
            return await _context.Motos.ToListAsync();
        }

        public async Task<Moto?> GetByIdAsync(int id)
        {
            return await _context.Motos.FindAsync(id);
        }

        public async Task<Moto?> CreateAsync(Moto moto)
        {
            bool exists = await _context.Motos.AnyAsync(m => m.IdentificadorUWB == moto.IdentificadorUWB);
            if (exists) return null;

            moto.Ativo = true;
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return moto;
        }

        public async Task<bool> UpdateAsync(int id, Moto moto)
        {
            var existingMoto = await _context.Motos.FindAsync(id);
            if (existingMoto == null)
                return false;

            existingMoto.Modelo = moto.Modelo;
            existingMoto.Cor = moto.Cor;
            existingMoto.IdentificadorUWB = moto.IdentificadorUWB;

            await _context.SaveChangesAsync();

            _context.Entry(existingMoto).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
