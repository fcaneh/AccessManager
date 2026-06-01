using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.Users.CreateUser
{
    public class CreateUserHandler
    {
        public readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CreateUserResponse Handle(CreateUserCommand command)
        {
            var existingUser = _userRepository.GetUserByBadgeNumber(command.BadgeNumber);

            if (existingUser != null)
            {
                return new CreateUserResponse
                {
                    Success = false,
                    Message = "Un utilisateur avec ce numéro de badge existe déjà"
                };
            }

            var newUser = new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                BadgeNumber = command.BadgeNumber,
                AccessLevel = command.AccessLevel,
                IsActive = true
            };

            _userRepository.CreateUser(newUser);
            
            return new CreateUserResponse
            {
                Success = true,
                Message = "Utilisateur créé avec succès"
            };
        }
    }
}
