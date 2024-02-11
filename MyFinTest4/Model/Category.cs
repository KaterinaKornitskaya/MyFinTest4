using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Model
{
    internal class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public IEnumerable<MyTransaction>? MyTransactions { get; set; }

        public static List<Category> TestData()
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
            return list;
        }

        public override string ToString()
        {
            return ($"category: {Name}\n");
        }
    }
}
