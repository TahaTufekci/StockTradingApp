using Microsoft.EntityFrameworkCore;
using StockTradingApp.Data;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUser(int userID)
        {
            return _context.Users.Include(u => u.Portfolio).Include(u => u.Watchlist).ThenInclude(w => w.Stocks).FirstOrDefault(u => u.UserID == userID);
        }

        public User GetUser(string password)
        {
            return _context.Users.FirstOrDefault(u => u.Password == password);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(int userID)
        {
            return _context.Users.Any(u => u.UserID == userID);
        }
    }
}
