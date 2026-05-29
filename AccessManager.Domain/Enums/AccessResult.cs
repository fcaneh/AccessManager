using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManager.Domain.Enums
{
    public enum AccessResult
    {
        Granted,
        UserNotFound,
        BadgeDisabled,
        InsufficientAccessLevel,
        ZoneNotFound
    }
}
