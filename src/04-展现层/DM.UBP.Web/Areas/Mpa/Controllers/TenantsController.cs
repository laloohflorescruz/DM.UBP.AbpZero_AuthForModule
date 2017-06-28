using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Application.Service.SysManage.Common;
using DM.UBP.Application.Service.SysManage.Editions;
using DM.UBP.Application.Service.SysManage.MultiTenancy;
using DM.UBP.Domain.Service.SysManage.Authorization;
using DM.UBP.Domain.Service.SysManage.MultiTenancy;
using DM.UBP.Web.Areas.Mpa.Models.Tenants;
using DM.UBP.Web.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DM.UBP.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenants)]
    public class TenantsController : UbpControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly ICommonLookupAppService _commonLookupAppService;
        private readonly TenantManager _tenantManager;
        private readonly IEditionAppService _editionAppService;

        public TenantsController(
            ITenantAppService tenantAppService, 
            TenantManager tenantManager, 
            IEditionAppService editionAppService, 
            ICommonLookupAppService commonLookupAppService)
        {
            _tenantAppService = tenantAppService;
            _tenantManager = tenantManager;
            _editionAppService = editionAppService;
            _commonLookupAppService = commonLookupAppService;
        }

        public ActionResult Index()
        {
            ViewBag.FilterText = Request.QueryString["filterText"];
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var editionItems = await _editionAppService.GetEditionComboboxItems();
            var defaultEditionName = _commonLookupAppService.GetDefaultEditionName();
            var defaultEditionItem = editionItems.FirstOrDefault(e => e.DisplayText == defaultEditionName);
            if (defaultEditionItem != null)
            {
                defaultEditionItem.IsSelected = true;
            }

            var viewModel = new CreateTenantViewModel(editionItems);

            return PartialView("_CreateModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<PartialViewResult> EditModal(int id)
        {
            var tenantEditDto = await _tenantAppService.GetTenantForEdit(new EntityDto(id));
            var editionItems = await _editionAppService.GetEditionComboboxItems(tenantEditDto.EditionId);
            var viewModel = new EditTenantViewModel(tenantEditDto, editionItems);

            return PartialView("_EditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task<PartialViewResult> FeaturesModal(int id)
        {
            var tenant = await _tenantManager.GetByIdAsync(id);
            var output = await _tenantAppService.GetTenantFeaturesForEdit(new EntityDto(id));
            var viewModel = new TenantFeaturesEditViewModel(tenant, output);

            return PartialView("_FeaturesModal", viewModel);
        }
    }
}