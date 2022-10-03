namespace HekaMiniumApi.Models{
    public class StaffPermitModel{
        public int Id { get; set; }
        public string StaffId { get; set; }
        public string StaffPermitExplanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PermitStatus { get; set; }
    }
}