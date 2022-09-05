namespace HekaMiniumApi.Models{
    public class ItemOrderModel{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int? FirmId { get; set; }
        public int? ItemDemandId { get; set; }
        public string DocumentNo { get; set; }
        public string Explanation { get; set; }
        public int? PlantId { get; set; }
        public bool? IsWaybilled { get; set; }
        public int? ReceiptStatus { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string ItemDemandNo { get; set; }
        public string StatusText { get; set; }

        public ItemOrderDetailModel[] Details { get; set; }
        #endregion
    }
}