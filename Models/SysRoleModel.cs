namespace HekaMiniumApi.Models{
    public class SysRoleModel{
        public int Id { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public int RoleAuthType { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoot { get; set; }
        public int? PlantId { get; set; }

        #region VISUAL ELEMENTS
        public SysRoleSectionModel[] Sections { get; set; }
        #endregion
    }
}