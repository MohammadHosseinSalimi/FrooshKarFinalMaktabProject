using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface ICategoryRepository
	{
		Task Create(CategoryDtoModel entity, CancellationToken cancellationToken);
		Task<List<CategoryDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CategoryDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CategoryDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Save(CancellationToken cancellationToken);



	}
}
