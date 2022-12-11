using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using POWER_System.Areas.Admin.Services;
using POWER_System.Areas.Admin.Services.Contracts;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Models.Enum;
using POWER_System.Tests.UsersAreaTests;

namespace POWER_System.Tests.AdminAreaTests
{
    public class UserServiceTests
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
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        //[Test]
        //public async Task AssignRoleToUser()
        //{
        //    var service = serviceProvider.GetService<IUserService>();            

        //    var user = service;
           
        //    Assert.That(user != null);
        //}


        [Test]
        public async Task GetUserByIdForEdit()
        {
            var service = serviceProvider.GetService<IUserService>();

            var userId = "1";

            var user = await service.GetUserById(userId);

            Assert.That(user != null);
        }


        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var role1 = new IdentityRole()
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "Stamp"
            };

            var role2 = new IdentityRole()
            {
                Id = "2",
                Name = "Kibik",
                NormalizedName = "KIBIK",
                ConcurrencyStamp = "Top"
            };

            var identityRole1 = new IdentityUserRole<string>()
            {
                RoleId = "1",
                UserId = "1"
            };

            var identityRole2 = new IdentityUserRole<string>()
            {
                RoleId = "2",
                UserId = "2"
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                CreatedOn = DateTime.UtcNow,
                Email = "FirstUser@abv.bg",
                EmailConfirmed = true,
                FirstName = "En4o",
                LastName = "Ben4o",
                OfficeLocation = Offices.Bulgaria.ToString(),
                UserName = "En4hoto",
                Position = "Leader",
                Roles = new List<IdentityUserRole<string>>()
                {
                    identityRole1
                }
            };


            var user2 = new ApplicationUser()
            {
                Id = "2",
                CreatedOn = DateTime.UtcNow,
                Email = "SecondUser@abv.bg",
                EmailConfirmed = true,
                FirstName = "Bincho",
                LastName = "Binchev",
                OfficeLocation = Offices.Bulgaria.ToString(),
                UserName = "Binata",
                Position = "Beer Drinker",
                Roles = new List<IdentityUserRole<string>>()
                {
                    identityRole2
                }
            };

            await repo.AddAsync(user);
            await repo.AddAsync(user2);
            await repo.AddAsync(role1);
            await repo.AddAsync(role2);
            await repo.AddAsync(identityRole1);
            await repo.AddAsync(identityRole2);

            await repo.SaveChangesAsync();
        }
    }
}
