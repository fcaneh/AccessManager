using System;
using AccessManager.Application.Contracts;
using AccessManager.Application.Features.Access.CheckAccess;
using AccessManager.Application.Features.AccessAttemps.GetAllAccessAttemps;
using AccessManager.Application.Features.Users.CreateUser;
using AccessManager.Application.Features.Users.GetAllUsers;
using AccessManager.Application.Features.Users.GetUserByBadgeNumber;
using AccessManager.Domain.Entities;
using AccessManager.Infrastructure.Repositories;
using AccessManager.Domain.Enums;
using AccessManager.Application.Features.Users.DisableUser;
using AccessManager.Application.Features.Statistics;
using AccessManager.Application.Features.AccessAttemps.GetAccessAttemptsByBadgeNumberQuery;

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

            var getAllAccessAttempsHandler =
                new GetAllAccessAttemptsHandler(accessAttemptRepository);

            var checkAccessHandler =
                new CheckAccessHandler(
                    userRepository,
                    accessZoneRepository,
                    accessAttemptRepository);

            var getAllUsersHandler =
                new GetAllUsersHandler(userRepository);

            var getUserByBadgeNumberHandler =
                new GetUserByBadgeNumberHandler(userRepository);

            var createUserHandler =
                new CreateUserHandler(userRepository);

            var disableUserHandler =
                new DisableUserHandler(userRepository);

            var getStatisticsHandler =
                new GetStatisticsHandler(userRepository, accessAttemptRepository);

            var getAccessAttemptsByBadgeNumberHandler =
                new GetAccessAttemptsByBadgeNumberHandler(accessAttemptRepository);

            ShowMenu(
                checkAccessHandler,
                getAllUsersHandler,
                getUserByBadgeNumberHandler,
                accessZoneRepository,
                getAllAccessAttempsHandler,
                createUserHandler,
                disableUserHandler,
                getStatisticsHandler,
                getAccessAttemptsByBadgeNumberHandler);

        }

        private static void ShowMenu(
            CheckAccessHandler checkAccessHandler,
            GetAllUsersHandler getAllUsersHandler,
            GetUserByBadgeNumberHandler getUserByBadgeNumberHandler,
            IAccessZoneRepository accessZoneRepository,
            GetAllAccessAttemptsHandler getAllAccessAttempsHandler,
            CreateUserHandler createUserHandler,
            DisableUserHandler disableUserHandler,
            GetStatisticsHandler getStatisticsHandler,
            GetAccessAttemptsByBadgeNumberHandler getAccessAttemptsByBadgeNumberHandler
           )
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Menu ---");
                Console.WriteLine("1. Vérifier l'accès");
                Console.WriteLine("2. Afficher tous les utilisateurs");
                Console.WriteLine("3. Rechercher un utilisateur");
                Console.WriteLine("4. Afficher les demandes d'accès");
                Console.WriteLine("5. Créer un utilisateur");
                Console.WriteLine("6. Désactiver un utilisateur");
                Console.WriteLine("7. Afficher les statistiques");
                Console.WriteLine("8. Afficher l'historique d'accès d'un utilisateur");
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

                    case "4":
                        GetAllAccessAttempts(getAllAccessAttempsHandler);
                        break;

                    case "5":
                        CreateUser(createUserHandler);
                        break;

                    case "6":
                        DisableUser(
                            getAllUsersHandler,
                            disableUserHandler);
                        break;

                    case "7":
                        ShowStatistics(getStatisticsHandler);
                        break;

                    case "8":
                    AccessAttemptsByBadgeNumber(getAccessAttemptsByBadgeNumberHandler, getAllUsersHandler);
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

        private static void GetAllUsers(GetAllUsersHandler getAllUsersHandler)
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

        private static void GetUserByBadgeNumber(GetUserByBadgeNumberHandler getUserByBadgeNumberHandler)
        {
            Console.Write("--- Numéro de badge : ");

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

        private static void GetAllAccessAttempts(GetAllAccessAttemptsHandler getAllAccessAttemptsHandler)
        {
            var response = getAllAccessAttemptsHandler.Handle(new GetAllAccessAttemptsQuery());
            Console.WriteLine("--- Demandes d'accès ---");

            foreach (var attempt in response.AccessAttempts)
            {
                Console.WriteLine(
                                        $"Badge : {attempt.BadgeNumber} | " +
                                        $"Zone : {attempt.ZoneName} | " +
                                        $"ZoneID : {attempt.AccessZoneId} | " +
                                        $"Time : {attempt.AttemptTime} | " +
                                        $"Niveau : {attempt.AccessResult}");
            }
        }

        private static void CreateUser(CreateUserHandler createUserHandler)
        {
            Console.WriteLine();
            Console.WriteLine("--- Créer un utilisateur ---");
            Console.Write("Prénom : ");
            var firstName = Console.ReadLine();
            Console.Write("Nom : ");
            var lastName = Console.ReadLine();
            Console.Write("Numéro de badge : ");
            var badgeNumber = Console.ReadLine();

            Console.WriteLine("Niveau d'accès : ");
            foreach (var level in Enum.GetValues<AccessLevel>())
            {
                Console.WriteLine($"{(int)level} - {level}");
            }
            var accessLevelInput = Console.ReadLine();
            if (!int.TryParse(accessLevelInput, out var accessLevelInt) || !Enum.IsDefined(typeof(AccessLevel), accessLevelInt))
            {
                Console.WriteLine("Niveau d'accès invalide.");
                return;
            }
            var accessLevel = (AccessLevel)accessLevelInt;
            var command = new CreateUserCommand
            {
                FirstName = firstName ?? string.Empty,
                LastName = lastName ?? string.Empty,
                BadgeNumber = badgeNumber ?? string.Empty,
                AccessLevel = accessLevel
            };
            var response = createUserHandler.Handle(command);
            Console.WriteLine(response.Message);
        }

        private static void DisableUser(GetAllUsersHandler getAllUsersHandler, DisableUserHandler disableUserHandler)
        {
            Console.WriteLine();
            Console.WriteLine("--- Désactiver un utilisateur ---");

            Console.WriteLine();
            Console.WriteLine("--- Utilisateurs disponibles ---");

            var users = getAllUsersHandler.Handle(new GetAllUsersQuery());

            foreach (var user in users.Users)
            {
                Console.WriteLine(
                    $"{user.FirstName} {user.LastName} | " +
                    $"Badge : {user.BadgeNumber} ");
            }

            Console.Write("Numéro de badge : ");
            var badgeNumber = Console.ReadLine();
            var command = new DisableUserCommand
            {
                BadgeNumber = badgeNumber ?? string.Empty
            };
            var response = disableUserHandler.Handle(command);
            Console.WriteLine(response.Message);
        }

        private static void ShowStatistics(GetStatisticsHandler getStatisticsHandler)
        {
            var response = getStatisticsHandler.Handle(new GetStatisticsQuery());
            Console.WriteLine();
            Console.WriteLine("--- Statistiques ---");
            Console.WriteLine($"Nombre total d'utilisateurs : {response.UsersCount}");
            Console.WriteLine($"Nombre total d'utilisateurs actifs: {response.ActiveUsersCount}");
            Console.WriteLine($"Nombre total d'utilisateurs inactifs: {response.InactiveUsersCount}");
            Console.WriteLine($"Nombre total de demandes d'accès : {response.TotalAccessAttempts}");
            Console.WriteLine($"Nombre total d'accès autorisés : {response.GrantedAccessAttempts}");
            Console.WriteLine($"Nombre total d'accès refusés : {response.DeniedAccessAttempts}");
            Console.WriteLine($"Zone ayant reçu le plus de demandes d'accès : {response.MostUsedZone}");
        }

        private static void AccessAttemptsByBadgeNumber(GetAccessAttemptsByBadgeNumberHandler getAccessAttemptsByBadgeNumberHandler, GetAllUsersHandler getAllUsersHandler)
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
            Console.Write("--- Numéro de badge : ");
            var badgeNumber = Console.ReadLine();

            var query = new GetAccessAttemptsByBadgeNumberQuery
            {
                BadgeNumber = badgeNumber ?? string.Empty
            };

            var response = getAccessAttemptsByBadgeNumberHandler.Handle(query);

            if (response == null)
            {
                Console.WriteLine("Utilisateur non trouvé.");
                return;
            }

            foreach(var attempt in response.AccessAttempts)
            {
                Console.WriteLine(
                    $"Zone : {attempt.ZoneName} | " +
                    $"Time : {attempt.AttemptTime} | " +
                    $"Résultat : {attempt.AccessResult}");
            };                
        }
    }
}