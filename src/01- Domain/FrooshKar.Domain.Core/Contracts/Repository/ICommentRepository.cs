using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface ICommentRepository
	{
		Task Create(CommentDtoModel entity, CancellationToken cancellationToken);
		Task<List<CommentDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CommentDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CommentDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Save(CancellationToken cancellationToken);
		public Task<List<CommentDtoModel>> GetAllCommentsWithVendors(CancellationToken cancellationToken);

		Task ValidationByAdmin(CommentDtoModel entity, CancellationToken cancellationToken);

	}
}
