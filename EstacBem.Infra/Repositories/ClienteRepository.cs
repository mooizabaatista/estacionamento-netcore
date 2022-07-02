using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;
using System.Threading.Tasks;

namespace EstacBem.Infra.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
