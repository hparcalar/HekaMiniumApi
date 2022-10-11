namespace HekaMiniumApi.Models{
    public class ProjectFieldServiceDetailModel{
        public int Id { get; set; }
        public int? ProjectFieldServiceId { get; set; }

        public int? LineNumber { get; set; }
        public string WorkExplanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? ServiceStatus { get; set; }

        #region VISUAL ELEMENTS
        public ProjectFieldServiceAttachmentModel[] Attachments { get; set; }
        public string ServiceStatusText { get; set; }
        #endregion
    }
}