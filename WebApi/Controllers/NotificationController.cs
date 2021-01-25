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
    [Authorize]
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
        [Route("mine")]
        public async Task<ActionResult<List<NotificationDto>>> GetAllNotificationsAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            var notif = await _notification.GetNotificationsForUserAsync(userId);
            if (notif != null)
            {
                return notif;
            }

            return BadRequest("error la notif");
        }

        [AllowAnonymous]
        [HttpPost("new")] // just for testing
        public async Task<ActionResult<NotificationDto>> AddNewNotification(NotificationDto requestDto)
        {
            var notif = await _notification.CreateAsync(requestDto);
            return notif;
        }

        [HttpPut("seen/{id}")]
        public async Task<ActionResult<string>> SeenNotificationAsync([FromRoute] string id)
        {
            return await _notification.SeenNotificationAsync(id);
        }

    }
}
