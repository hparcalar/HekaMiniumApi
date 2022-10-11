namespace HekaMiniumApi.Models{
    public class ItemOfferDetailModel{
        public int Id { get; set; }
        public int? ItemOfferId { get; set; }

        public int? LineNumber { get; set; }
        public int? ItemId { get; set; }
        public int? UnitId { get; set; }
        public int? AcceptedFirmId { get; set; }

        public int? ForexId { get; set; }

        public decimal? ForexRate { get; set; }

        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? SubForexTotal { get; set; }
        public bool? TaxIncluded { get; set; }
        public int? TaxRate { get; set; }
        public decimal? OverallTotal { get; set; }
        public decimal? OverallForexTotal { get; set; }

        public decimal? Quantity { get; set; }
        public int? OfferStatus { get; set; }

        public decimal? NetQuantity { get; set; }
        public string ItemExplanation { get; set; }
        public string Explanation { get; set; }
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string StatusText { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string ForexCode { get; set; }
        public string ForexName { get; set; }
        public ItemOfferDetailDemandModel[] Demands { get; set; }
        public ItemOfferFirmPriceModel[] FirmPrices { get; set; }
        public ItemOrderDetailModel OrderDetail { get; set; }
        #endregion
    }
}