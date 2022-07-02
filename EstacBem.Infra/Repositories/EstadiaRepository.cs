using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.Infra.Repositories
{
    public class EstadiaRepository : RepositoryBase<Estadia>, IEstadiaRepository
    {
        public EstadiaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
