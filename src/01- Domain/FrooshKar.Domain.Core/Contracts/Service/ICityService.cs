using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Service
{
	public interface ICityService
	{

		Task Create(CityDtoModel entity, CancellationToken cancellationToken);
		Task<List<CityDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CityDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CityDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);



	}
}
