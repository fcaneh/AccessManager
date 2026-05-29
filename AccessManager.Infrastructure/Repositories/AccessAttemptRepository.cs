using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;
using AccessManager.Domain.Entities;

namespace AccessManager.Infrastructure.Repositories
{
    public class AccessAttemptRepository : IAccessAttemptRepository
    {
        private readonly List<AccessAttempt> _accesAttempts = new();

        public IEnumerable<AccessAttempt> GetAllAccessAttempt()
        {
            throw new NotImplementedException();
        }

        public void SaveAccessAttempt(AccessAttempt accessAttempt)
        {
            throw new NotImplementedException();
        }
    }
}
