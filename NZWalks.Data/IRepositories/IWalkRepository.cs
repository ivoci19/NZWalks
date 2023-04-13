using NZWalks.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Data.IRepositories
{
    public interface IWalkRepository
    {
        Task<IList<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,string? sortBy = null, bool isAscending = true);
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}
