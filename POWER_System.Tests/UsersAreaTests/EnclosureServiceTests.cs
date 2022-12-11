using Microsoft.Extensions.DependencyInjection;
using POWER_System.Data.Repositories;
using POWER_System.Services.Contracts;
using POWER_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POWER_System.Models;
using POWER_System.Services.Models;

namespace POWER_System.Tests.UsersAreaTests
{
    public class EnclosureServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IProjectService, ProjectService>()
                .AddSingleton<IEnclosureService, EnclosureService>()
                .AddSingleton<IOrderService, OrderService>()
                .AddSingleton<IPartService, PartService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task NewEnclosureAddedToProject()
        {
            var projectId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001");

            var enclosure = new EnclosureServiceModel()
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Location = "Location",
                Plant = "Plane",
                Revision = "R01",
                Status = Models.Enum.EnclosureStatus.Mounted,
                Tag = "NewEnclosure",
                Comment = "Comment"
            };

            var service = serviceProvider.GetService<IEnclosureService>();

            Assert.DoesNotThrowAsync(async () => await service.AddProjectEnclosureAsync(enclosure));
        }

        [Test]
        public async Task EnclosureWithExistingTagIsNotAddedToProject()
        {
            var projectId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001");

            var enclosure = new EnclosureServiceModel()
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Location = "Location",
                Plant = "Plane",
                Revision = "R01",
                Status = Models.Enum.EnclosureStatus.Mounted,
                Tag = "Tag",
                Comment = "Comment"
            };

            var service = serviceProvider.GetService<IEnclosureService>();

            Assert.CatchAsync<ArgumentException>(
                async () => await service.AddProjectEnclosureAsync(enclosure),
                "Enclosure with that Tag already exists.");
        }

        [Test]
        public async Task ReturnsAllEnclosuresForProject()
        {
            var projectId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001");

            var service = serviceProvider.GetService<IEnclosureService>();

            var status = await service.GetAllEnclosuresForProjectAsync(projectId);

            Assert.True(status.Count() == 2);
        }

        [Test]
        public async Task ReturnsSpecificEnclosureWithParts()
        {
            var service = serviceProvider.GetService<IEnclosureService>();

            var id = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a");

            var enclosure = await service.EnclosureSummarizedDetails(id);

            Assert.That(enclosure != null);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var project = new Project()
            {
                Id = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001"),
                Number = "P010203",
                Contractor = "ViK",
                DateCreated = DateTime.Now,
                Description = "Description",
                Name = "Pompena stanciq iztok",
                Status = Models.Enum.ProjectStatus.BasicEngineering
            };

            var project2 = new Project()
            {
                Id = Guid.Parse("74e1bee8-6804-4689-ad1c-d0dbde3cadc9"),
                Number = "P123456",
                Contractor = "Energoto",
                DateCreated = DateTime.Now,
                Description = "Description",
                Name = "AEC Kozloduy pompi",
                Status = Models.Enum.ProjectStatus.BasicEngineering
            };

            var project3 = new Project()
            {
                Id = Guid.Parse("790e252f-c9eb-4af2-85d3-e150d3baa25a"),
                Number = "P125678",
                Contractor = "Pizza Palermo",
                DateCreated = DateTime.Now,
                Description = "Description",
                Name = "Da hapnem po edna pizza?",
                Status = Models.Enum.ProjectStatus.BasicEngineering
            };

            var enclosure = new Enclosure()
            {
                Project = project,
                ProjectId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001"),
                Status = Models.Enum.EnclosureStatus.Mounted,
                DateCreated = DateTime.Now,
                Id = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a"),
                Location = "Location",
                Plant = "Plant",
                Revision = "R0",
                Tag = "Tag"
            };

            var enclosure2 = new Enclosure()
            {
                Project = project,
                ProjectId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001"),
                Status = Models.Enum.EnclosureStatus.Mounted,
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Location = "Location",
                Plant = "Plant",
                Revision = "R0",
                Tag = "Mag"
            };

            var enclosure3 = new Enclosure()
            {
                Project = project2,
                ProjectId = Guid.Parse("74e1bee8-6804-4689-ad1c-d0dbde3cadc9"),
                Status = Models.Enum.EnclosureStatus.Mounted,
                DateCreated = DateTime.Now,
                Id = Guid.Parse("813e48e2-9db6-4f22-b7a4-d568db93cf1e"),
                Location = "Location",
                Plant = "Plant",
                Revision = "R0",
                Tag = "Tag"
            };

            var part1 = new Part()
            {
                Id = 1,
                Manufacturer = "Sie",
                OrderNumber = "1",
                Description = "Desc",
                Measure = "",
                Quantity = 1,
                Delivery = Models.Enum.OrderDelivery.ACS
            };

            var part2 = new Part()
            {
                Id = 2,
                Manufacturer = "Sie",
                OrderNumber = "2",
                Description = "Desc",
                Measure = "",
                Quantity = 1,
                Delivery = Models.Enum.OrderDelivery.ACS
            };

            var partsList = new List<EnclosurePart>()
            {
                new EnclosurePart()
                {
                    Id = 1,
                    Delivery = Models.Enum.OrderDelivery.ACS,
                    Enclosure = enclosure,
                    EnclosureId = enclosure.Id,
                    Quantity = 1,
                    Tag = "PartOne",
                    PartId = 1,
                    Part = part1,
                },

                new EnclosurePart()
                {
                    Id = 2,
                    Delivery = Models.Enum.OrderDelivery.ACS,
                    Enclosure = enclosure,
                    EnclosureId = enclosure.Id,
                    Quantity = 1,
                    Tag = "PartTwo",
                    PartId = 2,
                    Part = part2,
                }
            };

            var order = new PartOrder()
            {
                Id = Guid.Parse("16a1743b-869f-4bb4-9059-b165430866e0"),
                DateCreated = DateTime.Now,
                Enclosure = enclosure,
                EnclosureId = enclosure.Id,
                OrderDate = DateTime.Now,
                Status = Models.Enum.OrderStatus.Delivered
            };

            await repo.AddAsync(project);
            await repo.AddAsync(project2);
            await repo.AddAsync(project3);
            await repo.AddAsync(enclosure);
            await repo.AddAsync(enclosure2);
            await repo.AddAsync(enclosure3);
            await repo.AddAsync(part1);
            await repo.AddAsync(part2);
            await repo.AddAsync(order);
            await repo.SaveChangesAsync();
        }
    }
}
