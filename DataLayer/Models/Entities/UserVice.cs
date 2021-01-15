using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Entities
{
   public class UserVice : BaseEntity
    {
        public string UserId { get; set; }
        public string ViceId { get; set; }
        public double Money { get; set; }
        public double Score { get; set; }
        public User User { get; set; }
        public Vice Vice { get; set; }
    }
}
