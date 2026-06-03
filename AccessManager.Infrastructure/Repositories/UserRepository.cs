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

        public void ToggleStatusUser(User user)
        {
            // Aucune action nécessaire.
            // User est déjà une référence vers l'objet stocké dans la collection.
            // Dans une implémentation EF Core ou SQL, cette méthode effectuerait la persistance.
        }
    }
}
