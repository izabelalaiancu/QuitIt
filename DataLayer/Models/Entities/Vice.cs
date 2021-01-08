using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Entities
{
    public class Vice : BaseEntity
    {
        public string Name { get; set; }
        public List<UserVice> UserVices { get; set; }
        public Vice()
        {
            UserVices = new List<UserVice>();
        }
    }

}
