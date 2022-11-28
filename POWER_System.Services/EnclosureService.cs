using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using POWER_System.Data;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Services
{
    public class EnclosureService : IEnclosureService
    {
        private readonly IApplicationDbRepository repo;

        private readonly IPartService partService;

        public EnclosureService(IApplicationDbRepository _repo,
            IPartService _partService
            )
        {
            repo = _repo;
            partService = _partService;
        }

        public async Task AddProjectEnclosureAsync(EnclosureServiceModel model)
        {
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

        public async Task<IEnumerable<EnclosureServiceModel>> GetAllEnclosuresForProjectAsync(Guid projectId)
        {
            return await repo.All<Enclosure>()
                .Where(x => x.ProjectId == projectId)
                .Select(p => new EnclosureServiceModel
                {
                    Id = p.Id,
                    Plant = p.Plant,
                    Location = p.Location,
                    Tag = p.Tag,
                    Status = p.Status,
                    Revision = p.Revision,
                    Comment = p.Comment,
                    ProjectId = p.Id
                }).ToListAsync();
        }


        public async Task<EnclosureServiceModel> EnclosureDetails(Guid enclosureId)
        {
            var enclosure = await repo.All<Enclosure>()
                .FirstOrDefaultAsync(e => e.Id == enclosureId);


            var parts = await partService.GetAllPartsForEnclosuresAsync(enclosureId);

            var specificEnclosure = new EnclosureServiceModel()
            {
                Id = enclosure.Id,
                Plant = enclosure.Plant,
                Location = enclosure.Location,
                Tag = enclosure.Tag,
                Status = enclosure.Status,
                Revision = enclosure.Revision,
                Comment = enclosure.Comment,
                ProjectId = enclosure.Id,
                Parts = parts
            };

            return specificEnclosure;
        }

        public async Task<EnclosureServiceModel> AddPartsToEnclosure(Guid enclosureId, IFormFile file)
        {
            var enclosure = await repo.All<Enclosure>()
                .Include(e => e.Parts)
                .FirstOrDefaultAsync(e => e.Id == enclosureId);

            var parts = await partService.AddPartsFromFile(file, enclosureId);

            var specificEnclosure = new EnclosureServiceModel()
            {
                Id = enclosureId,
                Tag = enclosure.Tag, 
                Status = enclosure.Status,
                Revision = enclosure.Revision,
                Comment = enclosure.Comment,
                Location = enclosure.Location,
                Plant = enclosure.Plant,
                ProjectId = enclosure.ProjectId,
                Parts = parts
            };

            return specificEnclosure;
        }




    }
}
