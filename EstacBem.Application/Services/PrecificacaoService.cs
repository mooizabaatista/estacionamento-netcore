using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;

namespace EstacBem.Application.Services
{
    public class PrecificacaoService : ServiceBase<Precificacao>, IPrecificacaoService
    {
        public PrecificacaoService(IPrecificacaoRepository repository) : base(repository)
        {

        }
    }
}
