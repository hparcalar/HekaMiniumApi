namespace HekaMiniumApi.Models{
    public class ItemReceiptModel{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? FirmId { get; set; }
        public string DocumentNo { get; set; }
        public string Explanation { get; set; }
        public int? PlantId { get; set; }
        public int? InWarehouseId { get; set; }
        public int? OutWarehouseId { get; set; }
        public bool? IsInvoiced { get; set; }
        public int? ReceiptStatus { get; set; }
        public int? SysUserId { get; set; }

        #region VISUAL ELEMENTS
        public ItemReceiptDetailModel[] Details { get; set; }
        public string StatusText { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string ItemOrderNo { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        #endregion
    }
}