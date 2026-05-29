using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Entities;

namespace AccessManager.Application.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserByBadgeNumber(string badgeNumber);

    }
}
