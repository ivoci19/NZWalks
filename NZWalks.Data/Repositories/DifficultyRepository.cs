using Microsoft.EntityFrameworkCore;
using NZWalks.Data.Data;
using NZWalks.Data.IRepositories;
using NZWalks.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Data.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {

        private readonly ApplicationDbContext _context;

        public DifficultyRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Difficulty> AddAsync(Difficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _context.Difficulties.AddAsync(walkDifficulty);
            await _context.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<Difficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await _context.Difficulties.FindAsync(id);
            if (existingWalkDifficulty != null)
            {
                _context.Difficulties.Remove(existingWalkDifficulty);
                await _context.SaveChangesAsync();
                return existingWalkDifficulty;
            }
            return null;
        }

        public async Task<IEnumerable<Difficulty>> GetAllAsync()
        {
            return await _context.Difficulties.ToListAsync();
        }

        public async Task<Difficulty> GetAsync(Guid id)
        {
            return await _context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Difficulty> UpdateAsync(Guid id, Difficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _context.Difficulties.FindAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Name = walkDifficulty.Name;
            await _context.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
