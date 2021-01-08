using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Models.Entities
{
    public class Role : IdentityRole
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }
}