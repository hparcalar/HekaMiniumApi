namespace HekaMiniumApi.Models{
    public class ItemDemandConsumeModel{
        public int Id { get; set; }
        public int ItemDemandDetailId { get; set; }
        public int? ItemOrderDetailId { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? ConsumeDate { get; set; }

        #region VISUAL ELEMENTS
        public int? ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemExplanation { get; set; }
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }
        public decimal? DemandQuantity { get; set; }
        public string ProjectName { get; set; }
        public decimal? RemainingQuantity { get; set; }

        #endregion
    }
}