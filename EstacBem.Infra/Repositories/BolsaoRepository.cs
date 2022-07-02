using EstacBem.Domain.Entities;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.Infra.Repositories
{
    public class BolsaoRepository : RepositoryBase<Bolsao>, IBolsaoRepository
    {
        public BolsaoRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
