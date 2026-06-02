using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.AccessAttempts.GetAllAccessAttempts
{
    public class GetAllAccessAttemptsHandler
    {
        private readonly IAccessAttemptRepository _accessAttemptRepository;

        public GetAllAccessAttemptsHandler(IAccessAttemptRepository accessAttemptRepository)
        {
            _accessAttemptRepository = accessAttemptRepository;
        }

        public GetAllAccessAttemptsResponse Handle(GetAllAccessAttemptsQuery request)
        {
            return new GetAllAccessAttemptsResponse
            {
                AccessAttempts = _accessAttemptRepository.GetAllAccessAttempts()
            };
        }
    }
}
