namespace HekaMiniumApi.Models{
    public class ItemOfferDetailDemandModel{
        public int Id { get; set; }
        public int? ItemOfferDetailId { get; set; }
        public int? ItemDemandDetailId { get; set; }

        public decimal? Quantity { get; set; }
        public int? DemandOrder { get; set; }

        #region VISUAL ELEMENTS
        public string ItemName { get; set; }
        public string ItemExplanation { get; set; }
        public string ProjectName { get; set; }
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }
        public decimal? DemandQuantity { get; set; }
        #endregion
    }
}