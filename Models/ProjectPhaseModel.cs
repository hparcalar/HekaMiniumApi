namespace HekaMiniumApi.Models{
    public class ProjectPhaseModel{
        public int Id { get; set; }
        public int? ProjectId { get; set; }

        public string PhaseTitle { get; set; }
        public string PhaseColor { get; set; }
        public string Explanation { get; set; }
        public int PhaseOrder { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PhaseStatus { get; set; }
    }
}