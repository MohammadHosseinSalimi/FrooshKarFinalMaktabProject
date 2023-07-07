using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrooshKar.Domain.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface IAppUserAppService
	{
		public Task<bool> IsExist(string emailAddress, CancellationToken cancellationToken);
		public Task<int> Create(AppUserDtoModel command, CancellationToken cancellationToken);
		public Task<List<AppUserDtoModel>> GetAll(CancellationToken cancellationToken);
		public Task<SignInResult> Login(AppUserDtoModel command, CancellationToken cancellationToken);
		public Task Logout();
		public Task<int> GetCurrentUserId();

		public Task UpdateUser(AppUserDtoModel entity, CancellationToken cancellationToken);
		Task<AppUserDtoModel> GetById(int id, CancellationToken cancellationToken);
		public Task<string> FindUserRoleByEmail(string email, CancellationToken cancellationToken);

		public Task DeleteUser(int userId, CancellationToken cancellationToken);


	}
}
