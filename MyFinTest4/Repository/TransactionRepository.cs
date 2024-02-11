using Microsoft.EntityFrameworkCore;
using MyFinTest4.Data;
using MyFinTest4.Interfaces;
using MyFinTest4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Repository
{
    internal class TransactionRepository : ITransaction
    {
        // добавление транзакции
        public void AddTransaction(string tName, string tDescription, double tSum, 
            string type, int cat)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                Console.WriteLine("To add transaction  - enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                UserRepository userRep = new UserRepository();
                if (userRep.Autorization(email, password) == true)
                {
                    MyTransaction trans = new MyTransaction
                    {
                        Name = tName,
                        Description = tDescription,
                        Sum = tSum,
                        dateTime = DateTime.Now,
                        MyTrType = type,
                        UserId = db.Users.FirstOrDefault(u => u.Email.Equals(email)).Id,
                        CategoryId = cat
                    };
                    db.Transactions.Add(trans);
                    db.SaveChanges();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("not authorized");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // вывод всех транзакций на экран
        public void ShowAllMyTransaction()
        {
            using (ApplicationContext db = Program.DbContext())
            {
                Console.WriteLine("To show all transaction enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                UserRepository userRep = new UserRepository();
                if (userRep.Autorization(email, password) == true)
                {
                    var currUser = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();

                    var list = db.Transactions.Where(t => t.UserId==currUser.Id).Include(t => t.Category).ToList();

                    if(list.Count > 0)
                    {
                        int count = 0;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var t in list)
                        {
                            count++;
                            Console.WriteLine($"Transaction #{count}:\n{t.ToString()}\n{t.Category.ToString()}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("No transactions.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
            }
        }

        // вывод баланса
        public void ShowBalance(string inc, string outc)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                Console.WriteLine("To show balance enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                UserRepository userRep = new UserRepository();
                //Console.WriteLine("For incomes - press 1:");

                if (userRep.Autorization(email, password) == true)
                {
                    var currUser = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();

                    double sumIncomes = db.Transactions.Where(t => t.UserId==currUser.Id).Where(t => t.MyTrType==inc).Sum(t => t.Sum);
                    double sumOutcomes = db.Transactions.Where(t => t.UserId==currUser.Id).Where(t => t.MyTrType==outc).Sum(t => t.Sum);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Sum of incomes = {sumIncomes}");
                    Console.WriteLine($"Sum of outcomes = {sumOutcomes}");
                    Console.WriteLine($"Balance = {sumIncomes-sumOutcomes}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // вывод транзакций по выбранному типу по выбранной категории
        public void ShowPerCategory(string type, int cat)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                Console.WriteLine("To show balance enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                UserRepository userRep = new UserRepository();
                //Console.WriteLine("For incomes - press 1:");

                if (userRep.Autorization(email, password) == true)
                {
                    var currUser = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
                    var list = db.Transactions.Where(t => t.UserId==currUser.Id)
                        .Where(t=>t.MyTrType==type)
                        .Include(t => t.Category).Where(t=>t.CategoryId==cat).ToList();

                    if(list.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("No transactions.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        int count = 0;
                        foreach (var t in list)
                        {
                            count++;
                            Console.WriteLine($"Transaction #{count}:\n{t.ToString()}\n{t.Category.ToString()}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        // вывод транзакций по дате (дата от...дата до)
        public void ShowAllPerDate(DateTime dStart, DateTime dEnd)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                Console.WriteLine("To show all transaction enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                UserRepository userRep = new UserRepository();
                if (userRep.Autorization(email, password) == true)
                {
                    var currUser = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();

                    var list = db.Transactions.Where(t => t.UserId==currUser.Id)
                        .Where(t=>t.dateTime >= dStart && t.dateTime <= dEnd)
                        .Include(t => t.Category).ToList();

                    if(list.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        int count = 0;
                        foreach (var t in list)
                        {
                            count++;
                            Console.WriteLine($"Transaction #{count}:\n{t.ToString()}\n{t.Category.ToString()}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("No transactions.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
            }
        }
    }
}
