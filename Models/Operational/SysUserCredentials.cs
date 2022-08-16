using HekaMiniumApi.Models;

namespace HekaMiniumApi.Models.Operational{
    public class SysUserCredentials{
        public int RoleAuthType { get; set; }
        public string RoleName { get; set; }
        public SysRoleSectionModel[] Sections { get; set; }
    }
}