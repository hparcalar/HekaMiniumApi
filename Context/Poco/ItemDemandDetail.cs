using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemDemandDetail{
        public int Id { get; set; }

        [ForeignKey("ItemDemand")]
        public int? ItemDemandId { get; set; }

        public int? LineNumber { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        public string ItemExplanation { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("UnitType")]
        public int? UnitId { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? NetQuantity { get; set; }

        public int? DemandStatus { get; set; }

        public virtual ItemDemand ItemDemand { get; set; }
        public virtual Item Item { get; set; }
        public virtual UnitType UnitType { get; set; }

    }
}