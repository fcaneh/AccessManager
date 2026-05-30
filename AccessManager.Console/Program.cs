using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Infrastructure.Repositories;
using AccessManager.Application.Contracts;
using AccessManager.Application.Features.Access.CheckAccess;

namespace AccessManager.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Access Manager Console Application started");

            var userRepository = new UserRepository();
            var accessZoneRepository = new AccessZoneRepository();
            var accessAttemptRepository = new AccessAttemptRepository();

            var handler = new CheckAccessHandler(userRepository, accessZoneRepository, accessAttemptRepository);

            var query = new CheckAccessQuery
            {
                BadgeNumber = "EMP001",
                ZoneId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            };

            var response = handler.Handle(query);

            Console.WriteLine($"Résultat : {response.Result}");
        }
    }
}
