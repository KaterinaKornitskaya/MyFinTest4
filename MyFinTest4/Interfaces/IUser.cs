using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Interfaces
{
    internal interface IUser
    {
        public bool Registration(string fname, string lname, string email, string password,
            int age, string address);
        public bool Autorization(string email, string password);
    }
}
