namespace HekaMiniumApi.Models{
    public class InvoiceReceiptDetailModel{
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? ItemReceiptDetailId { get; set; }


        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string BrandModelCode { get; set; }
        public string BrandModelName { get; set; }
        public string UnitCode { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string UnitName { get; set; }
        public string ForexCode { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ReceiptNo { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ForexId { get; set; }
        public decimal? ForexRate { get; set; }
        public decimal? ForexUnitPrice { get; set; }
        public decimal? Quantity { get; set; }
        public bool? TaxIncluded { get; set; }
        public int? TaxRate { get; set; }
        public decimal? TaxPrice { get; set; }
        public decimal? ForexTaxPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ForexSubTotal { get; set; }
        public decimal? OverallTotal { get; set; }
        public decimal? ForexOverallTotal { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public ItemOrderConsumeModel[] OrderConsumes { get; set; }
        #endregion
    }
}