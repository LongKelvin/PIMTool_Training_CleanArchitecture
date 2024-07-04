using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Infrastructure.Data.DataContext;

namespace ProjectManagement.Infrastructure.Data.SampleData
{
    public static class SampleData
    {
        private static readonly Random random = new();

        public static void InitializeData(PIMToolDbContext context)
        {
            CreateSampleData(context);
        }

        private static void CreateSampleData(PIMToolDbContext context)
        {
            CreateSampleEmployeeData(context);
            CreateSampleGroupData(context);
            CreateSampleProjectData(context);
        }

        private static void CreateSampleEmployeeData(PIMToolDbContext context)
        {
            if (!context.Employees.Any())
            {
                var listEmployees = new List<Employee>
                {
                    new() { Visa = "ABC", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1) },
                    new(){ Visa = "DEF", FirstName = "Jane", LastName = "Smith", BirthDate = new DateTime(1985, 5, 23) },
                    new() { Visa = "GHI", FirstName = "Mike", LastName = "Johnson", BirthDate = new DateTime(1978, 8, 12) },
                     new() { Visa = "JKL", FirstName = "Emily", LastName = "Davis", BirthDate = new DateTime(1982, 3, 15) },
                     new() { Visa = "MNO", FirstName = "David", LastName = "Wilson", BirthDate = new DateTime(1995, 11, 30) },
                     new() { Visa = "PQR", FirstName = "Sophia", LastName = "Martinez", BirthDate = new DateTime(1988, 7, 7) }
                };

                context.Employees.AddRange(listEmployees);
                context.SaveChanges();
            }
        }

        private static void CreateSampleGroupData(PIMToolDbContext context)
        {
            if (!context.Groups.Any())
            {
                if (!context.Employees.Any())
                {
                    CreateSampleEmployeeData(context);
                }

                var listEmployees = context.Employees.ToList();
                var listGroups = new List<Group>
                {
                    new() { GroupLeaderId = listEmployees[0].Id, Name = "Phoenix", GroupLeader = listEmployees[0] },
                    new() { GroupLeaderId = listEmployees[1].Id, Name = "Atlas Vanguard" , GroupLeader = listEmployees[1]},
                    new() { GroupLeaderId = listEmployees[2].Id, Name = "Wolfpack Collective" , GroupLeader = listEmployees[2]},
                    new() { GroupLeaderId = listEmployees[3].Id, Name = "Nexus" , GroupLeader = listEmployees[3]},
                    new() { GroupLeaderId = listEmployees[4].Id, Name = "Spark" , GroupLeader = listEmployees[4]}
                };

                context.Groups.AddRange(listGroups);
                context.SaveChanges();
            }
        }

        private static void CreateSampleProjectData(PIMToolDbContext context)
        {
            if (!context.Projects.Any())
            {
                if (!context.Employees.Any())
                {
                    CreateSampleEmployeeData(context);
                }

                if (!context.Groups.Any())
                {
                    CreateSampleGroupData(context);
                }

                var listGroups = context.Groups.ToList();
                var listProjects = new List<Project>();

                for (int i = 0; i < 10; i++)
                {
                    var groupId = listGroups[random.Next(listGroups.Count)].Id;
                    var project = new Project
                    {
                        Id = Guid.NewGuid(),
                        GroupId = groupId,
                        Group = listGroups.Find(x => x.Id.Equals(groupId)),
                        ProjectNumber = 1000 + i,
                        Name = $"{GetProjectName(i)}",
                        Customer = $"{GetCustomerName(i)}",
                        Status = ((ProjectStatus)random.Next(0, 4)).ToString(),
                        StartDate = GenerateRandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31)),
                        EndDate = i % 2 == 0 ? (DateTime?)null : GenerateRandomDate(new DateTime(2024, 6, 1), new DateTime(2025, 12, 31))
                    };

                    // Assign random employees to the project
                    var listEmployees = context.Employees.ToList();
                    int numberOfEmployees = random.Next(1, 10);
                    for (int j = 0; j < numberOfEmployees; j++)
                    {
                        var employee = listEmployees[random.Next(listEmployees.Count)];
                        project!.Employees?.Add(employee);
                    }

                    listProjects.Add(project);
                }

                context.Projects.AddRange(listProjects);
                context.SaveChanges();
            }
        }

        private static string GetProjectName(int index)
        {
            var projectNames = new[] { "Apollo", "Hermes", "Zeus", "Athena", "Poseidon", "Artemis", "Ares", "Hera", "Demeter", "Hades" };
            return projectNames[index % projectNames.Length];
        }

        private static string GetCustomerName(int index)
        {
            var customerNames = new[] { "Acme Corp", "Globex Inc", "Soylent Corp", "Initech", "Umbrella Corp", "Wayne Enterprises", "Stark Industries", "Oscorp", "LexCorp", "Aperture Science" };
            return customerNames[index % customerNames.Length];
        }

        private static DateTime GenerateRandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}