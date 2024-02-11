using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Model
{
    internal class UserSettings
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
