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
    public class WalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _context;

        public WalkRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            // Assign New ID
            walk.Id = Guid.NewGuid();
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await _context.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            _context.Walks.Remove(existingWalk);
            await _context.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IList<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            //return await
            //    _context.Walks
            //    .Include(x => x.Region)
            //    .Include(x => x.Difficulty)
            //    .ToListAsync();

            var walks = _context.Walks
               .Include(x => x.Region)
               .Include(x => x.Difficulty)
               .AsQueryable();

            //Apply filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)){

                    walks = walks.Where(x => x.Name.Contains(filterQuery));
          
                }
                
            }
            return await walks.ToListAsync();
        }

        public Task<Walk> GetAsync(Guid id)
        {
            return _context.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await _context.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.Name = walk.Name;
                existingWalk.DifficultyId = walk.DifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await _context.SaveChangesAsync();
                return existingWalk;
            }

            return null;
        }
    }
}
