namespace DM.UBP.Application.Dto.SysManage.Configuration.Tenants
{
    public class LdapSettingsEditDto
    {
        public bool IsModuleEnabled { get; set; }

        public bool IsEnabled { get; set; }
        
        public string Domain { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}