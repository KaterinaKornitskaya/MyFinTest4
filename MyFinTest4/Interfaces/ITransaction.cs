using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Interfaces
{
    internal interface ITransaction
    {
        public void AddTransaction(string tName, string tDescription,
           double tSum, string type, int cat);

        public void ShowAllMyTransaction();

        public void ShowBalance(string inc, string outc);

        public void ShowPerCategory(string type, int cat);

        public void ShowAllPerDate(DateTime dStart, DateTime dEnd);
    }
}
