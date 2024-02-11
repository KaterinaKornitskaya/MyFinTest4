using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public string? SaltForHash { get; set; }
        public UserSettings? UserSetting { get; set; }
        public IEnumerable<MyTransaction>? MyTransactions { get; set; }
    }
}
