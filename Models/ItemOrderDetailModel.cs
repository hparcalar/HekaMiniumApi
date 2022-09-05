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

        #region VISUAL ELEMENTS
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
        public string StatusText { get; set; }
        #endregion
    }
}