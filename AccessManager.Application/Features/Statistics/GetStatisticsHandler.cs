using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;
using AccessManager.Domain.Enums;

namespace AccessManager.Application.Features.Statistics
{
    public class GetStatisticsHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessAttemptRepository _attemptRepository;

        public GetStatisticsHandler(IUserRepository userRepository, IAccessAttemptRepository attemptRepository)
        {
            _userRepository = userRepository;
            _attemptRepository = attemptRepository;
        }


        public GetStatisticsResponse Handle(GetStatisticsQuery  query)
        {
            var users = _userRepository.GetAllUsers().ToList();
            var accessAttempts = _attemptRepository.GetAllAccessAttempts().ToList();

            return new GetStatisticsResponse
            {
                UsersCount = users.Count,
                ActiveUsersCount = users.Count(user => user.IsActive),
                InactiveUsersCount = users.Count(user => !user.IsActive),
                TotalAccessAttempts = accessAttempts.Count,
                GrantedAccessAttempts = accessAttempts.Count(accessAttempt => accessAttempt.AccessResult == AccessResult.Granted),
                DeniedAccessAttempts = accessAttempts.Count(accessAttempt => accessAttempt.AccessResult != AccessResult.Granted),
                MostUsedZone = accessAttempts
                    .GroupBy(attempt => attempt.ZoneName)
                    .OrderByDescending(attempt => attempt.Count())
                    .FirstOrDefault()?.Key ?? "N/A"
            };
        }
    }
}
