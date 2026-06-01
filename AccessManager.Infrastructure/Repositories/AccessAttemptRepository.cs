using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;
using AccessManager.Domain.Entities;
using AccessManager.Infrastructure.Seeders;

namespace AccessManager.Infrastructure.Repositories
{
    public class AccessAttemptRepository : IAccessAttemptRepository
    {
        private readonly List<AccessAttempt> _accessAttempts = DemoData.AccessAttempts;

        public IEnumerable<AccessAttempt> GetAllAccessAttempts()
        {
            return _accessAttempts;
        }

        public void SaveAccessAttempt(AccessAttempt accessAttempt)
        {
            _accessAttempts.Add(accessAttempt);
        }
    }
}
