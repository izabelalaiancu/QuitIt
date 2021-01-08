using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Entities
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; }
        public bool Seen { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }

        public Notification()
        {
            Seen = false;
        }

    }
}
