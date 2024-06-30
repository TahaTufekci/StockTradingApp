using StockTradingApp.Models;
using System.Collections.Generic;

namespace StockTradingApp.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int userID);
        User GetUser(string password);
        ICollection<User> GetUsers();
        bool UserExists(int userID);
    }
}
