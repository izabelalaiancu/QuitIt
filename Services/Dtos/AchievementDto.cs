using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class AchievementDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Reached { get; set; }
    }
}
