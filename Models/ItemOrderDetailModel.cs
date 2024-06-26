namespace HekaMiniumApi.Models{
    public class ItemOrderDetailModel{
        public int Id { get; set; }
        public int? ItemOrderId { get; set; }
        public int? LineNumber { get; set; }
        public int? ItemId { get; set; }
        public int? BrandId { get; set; }
        public int? BrandModelId { get; set; }
        public int? UnitId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ForexId { get; set; }
        public int? ItemDemandDetailId { get; set; }
        public int? ProjectId { get; set; }
        public decimal? ForexRate { get; set; }
        public decimal? ForexUnitPrice { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? AlternatingQuantity { get; set; }
        public decimal? NetQuantity { get; set; }
        public decimal? GrossQuantity { get; set; }
        public decimal? UsedNetQuantity { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? ForexDiscountPrice { get; set; }
        public bool? TaxIncluded { get; set; }
        public int? TaxRate { get; set; }
        public decimal? TaxPrice { get; set; }
        public decimal? ForexTaxPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ForexSubTotal { get; set; }
        public decimal? OverallTotal { get; set; }
        public decimal? ForexOverallTotal { get; set; }
        public string Explanation { get; set; }
        public int? ReceiptStatus { get; set; }
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }
        public bool? IsContracted { get; set; }
        public string ItemExplanation { get; set; }
        public int? ItemOfferDetailId { get; set; }
        public string DenialExplanation { get; set; }

        #region VISUAL ELEMENTS
        public ItemDemandConsumeModel[] DemandConsumes { get; set; }
        public int? FirmId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string BrandModelCode { get; set; }
        public string BrandModelName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string ForexCode { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string ReceiptNo { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public int? ItemOfferId { get; set; }
        public string ItemOfferNo { get; set; }
        public string StatusText { get; set; }
        #endregion
    }
}