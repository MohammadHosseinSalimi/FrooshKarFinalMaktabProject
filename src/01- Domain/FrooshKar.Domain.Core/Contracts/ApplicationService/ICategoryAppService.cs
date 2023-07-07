using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface ICategoryAppService
	{

		Task Create(CategoryDtoModel entity, CancellationToken cancellationToken);
		Task<List<CategoryDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CategoryDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CategoryDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);





	}
}
