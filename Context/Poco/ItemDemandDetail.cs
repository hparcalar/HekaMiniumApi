using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemDemandDetail{
        public ItemDemandDetail(){
            this.ItemReceiptDetails = new HashSet<ItemReceiptDetail>();
            this.ItemDemandDetailParts = new HashSet<ItemDemandDetailPart>();
            this.ItemDemandProcesses = new HashSet<ItemDemandProcess>();
        }
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
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }
        public bool? IsContracted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual ItemDemand ItemDemand { get; set; }
        public virtual Item Item { get; set; }
        public virtual UnitType UnitType { get; set; }

        public decimal? PartWidth { get; set; }
        public decimal? PartHeight { get; set; }
        public decimal? PartThickness { get; set; }

        [InverseProperty("ItemDemandDetail")]
        public virtual ICollection<ItemReceiptDetail> ItemReceiptDetails { get; set; }

        [InverseProperty("ItemDemandDetail")]
        public virtual ICollection<ItemDemandDetailPart> ItemDemandDetailParts { get; set; }

        [InverseProperty("ItemDemandDetail")]
        public virtual ICollection<ItemDemandProcess> ItemDemandProcesses { get; set; }

    }
}