namespace HekaMiniumApi.Models{
    public class ItemOfferFirmOptionModel{
        public int Id { get; set; }
        public int? ItemOfferId { get; set; }
        public int? FirmId { get; set; }
        public int? FirmOrder { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        #endregion
    }
}