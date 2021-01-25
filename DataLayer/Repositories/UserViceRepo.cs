using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IUserViceRepo : IBaseRepository<UserVice>
    {
        Task<List<UserVice>> GetVicesByUserIdAsync(string userId);
        Task<UserVice> GetVicesByUserIdAndViceIdAsync(string userId, string viceId);
    }

    public class UserViceRepo: BaseRepository<UserVice>, IUserViceRepo
    {
        public UserViceRepo(Context db) : base(db)
        {
        }
        

        public async Task<List<UserVice>> GetVicesByUserIdAsync(string userId)
        {
            return await _db.UserVices.Where(v => v.UserId == userId && v.IsDeleted == false)
                .Include(p => p.Vice)
                .ToListAsync();

        }

        public async Task<UserVice> GetVicesByUserIdAndViceIdAsync(string userId, string viceId)
        {
            return await _db.UserVices.Where(v => v.UserId == userId && v.ViceId == viceId)
                .Include(p => p.Vice)
                .FirstOrDefaultAsync();
        }
    }
}
