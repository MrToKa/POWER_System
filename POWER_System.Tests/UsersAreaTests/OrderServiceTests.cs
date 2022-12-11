using Microsoft.Extensions.DependencyInjection;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Tests.UsersAreaTests
{
    public class OrderServiceTests
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
        public async Task NewPartsAreAddedToOrder()
        {
            var id = Guid.Parse("395bd5a1-b494-473a-ab66-975fe899ec5a");

            var service = serviceProvider.GetService<IOrderService>();

            var partsList = new List<PartServiceModel>()
            {
                new PartServiceModel()
                {
                    Manufacturer = "Sie",
                    OrderNumber = "1",
                    Quantity = 10,
                    Description = "Desc",
                    DeviceTag = "TagOne"
                },
                new PartServiceModel()
                {
                    Manufacturer = "Sie",
                    OrderNumber = "2",
                    Quantity = 10,
                    Description = "Desc",
                    DeviceTag = "TagTwo"
                }
            };

            Assert.DoesNotThrowAsync(async () => await service.CreatePartsOrder(partsList, id));
        }

        [Test]
        public async Task ReturnsAskedOrder()
        {
            var service = serviceProvider.GetService<IOrderService>();

            var enclosureId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001");
            var orderId = "16a1743b-869f-4bb4-9059-b165430866e0";

            var order = await service.GetOrderAsync(enclosureId, orderId);

            Assert.That(order != null);
        }

        [Test]
        public async Task ReturnsAllOrdersForProject()
        {
            var service = serviceProvider.GetService<IOrderService>();

            var enclosureId = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001");

            var status = await service.GetAllOrdersForProjectsAsync(enclosureId);

            Assert.True(status.Count() == 1);
        }

        [Test]
        public async Task ReturnsAllOrders()
        {
            var service = serviceProvider.GetService<IOrderService>();

            var status = await service.GetAllOrders();

            Assert.True(status.Count() == 2);
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
                Project = project2,
                ProjectId = Guid.Parse("74e1bee8-6804-4689-ad1c-d0dbde3cadc9"),
                Status = Models.Enum.EnclosureStatus.Mounted,
                DateCreated = DateTime.Now,
                Id = Guid.Parse("0a746acd-0999-4fba-839b-ce5e79df4e78"),
                Location = "Location",
                Plant = "Plant",
                Revision = "R0",
                Tag = "Mag"
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

            var order2 = new PartOrder()
            {
                Id = Guid.Parse("a0fd007a-4aa6-4435-8ba4-ff2a299500a3"),
                DateCreated = DateTime.Now,
                Enclosure = enclosure2,
                EnclosureId = enclosure2.Id,
                OrderDate = DateTime.Now,
                Status = Models.Enum.OrderStatus.Delivered
            };

            await repo.AddAsync(project);
            await repo.AddAsync(enclosure);
            await repo.AddAsync(enclosure2);
            await repo.AddAsync(part1);
            await repo.AddAsync(part2);
            await repo.AddAsync(order);
            await repo.AddAsync(order2);
            await repo.SaveChangesAsync();
        }
    }
}
