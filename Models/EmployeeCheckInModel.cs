namespace HekaMiniumApi.Models{
    public class EmployeeCheckInModel{
        public int Id { get; set; }
        public DateTime? ProcessDate { get; set; }
        public int? ProcessType { get; set; }
        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        #region  VISUAL ELEMENTS
        public DateTime? ExitDate { get; set; }
        public int AutoCheckOut { get; set; }
        public double? TotalHour { get; set; }
        #endregion
    }
}