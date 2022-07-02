using EstacBem.Domain.Entities;

namespace EstacBem.Domain.Interfaces
{
    public interface IVeiculoRepository : IRepositoryBase<Veiculo>
    {
        public Veiculo GetVeiculoByPlaca(string placa);
    }
}
