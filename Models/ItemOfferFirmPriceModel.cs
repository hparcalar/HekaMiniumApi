namespace HekaMiniumApi.Models{
    public class ItemOfferFirmPriceModel{
        public int Id { get; set; }

        public int? ItemOfferDetailId { get; set; }

        public int? ItemOfferId { get; set; }

        public int? FirmId { get; set; }

        public int? ForexId { get; set; }

        public decimal? ForexRate { get; set; }

        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? SubForexTotal { get; set; }
        public bool? TaxIncluded { get; set; }
        public int? TaxRate { get; set; }
        public decimal? OverallTotal { get; set; }
        public decimal? OverallForexTotal { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string ForexCode { get; set; }
        public string ForexName { get; set; }
        #endregion
    }
}