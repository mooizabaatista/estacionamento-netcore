using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstacBem.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(int id);
        public Task CreateAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteASync(TEntity entity);
    }
}
