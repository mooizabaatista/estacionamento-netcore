using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;

namespace EstacBem.Application.Services
{
    public class VeiculoService : ServiceBase<Veiculo>, IVeiculoService
    {
        private readonly IVeiculoRepository _repository;

        public VeiculoService(IVeiculoRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public Veiculo GetVeiculoByPlaca(string placa)
        {
            var item = _repository.GetVeiculoByPlaca(placa);
            return item;
        }
    }
}
