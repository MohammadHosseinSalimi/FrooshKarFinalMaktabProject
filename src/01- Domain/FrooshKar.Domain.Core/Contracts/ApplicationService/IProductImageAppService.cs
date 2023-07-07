﻿using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface IProductImageAppService
	{
		Task Create(ProductImageDtoModel entity, CancellationToken cancellationToken);
		Task<List<ProductImageDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<ProductImageDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(ProductImageDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
	}
}
