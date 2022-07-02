using EstacBem.Infra.Context;
using System.Threading.Tasks;

namespace EstacBem.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context = null)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public Task RollBack()
        {
            return Task.CompletedTask;
        }
    }
}
