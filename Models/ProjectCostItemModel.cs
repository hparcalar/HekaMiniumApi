namespace HekaMiniumApi.Models{
    public class ProjectCostItemModel{
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int LineNumber { get; set; }
        public int CostType { get; set; }
        public int CostStatus { get; set; }
        public int? ItemId { get; set; }
        public string CostName { get; set; }
        public string Explanation { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserId { get; set; }

        public DateTime? RealizedDate { get; set; }

        public decimal? Quantity { get; set; }
        public int? ForexId { get; set; }

        #region PRICES
        public decimal? EstimatedUnitPrice { get; set; }
        public decimal? EstimatedForexUnitPrice { get; set; }
        public decimal? EstimatedForexRate { get; set; }
        public decimal? EstimatedSubTotal { get; set; }
        public decimal? EstimatedForexSubTotal { get; set; }
        public decimal? EstimatedForexTaxTotal { get; set; }
        public decimal? EstimatedTaxTotal { get; set; }
        public decimal? EstimatedForexOverallTotal { get; set; }
        public decimal? EstimatedOverallTotal { get; set; }
        public int? TaxRate { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ForexUnitPrice { get; set; }
        public decimal? ForexRate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ForexSubTotal { get; set; }
        public decimal? ForexTaxTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? ForexOverallTotal { get; set; }
        public decimal? OverallTotal { get; set; }
        #endregion

        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CostTypeText { get; set; } // 0:item, 1:workmanship
        public string CostStatusText { get; set; } // 0:waiting, 1:realized
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ForexCode { get; set; }
        public string ForexName { get; set; }
        #endregion
    }
}