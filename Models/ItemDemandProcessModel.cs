namespace HekaMiniumApi.Models{
    public class ItemDemandProcessModel{
        public int Id { get; set; }
        public int? ItemDemandDetailId { get; set; }
        public int? ProcessId { get; set; }

        public int? ProcessOrder { get; set; }
        public int? ProcessStatus { get; set; }
        public string Explanation { get; set; }
        public int? AssignedUserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        #region VISUAL ELEMENTS
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string StatusText { get; set; }
        public string ProjectName { get; set; }
        public string ItemDemandNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? ProcessType { get; set; }
        #endregion
    }
}