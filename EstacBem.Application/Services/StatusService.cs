using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;

namespace EstacBem.Application.Services
{
    public class StatusService : ServiceBase<Status>, IStatusService
    {
        public StatusService(IStatusRepository repository) : base(repository)
        {

        }
    }
}
