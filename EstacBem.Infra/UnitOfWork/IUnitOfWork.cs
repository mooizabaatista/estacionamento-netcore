using System.Threading.Tasks;

namespace EstacBem.Infra.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task Commit();
        public Task RollBack();
    }
}
