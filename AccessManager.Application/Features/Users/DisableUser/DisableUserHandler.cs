using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.Users.DisableUser
{
    public  class DisableUserHandler
    {
        public readonly IUserRepository _userRepository;

        public DisableUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public DisableUserResponse Handle(DisableUserCommand command)
        {
            var user = _userRepository.GetUserByBadgeNumber(command.BadgeNumber);
            if (user == null)
            {
                return new DisableUserResponse
                {
                    Success = false,
                    Message = "Utilisateur non trouvé"
                };
            }

            if (!user.IsActive)
            {
                return new DisableUserResponse
                {
                    Success = false,
                    Message = "L'utilisateur est déjà désactivé"
                };
            }

            user.IsActive = false;
            _userRepository.DisableUser(user);
            return new DisableUserResponse
            {
                Success = true,
                Message = "Utilisateur désactivé avec succès"
            };
        }
    }
}
