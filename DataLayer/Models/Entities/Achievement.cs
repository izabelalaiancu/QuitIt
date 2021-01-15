using System.Collections.Generic;

namespace DataLayer.Models.Entities
{
    public class Achievement : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Reached { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}