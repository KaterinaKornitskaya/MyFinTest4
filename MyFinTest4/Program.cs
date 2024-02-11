using Microsoft.Identity.Client;
using MyFinTest4.Data;
using MyFinTest4.Model;
using MyFinTest4.Repository;

namespace MyFinTest4
{
    internal class Program
    {
        static UserRepository userRep;
        static TransactionRepository trRep;
        public static ApplicationContext DbContext()
        {
            return new ApplicationContextFactory().CreateDbContext();
        }

        
        static void Main(string[] args)
        {
            userRep = new UserRepository();

            using (ApplicationContext db = DbContext())
            {
                if (!db.Categories.Any())
                {
                    List<Category> list = new List<Category>
                    {
                        new Category { Name = "Food"},
                        new Category { Name = "House"},
                        new Category { Name = "Work"},
                        new Category { Name = "Salary"},
                        new Category { Name = "Pets"},
                        new Category { Name = "Restaurant"},
                        new Category { Name = "Etc"}
                    };
                    db.Categories.AddRange(list);
                    db.SaveChanges();
                }
            }

            while (true)
            {
                Console.WriteLine("1.Register. \n2.Login. \n0.Exit.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Enter Email:");
                            string eMail = Console.ReadLine();
                            Console.WriteLine("Enter password:");
                            string password = Console.ReadLine();
                            Console.WriteLine("Enter First Name:");
                            string fName = Console.ReadLine();
                            Console.WriteLine("Enter Last Name:");
                            string lName = Console.ReadLine();
                            Console.WriteLine("Enter your age:");
                            int age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Address:");
                            string address = Console.ReadLine();

                            if (userRep.Registration(fName, lName, eMail, password, age, address))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Great, You was registered!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error Registration.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Enter userName:");
                            string userName = Console.ReadLine();
                            Console.WriteLine("Enter password:");
                            string password = Console.ReadLine();

                            if (userRep.Autorization(userName, password))
                            {
                                MainMenu();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error Autorization.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        }
                    case "0":
                        {
                            return;
                        }
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Not valid input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        enum TrTypeEnum
        {
            Income,
            Outcome
        }

        public static void MainMenu()
        {
            string a = TrTypeEnum.Income.ToString();

            trRep = new TransactionRepository();
            Console.WriteLine("Main menu:");
            while (true)
            {
                Console.WriteLine("1.Add transaction." +
                    "\n2.Show all my transactions." +
                    "\n3.Show balance." +
                    "\n4.Filter by category." +
                    "\n5.Filter by date." +
                    "\n0.Exit.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Adding of Transaction:");
                            
                            Console.WriteLine("Enter transaction title:");
                            string tName = Console.ReadLine();
                            Console.WriteLine("Enter transaction description:");
                            string tDesc = Console.ReadLine();
                            Console.WriteLine("Enter sum of transaction:");
                            double tSum = Convert.ToInt64(Console.ReadLine());
                            Console.WriteLine("Enter type of transaction: " +
                                "income - press 1, outcome - press2");
                            int tType = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter category of transaction: " +
                                "food - press 1, house - press 2, work - press 3, salary - press 4," +
                                "pets - press 5, restaurant - press 6, etc - press 7.");
                            int cat = Convert.ToInt32(Console.ReadLine());

                            switch(tType)
                            {
                                case 1:
                                    {
                                        string type = TrTypeEnum.Income.ToString();
                                        trRep.AddTransaction(tName, tDesc, tSum, type, cat);
                                        break;
                                    }
                                case 2:
                                    {
                                        string type = TrTypeEnum.Outcome.ToString();
                                        trRep.AddTransaction(tName, tDesc, tSum, type, cat);
                                        break;
                                    }
                                default:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Not valid input");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }
                            break;
                        }
                    case "2":
                        {
                            trRep.ShowAllMyTransaction();
                            break;
                        }
                    case "3":
                        {
                            trRep.ShowBalance(TrTypeEnum.Income.ToString(), TrTypeEnum.Outcome.ToString());
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Enter type of transactions you want to filter: " +
                                "income - press 1, outcome - press2");
                            int tType = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Choose the category you want filter by:\n" +
                                "food - press 1, house - press 2, work - press 3, salary - press 4,\n " +
                                "pets - press 5, restaurant - press 6, etc - press 7.");
                            int cat = Convert.ToInt32(Console.ReadLine());

                            switch (tType)
                            {
                                case 1:
                                    {
                                        string type = TrTypeEnum.Income.ToString();
                                        trRep.ShowPerCategory(TrTypeEnum.Income.ToString(), cat);
                                        break;
                                    }
                                case 2:
                                    {
                                        string type = TrTypeEnum.Outcome.ToString();
                                        trRep.ShowPerCategory(TrTypeEnum.Outcome.ToString(), cat);
                                        break;
                                    }
                                default:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Not valid input");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Start date:");
                            Console.WriteLine("Enter start year (like this: 2024 ):");
                            int sYear = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter start month (like this: 05 ):");
                            int sMonth = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter start day (like this: 05 ):");
                            int sDay = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("End date:");
                            Console.WriteLine("Enter end year (like this: 2024 ):");
                            int eYear = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter end month (like this: 05 ):");
                            int eMonth = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter end day (like this: 05 ):");
                            int eDay = Convert.ToInt32(Console.ReadLine());

                            DateTime stDate = new DateTime(sYear, sMonth, sDay);
                            DateTime eDate = new DateTime(eYear, eMonth, eDay);
                           
                            trRep.ShowAllPerDate(stDate, eDate);
                            break;
                        }
                    case "0":
                        {
                            return;
                        }
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Not valid input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                }
            }
        }
    }
}
