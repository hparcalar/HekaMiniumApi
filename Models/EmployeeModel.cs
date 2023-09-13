namespace HekaMiniumApi.Models{
    public class EmployeeModel{
        public int Id { get; set; }
        public int? EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCardNo { get; set; }
        public decimal? EmployeeHourlyWage { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeAddress { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? DateOfStart { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public decimal? SeverancePay { get; set; }
        public string IsActive { get; set; }
    }
}