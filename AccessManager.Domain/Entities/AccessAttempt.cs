using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Enums;

namespace AccessManager.Domain.Entities
{
    public class AccessAttempt
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }
        public string BadgeNumber { get; set; } = string.Empty;
        public Guid AccessZoneId { get; set; }
        public string ZoneName { get; set; }
        public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
        public AccessResult AccessResult { get; set; }
    }
}
