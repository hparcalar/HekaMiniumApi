namespace HekaMiniumApi.Models{
    public class EmployeeCheckInModel{
        public int Id { get; set; }
        public DateTime? ProcessDate { get; set; }
        public int? ProcessType { get; set; }
        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }
    }
}