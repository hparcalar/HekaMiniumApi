using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemReceiptDetail{
        public int Id { get; set; }

        [ForeignKey("ItemReceipt")]
        public int? ItemReceiptId { get; set; }
        public int? LineNumber { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        [ForeignKey("Brand")]
        public int? BrandId { get; set; }

        [ForeignKey("BrandModel")]
        public int? BrandModelId { get; set; }

        [ForeignKey("UnitType")]
        public int? UnitId { get; set; }
        public decimal? UnitPrice { get; set; }

        [ForeignKey("Forex")]
        public int? ForexId { get; set; }

        [ForeignKey("Project")]
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
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }

        public virtual ItemReceipt ItemReceipt { get; set; }
        public virtual Item Item { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual BrandModel BrandModel { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual Forex Forex { get; set; }
        public virtual Project Project { get; set; }
    }
}