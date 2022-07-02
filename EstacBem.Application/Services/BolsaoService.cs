using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using System.Threading.Tasks;

namespace EstacBem.Application.Services
{
    public class BolsaoService : ServiceBase<Bolsao>, IBolsaoService
    {
        private readonly IBolsaoRepository _repository;

        public BolsaoService(IBolsaoRepository repository) : base(repository)
        {
            _repository = repository;
        }       
    }
}
