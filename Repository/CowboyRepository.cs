using Cowboy.Repository.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cowboy.Repository
{
    public class CowboyRepository : ICowboyRepository
    {
        private readonly CowboyDbContext _context;
        public CowboyRepository(CowboyDbContext context)
        {
            _context = context;
        }

        public async Task<CowboyEntity> AddCowboyAsync(CowboyEntity cowboy)
        {
            var result = await _context.Cowboys.AddAsync(cowboy);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteCowboyAsync(int id)
        {
            var result = await _context.Cowboys
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Cowboys.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CowboyEntity?> GetCowboyAsync(int id)
        {
            return await _context.Cowboys
                .Include(x => x.Firearm)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CowboyEntity>> GetCowboysAsync()
        {
            return await _context.Cowboys
                .Include(x => x.Firearm)
                .ToListAsync();
        }

        public async Task<CowboyEntity?> UpdateCowboyPatchAsync(int id, JsonPatchDocument cowboyDocument)
        {
            var cowboyQuery = await GetCowboyAsync(id);
            if (cowboyQuery == null)
            {
                return cowboyQuery;
            }

            cowboyDocument.ApplyTo(cowboyQuery);
            await _context.SaveChangesAsync();

            return cowboyQuery;
        }
    }
}
