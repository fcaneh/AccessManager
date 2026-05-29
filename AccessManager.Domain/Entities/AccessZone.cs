using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Enums;


namespace AccessManager.Domain.Entities
{
    public class AccessZone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public AccessLevel RequiredAccessLevel { get; set; }
    }
}
