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
                var listEmployees = new List<Employee>();

                for (int i = 0; i < 100; i++)
                {
                    listEmployees.Add(new Employee
                    {
                        Visa = GenerateRandomString(3),
                        FirstName = $"FirstName{i}",
                        LastName = $"LastName{i}",
                        BirthDate = GenerateRandomDate(new DateTime(1980, 1, 1),
                        new DateTime(2000, 1, 1))
                    });
                }

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
                var listGroups = new List<Group>();

                for (int i = 0; i < 10; i++)
                {
                    listGroups.Add(new Group
                    {
                        GroupLeaderId = listEmployees[random.Next(listEmployees.Count)].Id,
                        Name = $"Group{i}"
                    });
                }

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

                for (int i = 0; i < 50; i++)
                {
                    listProjects.Add(new Project
                    {
                        GroupId = listGroups[random.Next(listGroups.Count)].Id,
                        ProjectNumber = 1000 + i,
                        Name = $"Project {i}",
                        Customer = $"Customer {i}",
                        Status = ((ProjectStatus)random.Next(0, 3)).ToString(),
                        StartDate = GenerateRandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31)),
                        EndDate = i % 2 == 0 ? (DateTime?)null : GenerateRandomDate(new DateTime(2024, 6, 1), new DateTime(2025, 12, 31))
                    });
                }

                context.Projects.AddRange(listProjects);
                context.SaveChanges();
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static DateTime GenerateRandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}