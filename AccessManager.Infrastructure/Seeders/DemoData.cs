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
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Martin",
                BadgeNumber = "EMP001",
                AccessLevel = AccessLevel.Employee,
                IsActive = true
            },

            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Manager",
                BadgeNumber = "MGR001",
                AccessLevel = AccessLevel.Manager,
                IsActive = true
            },

            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Charles",
                LastName = "Admin",
                BadgeNumber = "ADM001",
                AccessLevel = AccessLevel.Administrator,
                IsActive = true
            },

            new User
            {
                Id= Guid.NewGuid(),
                FirstName = "Diana",
                LastName = "Technician",
                BadgeNumber = "TECH001",
                AccessLevel= AccessLevel.Technician,
                IsActive= true
            },

            new User
            {
                Id = Guid.NewGuid(),
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
    }
}
