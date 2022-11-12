using Microsoft.EntityFrameworkCore;
using POWER_System.Data;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Models.Enum;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;
using System.Security.Cryptography.X509Certificates;

namespace POWER_System.Services
{
    public class EnclosureService : IEnclosureService
    {
        private readonly IApplicationDbRepository repo;
        private readonly ApplicationDbContext context;

        public EnclosureService(IApplicationDbRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task AddProjectEnclosureAsync(EnclosureServiceModel model)
        {
            EnclosureStatus currentStatus;
            //Enum.TryParse(model.Status, out currentStatus);

            //var project = await repo.GetByIdAsync<Project>(model.ProjectId);

            var project = await repo.All<Project>()
                .Include(e => e.Enclosures)
                .Where(x => x.Id == model.ProjectId)
               .FirstOrDefaultAsync();

            var enclosure = new Enclosure()
            {
                Plant = model.Plant,
                Location = model.Location,
                Tag = model.Tag,
                Status = model.Status,
                Revision = model.Revision,
                Comment = model.Comment,
                ProjectId = model.Id,
            };

            project.Enclosures.Add(enclosure);
            await repo.AddAsync(enclosure);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnclosureServiceModel>> GetAllEnclosuresForProjectAsync(Guid id)
        {
            var enclosures = await repo.All<Enclosure>().ToListAsync();

            return enclosures.Where(x => x.ProjectId == id)
                .Select(p => new EnclosureServiceModel
                {
                    Plant = p.Plant,
                    Location = p.Location,
                    Tag = p.Tag,
                    Status = p.Status,
                    Revision = p.Revision,
                    Comment = p.Comment,
                    ProjectId = p.Id,
                }).ToList();
        }
    }
}
