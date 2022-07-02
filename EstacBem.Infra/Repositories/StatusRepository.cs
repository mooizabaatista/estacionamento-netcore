using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;

namespace EstacBem.Infra.Repositories
{
    public class StatusRepository : RepositoryBase<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
