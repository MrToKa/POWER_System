using Microsoft.Extensions.DependencyInjection;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Models.Contracts;
using POWER_System.Services;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POWER_System.Tests.UsersAreaTests
{
    internal class ProjectServiceTest
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
        public void ProjectWithNumberAlreadyExists()
        {
            var project = new ProjectServiceModel()
            {
                Id = Guid.NewGuid(),
                Number = "P010203",
                Name = "Pompi",
                Contractor = "ViK",
                Status = Models.Enum.ProjectStatus.BasicEngineering.ToString()
            };

            var service = serviceProvider.GetService<IProjectService>();

            Assert.CatchAsync<ArgumentException>(
                async () => await service.AddProjectAsync(project),
                "Project with that name already exists.");
        }

        [Test]
        public async Task NewProjectIsAddedToDB()
        {
            var project = new ProjectServiceModel()
            {
                Id = Guid.NewGuid(),
                Number = "P010210",
                Name = "Pompi",
                Contractor = "ViK",
                Status = Models.Enum.ProjectStatus.BasicEngineering.ToString()
            };

            var service = serviceProvider.GetService<IProjectService>();

            Assert.DoesNotThrowAsync(async () => await service.AddProjectAsync(project));
        }

        [Test]
        public async Task ReturnsAllProjects()
        {
            var service = serviceProvider.GetService<IProjectService>();

            var status = await service.GetAllProjectsAsync();

            Assert.True(status.Count() == 3);
        }

        [Test]
        public async Task ReturnsAskedProjects()
        {
            var service = serviceProvider.GetService<IProjectService>();

            var id = Guid.Parse("33e7b28f-ff37-4921-90f9-8e1161c17001");

            var project = await service.GetProjectAsync(id);

            Assert.That(project != null);
        }

        [Test]
        public async Task FailsToReturnUnknownProjects()
        {
            var service = serviceProvider.GetService<IProjectService>();

            var id = Guid.Parse("fd2f21e2-c710-4a28-a97c-c119bd37e6a5");

            Assert.CatchAsync<ArgumentException>(
                async () => await service.GetProjectAsync(id),
                "Project does not exists.");
        }

        [Test]
        public async Task SearchForProjectByName()
        {
            var service = serviceProvider.GetService<IProjectService>();

            string keyword = "stanciq";

            var projects = await service.SearchProjectAsync(keyword);

            Assert.True(projects.Count() == 1);
        }

        [Test]
        public async Task SearchForSeveralProjectByShortName()
        {
            var service = serviceProvider.GetService<IProjectService>();

            string keyword = "pomp";

            var projects = await service.SearchProjectAsync(keyword);

            Assert.True(projects.Count() == 2);
        }

        [Test]
        public async Task SearchreturnsAllProjectsIfKeywordIsNullOrEmpty()
        {
            var service = serviceProvider.GetService<IProjectService>();

            string keyword = "";

            var projects = await service.SearchProjectAsync(keyword);

            Assert.True(projects.Count() == 3);
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
            await repo.AddAsync(order);
            await repo.SaveChangesAsync();
        }
    }
}
