using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Model
{
    internal class MyTransaction
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Sum { get; set; }
        public DateTime dateTime { get; set; }
        public string? MyTrType { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public override string ToString()
        {
            return ($"title: {Name}\n" +
                $"description: {Description}\n" +
                $"sum = {Sum}\n" +
                $"type = {MyTrType}\n" +
                $"date: {dateTime}");
        }
    }
}
