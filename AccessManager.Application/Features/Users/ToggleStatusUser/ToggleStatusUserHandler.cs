using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.Users.ToggleStatusUser
{
    public class ToggleStatusUserHandler
    {
        public readonly IUserRepository _userRepository;

        public ToggleStatusUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ToggleStatusUserResponse Handle(ToggleStatusUserCommand command)
        {
            var user = _userRepository.GetUserByBadgeNumber(command.BadgeNumber);
            if (user == null)
            {
                return new ToggleStatusUserResponse
                {
                    Success = false,
                    Message = "Utilisateur non trouvé"
                };
            }
            
            user.IsActive =  !user.IsActive;
            _userRepository.ToggleStatusUser(user);

            return new ToggleStatusUserResponse
            {
                Success = true,
                Message = user.IsActive ? "Utilisateur réactivé avec succès" : "Utilisateur désactivé avec succès"
            };
            
        }
    }
}
