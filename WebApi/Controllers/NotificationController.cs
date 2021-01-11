using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace WebApi.Controllers
{
    
    
    [Route("api/notifications")]
    [ApiController]
    // [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notification;
        public NotificationController(INotificationService notificationService)
        {
            _notification = notificationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetUnseenNotificationByIdAsync([FromRoute] string id)
        {
            return await _notification.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<NotificationDto>>> GetNotificationsByIdAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            var notif = await _notification.GetNotificationsForUserAsync(userId);
            if (notif != null)
            {
                return notif;
            }

            return BadRequest("error la notif");
        }

        [HttpPost("new")] // just for testing
        public async Task<ActionResult<NotificationDto>> AddNewNotification(NotificationDto requestDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            var notif = await _notification.CreateAsync(requestDto, userId);
            return notif;
        }

        [HttpPatch("seen/{id}")]
        public async Task<ActionResult<string>> SeenNotificationAsync([FromRoute] string id)
        {
            return await _notification.SeenNotificationAsync(id);
        }

    }
}
