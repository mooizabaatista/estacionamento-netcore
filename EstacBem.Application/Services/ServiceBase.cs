using EstacBem.Application.Interfaces;
using EstacBem.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstacBem.Application.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        
        public async Task CreateAsync(TEntity entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteASync(entity);
        }      
    }
}
