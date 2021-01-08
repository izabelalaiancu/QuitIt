using System.Collections.Generic;
using DataLayer;
using DataLayer.Models.Entities;
using Services.Dtos;
using System.Threading.Tasks;
using AutoMapper;

namespace Services
{
    public interface INotificationService : IBaseService
    {
        Task<NotificationDto> CreateAsync(NotificationDto dto, string userId);
        Task<string> SeenNotificationAsync(string notificationId);
        Task<List<NotificationDto>> GetNotificationsForUserAsync(string userId);
        Task<NotificationDto> GetByIdAsync(string id);
    }

    public class NotificationService : BaseService, INotificationService
    {
        private readonly IMapper _mapper;
        public NotificationService(IMapper mapper, IUnitOfWork unit) : base(unit)
        {
            _mapper = mapper;
        }

        public async Task<NotificationDto> GetByIdAsync(string id)
        {
            var notification = await UnitOfWork.Notifications.GetByIdAsync(id);
            return notification == null ? null : _mapper.Map<NotificationDto>(notification);
        }

        public async Task<NotificationDto> CreateAsync(NotificationDto dto, string userId)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = dto.Title,
                Text = dto.Text
            };
            UnitOfWork.Notifications.Add(notification);
            await UnitOfWork.SaveChangesAsync();
            return _mapper.Map<NotificationDto>(dto);
        }

        public async Task<string> SeenNotificationAsync(string notificationId)
        {
            var notification = await UnitOfWork.Notifications.GetByIdAsync(notificationId);
            if (notification == null)
                return "Notification not found! naspi";
            notification.Seen = true;
            UnitOfWork.Notifications.Update(notification);
            await UnitOfWork.SaveChangesAsync();
            return "Notification Updated Successfully!";
        }

        public async Task<List<NotificationDto>> GetNotificationsForUserAsync(string userId)
        {
            var notifications = await UnitOfWork.Notifications.GetUnseenByUserIdAsync(userId);
            return _mapper.Map<List<NotificationDto>>(notifications);
        }

    }
}

