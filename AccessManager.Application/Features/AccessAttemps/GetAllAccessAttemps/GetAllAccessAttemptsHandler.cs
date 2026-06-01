using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.AccessAttemps.GetAllAccessAttemps
{
    public class GetAllAccessAttemptsHandler
    {
        private readonly IAccessAttemptRepository _accessAttemptRepository;

        public GetAllAccessAttemptsHandler(IAccessAttemptRepository accessAttemptRepository)
        {
            _accessAttemptRepository = accessAttemptRepository;
        }

        public GetAllAccessAttempsResponse Handle(GetAllAccessAttemptsQuery request)
        {
            return new GetAllAccessAttempsResponse
            {
                AccessAttempts = _accessAttemptRepository.GetAllAccessAttempts()
            };
        }
    }
}
