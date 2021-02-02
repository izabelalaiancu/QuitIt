using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IAchievementRepository : IBaseRepository<Achievement>
    {
        Task<List<Achievement>> GetUserAchievementsAsync(string userId);
    }
    public class AchievementRepository : BaseRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(Context db) : base(db)
        {
        }


        public async Task<List<Achievement>> GetUserAchievementsAsync(string userId)
        {
            return await _dbSet.Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}
