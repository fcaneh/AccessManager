using AccessManager.Application.Contracts;
using AccessManager.Application.Features.Access.CheckAccess;
using AccessManager.Application.Features.AccessAttempts.GetAccessAttemptsByBadgeNumber;
using AccessManager.Application.Features.AccessAttempts.GetAllAccessAttempts;
using AccessManager.Application.Features.Statistics;
using AccessManager.Application.Features.Users.CreateUser;
using AccessManager.Application.Features.Users.DisableUser;
using AccessManager.Application.Features.Users.GetAllUsers;
using AccessManager.Application.Features.Users.GetUserByBadgeNumber;
using AccessManager.Domain.Enums;
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

            var getAllAccessAttemptsHandler =
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
                getAllAccessAttemptsHandler,
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
            GetAllAccessAttemptsHandler getAllAccessAttemptsHandler,
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
                Console.WriteLine("1. Simuler une demande d'accès");
                Console.WriteLine("2. Accéder au menu Utilisateurs");
                Console.WriteLine("3. Accéder au menu des demandes d'accès");
                Console.WriteLine("4. Afficher les statistiques");
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
                        ShowUsersMenu(getAllUsersHandler, getUserByBadgeNumberHandler, createUserHandler, disableUserHandler);
                        break;


                    case "3":
                        ShowAccessAttemptsMenu(getAllAccessAttemptsHandler, getAccessAttemptsByBadgeNumberHandler, getAllUsersHandler);
                        break;

                    case "4":
                        ShowStatistics(getStatisticsHandler);
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
            DisplayUsers(getAllUsersHandler);

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
            DisplayUsers(getAllUsersHandler);
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
            DisplayUsers(getAllUsersHandler);

            Console.WriteLine();
            Console.WriteLine("--- Désactiver un utilisateur ---");

            Console.Write("Numéro de badge : ");
            var badgeNumber = Console.ReadLine();
            var command = new DisableUserCommand
            {
                BadgeNumber = badgeNumber ?? string.Empty
            };
            var response = disableUserHandler.Handle(command);
            Console.WriteLine(response.Message);
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


        private static void AccessAttemptsByBadgeNumber(GetAccessAttemptsByBadgeNumberHandler getAccessAttemptsByBadgeNumberHandler, GetAllUsersHandler getAllUsersHandler)
        {
            DisplayUsers(getAllUsersHandler);

            Console.Write("--- Numéro de badge : ");
            var badgeNumber = Console.ReadLine();

            var query = new GetAccessAttemptsByBadgeNumberQuery
            {
                BadgeNumber = badgeNumber ?? string.Empty
            };

            var response = getAccessAttemptsByBadgeNumberHandler.Handle(query);

            if (!response.AccessAttempts.Any())
            {
                Console.WriteLine("Aucune demande d'accès trouvée");
                return;
            }

            foreach (var attempt in response.AccessAttempts)
            {
                Console.WriteLine(
                    $"Zone : {attempt.ZoneName} | " +
                    $"Time : {attempt.AttemptTime} | " +
                    $"Résultat : {attempt.AccessResult}");
            }
            ;
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

        private static void DisplayUsers(GetAllUsersHandler getAllUsersHandler)
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
        }

        private static void ShowUsersMenu(GetAllUsersHandler getAllUsersHandler, GetUserByBadgeNumberHandler getUserByBadgeNumberHandler, CreateUserHandler createUserHandler, DisableUserHandler disableUserHandler)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Menu Utilisateurs ---");
                Console.WriteLine("1. Afficher tous les utilisateurs");
                Console.WriteLine("2. Rechercher un utilisateur par numéro de badge");
                Console.WriteLine("3. Créer un nouvel utilisateur");
                Console.WriteLine("4. Désactiver un utilisateur");
                Console.WriteLine("0. Retour au menu principal");
                Console.Write("Choisissez une option : ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GetAllUsers(getAllUsersHandler);
                        break;
                    case "2":
                        GetUserByBadgeNumber(getUserByBadgeNumberHandler);
                        break;
                    case "3":
                        CreateUser(createUserHandler);
                        break;
                    case "4":
                        DisableUser(getAllUsersHandler, disableUserHandler);
                        break;

                    case "0":
                        return;
                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }
            }
        }

        private static void ShowAccessAttemptsMenu(GetAllAccessAttemptsHandler getAllAccessAttemptsHandler, GetAccessAttemptsByBadgeNumberHandler getAccessAttemptsByBadgeNumberHandler, GetAllUsersHandler getAllUsersHandler)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Menu Demandes d'accès ---");
                Console.WriteLine("1. Afficher toutes les demandes d'accès");
                Console.WriteLine("2. Afficher les demandes d'accès par numéro de badge");
                Console.WriteLine("0. Retour au menu principal");

                Console.Write("Choisissez une option : ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GetAllAccessAttempts(getAllAccessAttemptsHandler);
                        break;
                    case "2":
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
    }
}