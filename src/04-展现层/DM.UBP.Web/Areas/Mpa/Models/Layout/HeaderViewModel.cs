using Abp.Localization;
using DM.UBP.Application.Dto.SysManage.Sessions;
using System.Collections.Generic;

namespace DM.UBP.Web.Areas.Mpa.Models.Layout
{
    public class HeaderViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public LanguageInfo CurrentLanguage { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public bool IsImpersonatedLogin { get; set; }

        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? ".\\" + userName
                : LoginInformations.Tenant.TenancyName + "\\" + userName;
        }

        public string GetLogoUrl(string appPath)
        {
            if (!IsMultiTenancyEnabled || LoginInformations.Tenant?.LogoId == null)
            {
                return appPath + "Common/Images/app-logo-on-light.png";
            }

            return appPath + "TenantCustomization/GetLogo?id=" + LoginInformations.Tenant.LogoId;
        }
    }
}