namespace HekaMiniumApi.Models{
    public class StaffPermitModel{
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string StaffPermitExplanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PermitStatus { get; set; }
        public int? PermitType { get; set; }
        public string StatusText { get; set; }

        #region VISUAL ELEMENTS
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int ExcusePermitCount { get; set; }
        public int AnnualPermitCount { get; set; }
        public int ReportPermitCount { get; set; }
        public int AnnualPermitTotal { get; set; }
        public string PermitTypeText { get; set; }
        #endregion
    }
}