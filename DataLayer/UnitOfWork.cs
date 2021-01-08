using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer
{
    public interface IUnitOfWork
    {
        Context Context { get; set; }
        IUserRepository Users { get; set; }
        INotificationRepository Notifications { get; set; }
        IViceRepository Vices { get; set; }
        IUserViceRepo UserVices { get; set; }
        Task SaveChangesAsync();

    }

    public class UnitOfWork : IUnitOfWork
    {

        public IUserRepository Users { get; set; }
        public INotificationRepository Notifications { get; set; }
        public IViceRepository Vices { get; set; }
        public IUserViceRepo UserVices { get; set; }
        public Context Context { get; set; }

        public UnitOfWork(Context context, IUserRepository userRepository, INotificationRepository notificationRepository, IUserViceRepo userVice, IViceRepository vice)
        {
            Context = context;
            Vices = vice;
            UserVices = userVice;
            Users = userRepository;
            Notifications = notificationRepository;
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
