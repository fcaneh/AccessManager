using AccessManager.Application.Contracts;
using AccessManager.Domain.Entities;
using AccessManager.Domain.Enums;

namespace AccessManager.Application.Features.Access.CheckAccess
{
    public class CheckAccessHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessZoneRepository _accessZoneRepository;
        private readonly IAccessAttemptRepository _accessAttemptRepository;

        public CheckAccessHandler(
            IUserRepository userRepository,
            IAccessZoneRepository accessZoneRepository,
            IAccessAttemptRepository accessAttemptRepository
            )
        {
            _userRepository = userRepository;
            _accessZoneRepository = accessZoneRepository;
            _accessAttemptRepository = accessAttemptRepository;
        }

        public CheckAccessResponse Handle(CheckAccessQuery request)
        {
            var accessAttempt = new AccessAttempt
            {
                AccessZoneId = request.ZoneId,
                BadgeNumber = request.BadgeNumber,
                AttemptTime = DateTime.UtcNow
            };


            var zone = _accessZoneRepository.GetZoneById(request.ZoneId);
            if (zone == null)
            {
                accessAttempt.ZoneName = "Unknown Zone";
                accessAttempt.AccessResult = AccessResult.ZoneNotFound;
                return SaveAndReturn(accessAttempt);
            }

            accessAttempt.ZoneName = zone.Name;
            accessAttempt.AccessZoneId = zone.Id;

            var user = _userRepository.GetUserByBadgeNumber(request.BadgeNumber);

            if (user == null)
            {
                accessAttempt.AccessResult = AccessResult.UserNotFound;
                return SaveAndReturn(accessAttempt);
            }
            accessAttempt.UserId = user.Id;

            if (!user.IsActive)
            {
                accessAttempt.AccessResult = AccessResult.BadgeDisabled;
                return SaveAndReturn(accessAttempt);
            }


            if (user.AccessLevel < zone.RequiredAccessLevel)
            {
                accessAttempt.AccessResult = AccessResult.InsufficientAccessLevel;
                return SaveAndReturn(accessAttempt);
            }


            accessAttempt.AccessResult = AccessResult.Granted;
            return SaveAndReturn(accessAttempt);
        }

        private CheckAccessResponse SaveAndReturn(AccessAttempt accessAttempt)
        {
            _accessAttemptRepository.SaveAccessAttempt(accessAttempt);
            return new CheckAccessResponse { Result = accessAttempt.AccessResult };
        }
    }
}
