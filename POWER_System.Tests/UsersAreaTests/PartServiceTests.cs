using Microsoft.Extensions.DependencyInjection;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Tests.UsersAreaTests
{
    public class PartServiceTests
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
        public async Task GetDetailedPartsForEnclosure()
        {
            var service = serviceProvider.GetService<IPartService>();

            var id = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a");

            var status = await service.GetDetailedPartsForEnclosuresAsync(id);

            Assert.True(status.Select(p => p.Quantity).Sum() == 3);
        }

        [Test]
        public async Task GetSummarizedPartsForEnclosure()
        {
            var service = serviceProvider.GetService<IPartService>();

            var id = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a");

            var status = await service.GetSummarizedPartsForEnclosuresAsync(id);

            Assert.True(status.Count() == 2);
        }

        [Test]
        public async Task PartsAreAssignedToProject()
        {
            var service = serviceProvider.GetService<IPartService>();

            var id = Guid.Parse("0a746acd-0999-4fba-839b-ce5e79df4e78");

            var partsList = new List<PartServiceModel>()
            {
                new PartServiceModel()
                {
                    Manufacturer = "Sie",
                    OrderNumber = "1",
                    Quantity = 1,
                    Description = "Desc",
                    DeviceTag = "TagOne"
                },
                new PartServiceModel()
                {
                    Manufacturer = "Sie",
                    OrderNumber = "2",
                    Quantity = 4,
                    Description = "Desc",
                    DeviceTag = "TagTwo"
                }
            };

            var status = await service.AssignPartsToEnclosure(partsList, id);

            Assert.True(status.Count() == 2);
        }

        [Test]
        public async Task ExactNumberOfPartsAreAssignedToProject()
        {
            var service = serviceProvider.GetService<IPartService>();

            var id = Guid.Parse("0a746acd-0999-4fba-839b-ce5e79df4e78");

            var partsList = new List<PartServiceModel>()
            {
                new PartServiceModel()
                {
                    Manufacturer = "Sie",
                    OrderNumber = "1",
                    Quantity = 1,
                    Description = "Desc",
                    DeviceTag = "TagOne"
                },
                new PartServiceModel()
                {
                    Manufacturer = "Sie",
                    OrderNumber = "2",
                    Quantity = 4,
                    Description = "Desc",
                    DeviceTag = "TagTwo"
                }
            };

            var status = await service.AssignPartsToEnclosure(partsList, id);

            Assert.True(status.Select(p => p.Quantity).Sum() == 5);
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
                Tag = "Tag",
                Parts = new List<EnclosurePart>()
                {
                    new EnclosurePart()
                    {
                    Id = 1,
                    Delivery = Models.Enum.OrderDelivery.ACS,
                    EnclosureId = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a"),
                    Quantity = 1,
                    Tag = "PartOne",
                    PartId = 1,
                    Part = part1,
                    },

                    new EnclosurePart()
                    {
                    Id = 2,
                    Delivery = Models.Enum.OrderDelivery.ACS,
                    EnclosureId = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a"),
                    Quantity = 2,
                    Tag = "PartTwo",
                    PartId = 2,
                    Part = part2,
                    },
                }
            };

            var enclosure2 = new Enclosure()
            {
                Project = project,
                ProjectId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001"),
                Status = Models.Enum.EnclosureStatus.Mounted,
                DateCreated = DateTime.Now,
                Id = Guid.Parse("0a746acd-0999-4fba-839b-ce5e79df4e78"),
                Location = "Location",
                Plant = "Plant",
                Revision = "R0",
                Tag = "Mag"
            };

            var enclosure3 = new Enclosure()
            {
                Project = project,
                ProjectId = Guid.Parse("74e1bee8-6804-4689-ad1c-d0dbde3cadc9"),
                Status = Models.Enum.EnclosureStatus.Mounted,
                DateCreated = DateTime.Now,
                Id = Guid.Parse("813e48e2-9db6-4f22-b7a4-d568db93cf1e"),
                Location = "Location",
                Plant = "Plant",
                Revision = "R0",
                Tag = "Tag"
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
