using POWER_System.Data.Common;

namespace POWER_System.Data.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(ApplicationDbContext context)
        {
            Context = context;
        }
    }
}
