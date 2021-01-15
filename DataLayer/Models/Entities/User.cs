using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Models.Entities
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Notification> Notifications { get; set; }
        public IList<UserVice> UserVices { get; set; }
        public IList<Achievement> Achievements { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            Notifications = new List<Notification>();
            UserVices = new List<UserVice>();
        }
    }
}
