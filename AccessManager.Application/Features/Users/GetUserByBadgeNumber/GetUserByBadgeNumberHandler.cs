using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.Users.GetUserByBadgeNumber
{
    public class GetUserByBadgeNumberHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByBadgeNumberHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUserByBadgeNumberResponse Handle(GetUserByBadgeNumberQuery query)
        {
            var user = _userRepository.GetUserByBadgeNumber(query.BadgeNumber);
            return new GetUserByBadgeNumberResponse
            {
                User = user
            };
        }
    }
}
