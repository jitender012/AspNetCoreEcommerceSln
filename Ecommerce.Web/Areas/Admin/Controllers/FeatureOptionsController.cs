using AutoMapper;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    public class FeatureOptionsController : Controller
    {
        private readonly IFeatureOptionService _featureOptionService;
        private readonly IMapper _mapper;
        public FeatureOptionsController(IFeatureOptionService featureOptionService, IMapper mapper)
        {
            _mapper = mapper;
            _featureOptionService = featureOptionService;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var featureOptions = await _featureOptionService.GetAllAsync();
                var featureOptionsVm = _mapper.Map<FeatureOptionViewModel>(featureOptions);
                return View(featureOptionsVm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<ActionResult> Create()
        //{

        //}
    }
}
