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
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<List<Notification>> GetUnseenByUserIdAsync(string userId);
    }

    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(Context db) : base(db)
        {
        }

        public async Task<List<Notification>> GetUnseenByUserIdAsync(string userId)
        {
            return await _db.Notifications
                .Where(n => n.UserId == userId && n.Seen == false)
                .ToListAsync();
        }
    }
}
