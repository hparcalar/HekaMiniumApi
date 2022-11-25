using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOrder{
        public ItemOrder(){
            this.ItemOrderDetail = new HashSet<ItemOrderDetail>();
        }
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? DeadlineDate { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }

        [ForeignKey("ItemDemand")]
        public int? ItemDemandId { get; set; }
        public string DocumentNo { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public bool? IsWaybilled { get; set; }
        public int? ReceiptStatus { get; set; }

        [ForeignKey("SysUser")]
        public int? SysUserId { get; set; }

        public bool? IsContracted { get; set; }

        public virtual SysUser SysUser { get; set; }
        public virtual ItemDemand ItemDemand { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Plant Plant { get; set; }

        [InverseProperty("ItemOrder")]
        public virtual ICollection<ItemOrderDetail> ItemOrderDetail { get; set; }
    }
}