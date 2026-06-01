using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;
using AccessManager.Domain.Entities;
using AccessManager.Infrastructure.Seeders;

namespace AccessManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly List<User> _users = DemoData.Users;

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserByBadgeNumber(string badgeNumber)
        {
            return _users.FirstOrDefault(u => u.BadgeNumber == badgeNumber);
        }

        public void CreateUser(User user)
        {
            _users.Add(user);
        }

        public void DisableUser(User user)
        {
            // Version qui serait nécessaire avec une copie des objets :
            //
            // var existingUser = _users.FirstOrDefault(
            //     u => u.BadgeNumber == user.BadgeNumber);
            //
            // if (existingUser != null)
            // {
            //     existingUser.IsActive = false;
            // }
            // ainsi que dans le cas d'une base de données, il faudrait faire une mise à jour de l'entité.
        }
    }
}
