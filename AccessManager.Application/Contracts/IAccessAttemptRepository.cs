using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Entities;

namespace AccessManager.Application.Contracts
{
    public interface IAccessAttemptRepository
    {
        IEnumerable<AccessAttempt> GetAllAccessAttempts();
        void SaveAccessAttempt(AccessAttempt accessAttempt);
    }
}
