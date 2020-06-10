using Microsoft.AspNetCore.Identity;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Services
{
    public class UserRepository : IUserRepository
    {
        private ShopDbContext _ctx;
        private readonly UserManager<User> _userManager;
        public UserRepository(ShopDbContext ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;

        }
        public User GetUserByUserName(string Username)
        {
            return _ctx.Users.FirstOrDefault(x => x.UserName == Username);
        }

        public bool LockedOutUser(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchUsers(string searchTerm, int page, int recordSize)
        {
            var users = _userManager.Users.Where(x=>x.Email != "admin@gmail.com").AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(searchTerm.ToLower()));
            }


            var skip = (page - 1) * recordSize;
            // skip  = (1    - 1) * 3 = 0 * 3 = 0
            // skip  = (2    - 1) * 3 = 1 * 3 = 3
            // skip  = (3    - 1) * 3 = 2 * 3 = 6

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchUsersCount(string searchTerm)
        {
            var users = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(searchTerm.ToLower()));
            }


            return users.Count();
        }
    }
}
