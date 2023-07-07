using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FrooshKar.Domain.AppService.AppServices
{
	public class AppUserAppService : IAppUserAppService
	{
		private readonly IAppUserService _appUserService;

		public AppUserAppService(IAppUserService appUserService)
		{
			_appUserService = appUserService;
		}

		public async Task<bool> IsExist(string emailAddress,CancellationToken cancellationToken)
		{
			return await _appUserService.IsExist(emailAddress,cancellationToken);
		}

		public async Task<int> Create(AppUserDtoModel command, CancellationToken cancellationToken)
		{
			return await _appUserService.Create(command, cancellationToken);
		}

		public async Task<List<AppUserDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			return await _appUserService.GetAll(cancellationToken);
		}

		public async Task<SignInResult> Login(AppUserDtoModel command, CancellationToken cancellationToken)
		{
			return await _appUserService.Login(command, cancellationToken);
		}

		public async Task Logout()
		{
			await _appUserService.Logout();
		}

		public async Task<int> GetCurrentUserId()
		{
			return await _appUserService.GetCurrentUserId();
		}

		public async Task UpdateUser(AppUserDtoModel entity, CancellationToken cancellationToken)
		{
			await _appUserService.UpdateUser(entity, cancellationToken);
		}

		public async Task<AppUserDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			return await _appUserService.GetById(id, cancellationToken);
		}

		public async Task<string> FindUserRoleByEmail(string email, CancellationToken cancellationToken)
		{
			if (await _appUserService.IsExist(email, cancellationToken))
			{
				return await _appUserService.FindUserRoleByEmail(email, cancellationToken);
			}

			return string.Empty;
		}

		public async Task DeleteUser(int userId, CancellationToken cancellationToken)
		{
			await _appUserService.DeleteUser(userId, cancellationToken);
		}

		



	}
}
