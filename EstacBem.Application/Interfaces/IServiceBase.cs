using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstacBem.Application.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(int id);
        public Task CreateAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);

    }
}
