using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUserName(string Username);
        IEnumerable<User> SearchUsers(string searchTerm, int page, int recordSize);
        bool LockedOutUser(int ID);
        int SearchUsersCount(string searchTerm);
    }
}
