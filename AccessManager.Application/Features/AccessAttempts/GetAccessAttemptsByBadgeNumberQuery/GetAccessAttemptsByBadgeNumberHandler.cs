using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.AccessAttempts.GetAccessAttemptsByBadgeNumber
{
    public class GetAccessAttemptsByBadgeNumberHandler
    {
        private readonly IAccessAttemptRepository _accessAttemptRepository;

        public GetAccessAttemptsByBadgeNumberHandler(IAccessAttemptRepository accessAttemptRepository)
        {
            _accessAttemptRepository = accessAttemptRepository;
        }

        public GetAccessAttemptsByBadgeNumberResponse Handle(GetAccessAttemptsByBadgeNumberQuery query)
        {
            var accessAttempts = _accessAttemptRepository.GetAllAccessAttempts().Where(accessAttemp => accessAttemp.BadgeNumber == query.BadgeNumber);

            return new GetAccessAttemptsByBadgeNumberResponse
            {
                AccessAttempts = accessAttempts
            };
        }
    }
}
