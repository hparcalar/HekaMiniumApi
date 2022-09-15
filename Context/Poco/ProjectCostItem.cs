using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectCostItem{
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public int LineNumber { get; set; }
        public int CostType { get; set; }
        public int CostStatus { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }
        public string CostName { get; set; }
        public string Explanation { get; set; }

        public DateTime? CreatedDate { get; set; }

        [ForeignKey("CreatedUser")]
        public int? CreatedUserId { get; set; }

        public DateTime? RealizedDate { get; set; }

        public decimal? Quantity { get; set; }

        [ForeignKey("Forex")]
        public int? ForexId { get; set; }

        public string PartNo { get; set; }
        public string PartDimensions { get; set; }

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

        public virtual Project Project { get; set; }
        public virtual Item Item { get; set; }
        public virtual Forex Forex { get; set; }
        public virtual SysUser CreatedUser { get; set; }

    }
}