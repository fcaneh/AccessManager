using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Entities;

namespace AccessManager.Application.Contracts
{
    public interface IAccessZoneRepository
    {
        IEnumerable<AccessZone> GetAllZones();
        AccessZone? GetZoneById(Guid zoneId);
    }
}
