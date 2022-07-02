using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using System.Threading.Tasks;

namespace EstacBem.Application.Services
{
    public class EstadiaService : ServiceBase<Estadia>, IEstadiaService
    {
        public EstadiaService(IEstadiaRepository repository) : base(repository)
        {
        }
    }
}
