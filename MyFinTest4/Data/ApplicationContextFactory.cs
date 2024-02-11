using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Data
{
    internal class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        private static IConfigurationRoot config;

        static ApplicationContextFactory()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            config = builder.Build();
        }

        public ApplicationContext CreateDbContext(string[]? args = null)
        {
            // считываем строку из полученного билда
            var options = new DbContextOptionsBuilder<ApplicationContext>();
            // достаем строку подключения
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new ApplicationContext(options.Options);
            // именно этот метод будет создавать ApplicationContext
        }
    }
}
