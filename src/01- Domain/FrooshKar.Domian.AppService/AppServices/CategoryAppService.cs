using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task Create(CategoryDtoModel entity, CancellationToken cancellationToken)
        {
            await _categoryService.Create(entity, cancellationToken);
        }

        public async Task<List<CategoryDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryService.GetAll(cancellationToken);
        }

        public async Task<CategoryDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _categoryService.GetById(id, cancellationToken);
        }

        public async Task Update(CategoryDtoModel entity, CancellationToken cancellationToken)
        {
            await _categoryService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.Delete(id, cancellationToken);
        }




    }
}
