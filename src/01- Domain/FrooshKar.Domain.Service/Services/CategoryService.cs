using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task Create(CategoryDtoModel entity, CancellationToken cancellationToken)
        {
            await _categoryRepository.Create(entity, cancellationToken);
        }

        public async Task<List<CategoryDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll(cancellationToken);
        }

        public async Task<CategoryDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetById(id, cancellationToken);
        }

        public async Task Update(CategoryDtoModel entity, CancellationToken cancellationToken)
        {
            await _categoryRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryRepository.Delete(id, cancellationToken);
        }




    }
}
