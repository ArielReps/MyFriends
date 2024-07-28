using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.DAL
{
    public class DataLayer : DbContext
    {
        
        public DataLayer(string connectionString) : base(GetOptions(connectionString))
        {
            Database.EnsureCreated();
            Seed();
        }

        private void Seed()
        {
           
            if (Friends.Count() > 0) return;
         
            Friend friend = new Friend
            {
                FirstName = "אריאל",
                LastName = "אוחנה",
                Email = "aa@aa.com",
                PhoneNumber = "052-1234567"
            };
            Friends.Add(friend);
            SaveChanges();
            
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            //SqlServerDbContextOptionsExtensions
            return new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .Options;
        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}