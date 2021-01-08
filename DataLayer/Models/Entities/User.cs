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
        public List<Notification> Notifications { get; set; }
        public List<UserVice> UserVices { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            Notifications = new List<Notification>();
            UserVices = new List<UserVice>();
        }
    }
}
