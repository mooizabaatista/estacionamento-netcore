using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;

namespace EstacBem.Application.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        public ClienteService(IClienteRepository repository) : base(repository)
        {
        }
    }
}
