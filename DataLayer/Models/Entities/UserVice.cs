using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Entities
{
   public class UserVice : BaseEntity
    {
        public string UserId { get; set; }
        public string ViceId { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public Vice Vice { get; set; }
    }
}
