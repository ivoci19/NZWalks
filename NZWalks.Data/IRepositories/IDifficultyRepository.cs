using NZWalks.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Data.IRepositories
{
    public interface IDifficultyRepository
    {
        Task<IEnumerable<Difficulty>> GetAllAsync();
        Task<Difficulty> GetAsync(Guid id);
        Task<Difficulty> AddAsync(Difficulty walkDifficulty);
        Task<Difficulty> UpdateAsync(Guid id, Difficulty walkDifficulty);
        Task<Difficulty> DeleteAsync(Guid id);
    }
}
