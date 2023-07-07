using AutoMapper;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.EndPoints.MVC.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Components
{
    public class FixedPriceProductViewComponent:ViewComponent
    {
        private readonly IFixedPriceProductAppService _fixedPriceProductAppService;
        private readonly IMapper _mapper;

        public FixedPriceProductViewComponent(IFixedPriceProductAppService fixedPriceProductAppService, IMapper mapper)
        {
	        _fixedPriceProductAppService = fixedPriceProductAppService;
	        _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var getAllFixedPriceProducts = await _fixedPriceProductAppService.GetAll(cancellationToken);
            var notDeletedFixedPriceProducts = getAllFixedPriceProducts.Where(x => x.IsDeleted == false).ToList();
            return View(notDeletedFixedPriceProducts);
        }



	}
}
