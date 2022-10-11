namespace HekaMiniumApi.Models{
    public class ItemOfferModel{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string Explanation { get; set; }
        public int? OfferStatus { get; set; }
        public int? OfferType { get; set; }
        public int? FirmId { get; set; }
        public int? PlantId { get; set; }
        public int? SysUserId { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string StatusText { get; set; }
        public ItemOfferDetailModel[] Details { get; set; }
        public ItemOfferFirmOptionModel[] FirmOptions { get; set; }
        public ItemOfferFirmPriceModel[] FirmPrices { get; set; }
        #endregion
    }
}