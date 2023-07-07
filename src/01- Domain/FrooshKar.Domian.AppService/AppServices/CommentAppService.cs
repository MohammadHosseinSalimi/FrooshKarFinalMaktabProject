using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;

        public CommentAppService(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task Create(CommentDtoModel entity, CancellationToken cancellationToken)
        {
            await _commentService.Create(entity, cancellationToken);
        }

        public async Task<List<CommentDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _commentService.GetAll(cancellationToken);
        }

        public async Task<CommentDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _commentService.GetById(id, cancellationToken);
        }

        public async Task Update(CommentDtoModel entity, CancellationToken cancellationToken)
        {
            await _commentService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _commentService.Delete(id, cancellationToken);
        }

        public async Task ValidationByAdmin(CommentDtoModel entity, CancellationToken cancellationToken)
        {
            await _commentService.ValidationByAdmin(entity, cancellationToken);
        }

        public async Task<List<CommentDtoModel>> GetAllCommentsWithVendors(CancellationToken cancellationToken)
        {
	        return await _commentService.GetAllCommentsWithVendors(cancellationToken);
        }

	}
}
