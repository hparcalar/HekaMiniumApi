namespace HekaMiniumApi.Models{
    public class ItemDemandModel{
        public int Id { get; set; }

        public string ReceiptNo { get; set; }

        public DateTime? ReceiptDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Explanation { get; set; }
        public int? PlantId { get; set; }
        public int? ProjectId { get; set; }

        public bool? IsOrdered { get; set; }
        public int? DemandStatus { get; set; }
        public int? SysUserId { get; set; }
        public bool? IsContracted { get; set; }

        #region VISUAL ELEMENTS
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string StatusText { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public ItemDemandDetailModel[] Details { get; set; }
        #endregion
    }
}