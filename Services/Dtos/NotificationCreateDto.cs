using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class NotificationCreateDto
    {
        public string UserId { get; set; }
        public bool Seen { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
