using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Entities;

namespace AccessManager.Application.Features.AccessAttemps.GetAllAccessAttemps
{
    public record GetAllAccessAttempsResponse
    {
        public IEnumerable<AccessAttempt> AccessAttempts { get; init; } = new List<AccessAttempt>();
    }
}
