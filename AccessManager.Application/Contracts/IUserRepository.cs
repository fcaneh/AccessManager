using AccessManager.Domain.Entities;

namespace AccessManager.Application.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserByBadgeNumber(string badgeNumber);
        void CreateUser(User user);

        void DisableUser(User user);

    }
}
