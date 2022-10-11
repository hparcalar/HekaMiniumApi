namespace HekaMiniumApi.Models{
    public class ProjectFieldServiceModel{
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string DocumentNo { get; set; }
        public int? ServiceUserId { get; set; }
        public int? ServiceStatus { get; set; }

        #region VISUAL ELEMENTS
        public ProjectFieldServiceDetailModel[] Details { get; set; }
        public ProjectFieldServiceAttachmentModel[] Attachments { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public string ServiceStatusText { get; set; }
        #endregion
    }
}