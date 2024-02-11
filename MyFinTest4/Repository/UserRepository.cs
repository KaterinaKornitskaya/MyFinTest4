using MyFinTest4.Data;
using MyFinTest4.Helpers;
using MyFinTest4.Model;
using MyFinTest4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Repository
{
    internal class UserRepository : IUser
    {
        public bool Registration(string fname, string lname, string email, string password,
            int age, string address)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                if (db.Users.Any(e => e.Email.Equals(email)))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("User with the such email already exist.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }

                string salt = SecurityHelper.GenerateSalt(12363);
                string hashedPassword = SecurityHelper.HashPassword(password, salt, 12363, 70);

                User user = new User
                {
                    FirstName = fname,
                    LastName = lname,
                    Email = email,
                    HashedPassword = hashedPassword,
                    SaltForHash = salt,
                    UserSetting = new UserSettings
                    {
                        Age = age,
                        Address = address
                    }
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Autorization(string email, string password)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                var currUser = db.Users.FirstOrDefault(e => e.Email.Equals(email));
                if (currUser != null)
                {
                    string hashedPassword = SecurityHelper.HashPassword
                        (password, currUser.SaltForHash, 12363, 70);

                    if (currUser.HashedPassword.Equals(hashedPassword))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
