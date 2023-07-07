using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface IMedalAppService
	{
		Task Create(MedalDtoModel entity, CancellationToken cancellationToken);
		Task<List<MedalDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<MedalDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(MedalDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
	}
}
