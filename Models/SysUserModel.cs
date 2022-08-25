namespace HekaMiniumApi.Models{
    public class SysUserModel{
        public int Id { get; set; }
        public string UserCode { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public string Password { get; set; }
        public string DefaultLanguage { get; set; }
        public bool IsActive { get; set; }
        public int? PlantId { get; set; }
        public int? SysRoleId { get; set; }
    }
}