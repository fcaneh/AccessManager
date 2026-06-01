using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Entities;
using AccessManager.Domain.Enums;

namespace AccessManager.Infrastructure.Seeders
{
    public static class DemoData
    {
        public static List<User> Users = new()
        {
            new User
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                FirstName = "Alice",
                LastName = "Martin",
                BadgeNumber = "EMP001",
                AccessLevel = AccessLevel.Employee,
                IsActive = true
            },

            new User
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                FirstName = "Bob",
                LastName = "Manager",
                BadgeNumber = "MGR001",
                AccessLevel = AccessLevel.Manager,
                IsActive = true
            },

            new User
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                FirstName = "Charles",
                LastName = "Admin",
                BadgeNumber = "ADM001",
                AccessLevel = AccessLevel.Administrator,
                IsActive = true
            },

            new User
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                FirstName = "Diana",
                LastName = "Technician",
                BadgeNumber = "TECH001",
                AccessLevel= AccessLevel.Technician,
                IsActive= true
            },

            new User
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                FirstName = "Eve",
                LastName = "Visitor",
                BadgeNumber = "VIS001",
                AccessLevel = AccessLevel.Visitor,
                IsActive = true
            }
        };

        public static List<AccessZone> Zones = new()
        {
            new AccessZone
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Accueil",
                RequiredAccessLevel = AccessLevel.Employee
            },

            new AccessZone
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Salle Serveur",
                RequiredAccessLevel = AccessLevel.Administrator
            },

            new AccessZone
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Bureau de Direction",
                RequiredAccessLevel = AccessLevel.Manager
            },

            new AccessZone
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Atelier Technique",
                RequiredAccessLevel = AccessLevel.Technician
            }
        };


        public static readonly List<AccessAttempt> AccessAttempts = new()
        {
            new AccessAttempt
            {
                UserId = Users.First(x => x.BadgeNumber == "EMP001").Id,
                BadgeNumber = "EMP001",
                AccessZoneId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ZoneName = "Accueil",
                AttemptTime = DateTime.UtcNow.AddMinutes(-45),
                AccessResult = AccessResult.Granted
            },

            new AccessAttempt
            {
                UserId = Users.First(x => x.BadgeNumber == "MGR001").Id,
                BadgeNumber = "MGR001",
                AccessZoneId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ZoneName = "Bureau de Direction",
                AttemptTime = DateTime.UtcNow.AddMinutes(-40),
                AccessResult = AccessResult.Granted
            },

            new AccessAttempt
            {
                UserId = Users.First(x => x.BadgeNumber == "VIS001").Id,
                BadgeNumber = "VIS001",
                AccessZoneId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ZoneName = "Accueil",
                AttemptTime = DateTime.UtcNow.AddMinutes(-35),
                AccessResult = AccessResult.InsufficientAccessLevel
            },

            new AccessAttempt
            {
                UserId = Users.First(x => x.BadgeNumber == "EMP001").Id,
                BadgeNumber = "EMP001",
                AccessZoneId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ZoneName = "Salle Serveur",
                AttemptTime = DateTime.UtcNow.AddMinutes(-30),
                AccessResult = AccessResult.InsufficientAccessLevel
            },

            new AccessAttempt
            {
                UserId = Users.First(x => x.BadgeNumber == "ADM001").Id,
                BadgeNumber = "ADM001",
                AccessZoneId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ZoneName = "Salle Serveur",
                AttemptTime = DateTime.UtcNow.AddMinutes(-25),
                AccessResult = AccessResult.Granted
            },

            new AccessAttempt
            {
                UserId = null,
                BadgeNumber = "BADGE999",
                AccessZoneId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ZoneName = "Accueil",
                AttemptTime = DateTime.UtcNow.AddMinutes(-20),
                AccessResult = AccessResult.UserNotFound
            },

            new AccessAttempt
            {
                UserId = Users.First(x => x.BadgeNumber == "TECH001").Id,
                BadgeNumber = "TECH001",
                AccessZoneId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ZoneName = "Atelier Technique",
                AttemptTime = DateTime.UtcNow.AddMinutes(-15),
                AccessResult = AccessResult.Granted
            }
        };
    }
}
