using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Application.Contracts;
using AccessManager.Domain.Entities;
using AccessManager.Infrastructure.Seeders;

namespace AccessManager.Infrastructure.Repositories
{
    public class AccessZoneRepository : IAccessZoneRepository
    {
        private readonly List<AccessZone> _accessZones = DemoData.Zones;

        public IEnumerable<AccessZone> GetAllZones()
        {
            return _accessZones;
        }

        public AccessZone? GetZoneById(Guid zoneId)
        {
            return _accessZones.FirstOrDefault(x => x.Id == zoneId);
        }
    }
}
