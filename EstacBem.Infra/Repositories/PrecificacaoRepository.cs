using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;

namespace EstacBem.Infra.Repositories
{
    public class PrecificacaoRepository : RepositoryBase<Precificacao>, IPrecificacaoRepository
    {
        public PrecificacaoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
