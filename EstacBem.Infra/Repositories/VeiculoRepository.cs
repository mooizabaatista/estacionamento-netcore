using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;
using System.Linq;

namespace EstacBem.Infra.Repositories
{
    public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository
    {
        private readonly ApplicationDbContext _context;


        public VeiculoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Veiculo GetVeiculoByPlaca(string placa)
        {
            var item = _context.Set<Veiculo>().Where(x => x.Placa == placa).FirstOrDefault();
            return item;
        }
    }
}
