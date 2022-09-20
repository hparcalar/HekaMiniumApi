namespace HekaMiniumApi.Models{
    public class ItemDemandDetailModel{
        public int Id { get; set; }
        public int? ItemDemandId { get; set; }

        public int? LineNumber { get; set; }
        public int? ItemId { get; set; }

        public string ItemExplanation { get; set; }
        public string Explanation { get; set; }
        public int? UnitId { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? NetQuantity { get; set; }

        public int? DemandStatus { get; set; }
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }
        public bool? IsContracted { get; set; }

        #region VISUAL ELEMENTS
        public int? ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public DateTime? DemandDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string ItemDemandNo { get; set; }
        public string StatusText { get; set; }
        public bool NewDetail { get; set; } = false;
        #endregion
    }
}