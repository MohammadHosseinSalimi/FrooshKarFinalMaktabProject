﻿using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface IFactorRepository
	{
		Task Create(FactorDtoModel entity, CancellationToken cancellationToken);
		Task<List<FactorDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<FactorDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(FactorDtoModel entity, CancellationToken cancellationToken);
		public Task<List<FactorDtoModel>> GetAllWithVendor(CancellationToken cancellationToken);

		Task Delete(int id, CancellationToken cancellationToken);
		Task Save(CancellationToken cancellationToken);
	}
}
