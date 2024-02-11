using Microsoft.EntityFrameworkCore;
using MyFinTest4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinTest4.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<MyTransaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //    Database.EnsureDeleted();
            //    Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modBuilder)
        {
            modBuilder.Entity<User>()
                .HasOne(u => u.UserSetting)
                .WithOne(u => u.User)
                .HasForeignKey<UserSettings>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modBuilder.Entity<MyTransaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.MyTransactions)
                .HasForeignKey(t => t.UserId);

            modBuilder.Entity<MyTransaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.MyTransactions)
                .HasForeignKey(t => t.CategoryId);

            modBuilder.Entity<User>().Property(e => e.Email).IsRequired();
            modBuilder.Entity<User>().Property(e => e.HashedPassword).IsRequired();
            modBuilder.Entity<User>().Property(e => e.SaltForHash).IsRequired();

            modBuilder.Entity<User>().HasAlternateKey(e => e.Email);
        }
    }
}
