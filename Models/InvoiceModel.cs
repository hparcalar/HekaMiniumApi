namespace HekaMiniumApi.Models{
    public class InvoiceModel{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int? ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? FirmId { get; set; }
        public string Explanation { get; set; }
        public bool? IsEInvoice { get; set; }
        public bool? EInvoiceStatus { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? OverallTotal { get; set; }
        public bool? IsTaxIncluded { get; set; }
        public int? CurrentAccountReceiptId { get; set; }
        public int? WorkingPeriodId { get; set; }

        #region VISUAL ELEMENTS
        public InvoiceReceiptDetailModel[] Details { get; set; }
        public InvoiceTypeModel[] InvoiceTypeList { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string InvoiceTypeText { get; set; }
        #endregion
    }
}