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

            if (project.Enclosures.Any(n => n.Tag == model.Tag))
            {
                throw new ArgumentException("Enclosure with that Tag already exists.");
            }

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
                    ProjectId = p.ProjectId
                }).ToListAsync();
        }

        public async Task<EnclosureServiceModel> EnclosureSummarizedDetails(Guid enclosureId)
        {
            var enclosure = await repo.All<Enclosure>()
                .Include(p => p.Parts)
                .FirstOrDefaultAsync(e => e.Id == enclosureId);

            var parts = await partService.GetSummarizedPartsForEnclosuresAsync(enclosureId);

            var specificEnclosure = new EnclosureServiceModel()
            {
                Id = enclosure.Id,
                Plant = enclosure.Plant,
                Location = enclosure.Location,
                Tag = enclosure.Tag,
                Status = enclosure.Status,
                Revision = enclosure.Revision,
                Comment = enclosure.Comment,
                ProjectId = enclosure.ProjectId,
                Parts = parts,
            };

            return specificEnclosure;
        }

        public async Task<EnclosureServiceModel> EnclosureFullDetails(Guid enclosureId)
        {
            var enclosure = await repo.All<Enclosure>()
                .Include(p => p.Parts)
                .FirstOrDefaultAsync(e => e.Id == enclosureId);

            var parts = await partService.GetDetailedPartsForEnclosuresAsync(enclosureId);

            var specificEnclosure = new EnclosureServiceModel()
            {
                Id = enclosure.Id,
                Plant = enclosure.Plant,
                Location = enclosure.Location,
                Tag = enclosure.Tag,
                Status = enclosure.Status,
                Revision = enclosure.Revision,
                Comment = enclosure.Comment,
                ProjectId = enclosure.ProjectId,
                Parts = parts,
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
            
            await repo.SaveChangesAsync();

            return specificEnclosure;
        }

        public async Task <List<EnclosurePart>> ManagePartsForOrder(Guid enclosureId)
        {
            //var enclosure = await repo.All<Enclosure>()
            //    .Include(p => p.Parts)
            //    .FirstOrDefaultAsync(e => e.Id == enclosureId);

            ////var parts = 

            throw new NotImplementedException();
        }
    }
}
