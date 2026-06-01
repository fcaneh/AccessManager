using System;
using AccessManager.Application.Contracts;
using AccessManager.Application.Features.Access.CheckAccess;
using AccessManager.Application.Features.Users.GetAllUsers;
using AccessManager.Application.Features.Users.GetUserByBadgeNumber;
using AccessManager.Infrastructure.Repositories;

namespace AccessManager.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Access Manager Console Application started");

            var userRepository = new UserRepository();
            var accessZoneRepository = new AccessZoneRepository();
            var accessAttemptRepository = new AccessAttemptRepository();

            var checkAccessHandler =
                new CheckAccessHandler(
                    userRepository,
                    accessZoneRepository,
                    accessAttemptRepository);

            var getAllUsersHandler =
                new GetAllUsersHandler(userRepository);

            var getUserByBadgeNumberHandler =
                new GetUserByBadgeNumberHandler(userRepository);

            ShowMenu(
                checkAccessHandler,
                getAllUsersHandler,
                getUserByBadgeNumberHandler,
                accessZoneRepository);
        }

        private static void ShowMenu(
            CheckAccessHandler checkAccessHandler,
            GetAllUsersHandler getAllUsersHandler,
            GetUserByBadgeNumberHandler getUserByBadgeNumberHandler,
            IAccessZoneRepository accessZoneRepository)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Menu ---");
                Console.WriteLine("1. Vérifier l'accès");
                Console.WriteLine("2. Afficher tous les utilisateurs");
                Console.WriteLine("3. Rechercher un utilisateur");
                Console.WriteLine("0. Quitter");
                Console.Write("Choisissez une option : ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckAccess(
                            checkAccessHandler,
                            getAllUsersHandler,
                            accessZoneRepository);
                        break;

                    case "2":
                        GetAllUsers(getAllUsersHandler);
                        break;

                    case "3":
                        GetUserByBadgeNumber(getUserByBadgeNumberHandler);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }
            }
        }

        private static void CheckAccess(
            CheckAccessHandler checkAccessHandler,
            GetAllUsersHandler getAllUsersHandler,
            IAccessZoneRepository accessZoneRepository)
        {
            Console.WriteLine();
            Console.WriteLine("--- Utilisateurs disponibles ---");

            var usersResponse = getAllUsersHandler.Handle(new GetAllUsersQuery());

            foreach (var user in usersResponse.Users)
            {
                Console.WriteLine(
                    $"{user.FirstName} {user.LastName} | " +
                    $"Badge : {user.BadgeNumber} | " +
                    $"Niveau : {user.AccessLevel}");
            }

            Console.WriteLine();
            Console.WriteLine("--- Zones disponibles ---");

            foreach (var zone in accessZoneRepository.GetAllZones())
            {
                Console.WriteLine(
                    $"{zone.Name} | " +
                    $"Id : {zone.Id} | " +
                    $"Niveau requis : {zone.RequiredAccessLevel}");
            }

            Console.WriteLine();

            Console.Write("Badge : ");
            var badgeNumber = Console.ReadLine();

            Console.Write("Zone Id : ");
            var zoneIdInput = Console.ReadLine();

            if (!Guid.TryParse(zoneIdInput, out var zoneId))
            {
                Console.WriteLine("Zone Id invalide.");
                return;
            }

            var query = new CheckAccessQuery
            {
                BadgeNumber = badgeNumber ?? string.Empty,
                ZoneId = zoneId
            };

            var response = checkAccessHandler.Handle(query);

            Console.WriteLine();
            Console.WriteLine($"Résultat : {response.Result}");
        }

        private static void GetAllUsers(
            GetAllUsersHandler getAllUsersHandler)
        {
            var response =
                getAllUsersHandler.Handle(
                    new GetAllUsersQuery());

            Console.WriteLine();
            Console.WriteLine("--- Utilisateurs ---");

            foreach (var user in response.Users)
            {
                Console.WriteLine(
                    $"{user.FirstName} {user.LastName} | " +
                    $"Badge : {user.BadgeNumber} | " +
                    $"Niveau : {user.AccessLevel}");
            }
        }

        private static void GetUserByBadgeNumber(
            GetUserByBadgeNumberHandler getUserByBadgeNumberHandler)
        {
            Console.Write("Numéro de badge : ");

            var badgeNumber = Console.ReadLine();

            var query = new GetUserByBadgeNumberQuery
            {
                BadgeNumber = badgeNumber ?? string.Empty
            };

            var response =
                getUserByBadgeNumberHandler.Handle(query);

            if (response.User == null)
            {
                Console.WriteLine("Utilisateur non trouvé.");
                return;
            }

            var user = response.User;

            Console.WriteLine();
            Console.WriteLine("Utilisateur trouvé :");
            Console.WriteLine($"{user.FirstName} {user.LastName}");
            Console.WriteLine($"Badge : {user.BadgeNumber}");
            Console.WriteLine($"Niveau : {user.AccessLevel}");
            Console.WriteLine($"Statut : {(user.IsActive ? "Actif" : "Inactif")}");
        }
    }
}