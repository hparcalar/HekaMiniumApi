namespace HekaMiniumApi.Models{
    public class SysRoleSectionModel{
        public int Id { get; set; }
        public int? SysRoleId { get; set; }
        public string SectionKey { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }
    }
}