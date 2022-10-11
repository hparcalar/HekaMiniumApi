namespace HekaMiniumApi.Models{
    public class ProjectTaskModel{
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ProjectPhaseId { get; set; }
        public int? AssigneeId { get; set; }
        public int? UserTeamId { get; set; }

        public string TaskName { get; set; }
        public string Explanation { get; set; }
        public int TaskStatus { get; set; }

        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? EndDate { get; set; }
    }
}