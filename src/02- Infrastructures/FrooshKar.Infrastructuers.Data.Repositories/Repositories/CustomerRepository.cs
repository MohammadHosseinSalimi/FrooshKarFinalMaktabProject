using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class CustomerRepository: ICustomerRepository
	{

		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

		public CustomerRepository(FrooshKarDbContext dbContext, IMapper mapper, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
		{
			_dbContext = dbContext;
			_mapper = mapper;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

		public async Task CreateByAppUser(CustomerDtoModel entity,int appUserId, CancellationToken cancellationToken)
		{
			entity.AppUserId=appUserId;
			await _dbContext.Customers.AddAsync(_mapper.Map<Customer>(entity),cancellationToken);
			await Save(cancellationToken);

		}


		//public async Task<CustomerDtoModel> GetCustomerByUserId(int userId, CancellationToken cancellationToken)
		//{
		//	var record = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.AppUserId == userId, cancellationToken);
		//	return _mapper.Map<CustomerDtoModel>(record);
		//}

		public async Task<List<CustomerDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Customers.AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<CustomerDtoModel>>(record);


		}

		public async Task<CustomerDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Customers
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<CustomerDtoModel>(record);
		}

		public async Task Update(CustomerDtoModel entity, CancellationToken cancellationToken)
		{
			//todo: why tracking doesn't work (this problem has been solved but I must copy this code to vendor dto model)
			var record = await _dbContext.Customers.AsNoTracking().Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			var customer = _mapper.Map(entity, record);
			_dbContext.Update(customer);
			await Save(cancellationToken);

			//var record = await _dbContext.Customers.Include(x=>x.AppUser).Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			//var customer = _mapper.Map(entity, record);
			//record.AppUserId=entity.AppUserId;
			//await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}
        public async Task<int> FindCurrentCustomerId(CancellationToken cancellationToken)
		{

            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            var record = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.AppUserId == currentUser.Id, cancellationToken);
            return record.Id;
        }


        public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}


	}
}
