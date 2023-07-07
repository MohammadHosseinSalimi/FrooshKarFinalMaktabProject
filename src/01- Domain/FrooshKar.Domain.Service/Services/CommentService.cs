using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task Create(CommentDtoModel entity, CancellationToken cancellationToken)
        {
            await _commentRepository.Create(entity, cancellationToken);
        }

        public async Task<List<CommentDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _commentRepository.GetAll(cancellationToken);
        }

        public async Task<CommentDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetById(id, cancellationToken);
        }

        public async Task Update(CommentDtoModel entity, CancellationToken cancellationToken)
        {
            await _commentRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _commentRepository.Delete(id, cancellationToken);
        }

        public async Task ValidationByAdmin(CommentDtoModel entity, CancellationToken cancellationToken)
        {
	        await _commentRepository.ValidationByAdmin(entity, cancellationToken);
        }

        public async Task<List<CommentDtoModel>> GetAllCommentsWithVendors(CancellationToken cancellationToken)
        {
	        return await _commentRepository.GetAllCommentsWithVendors(cancellationToken);
        }


	}
}
