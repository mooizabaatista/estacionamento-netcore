using EstacBem.Domain.Entities;

namespace EstacBem.Application.Interfaces
{
    public interface IVeiculoService : IServiceBase<Veiculo>
    {
        public Veiculo GetVeiculoByPlaca(string placa);
    }
}
