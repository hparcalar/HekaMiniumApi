using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemReceipt{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public string DocumentNo { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("InWarehouse")]
        public int? InWarehouseId { get; set; }

        [ForeignKey("OutWarehouse")]
        public int? OutWarehouseId { get; set; }
        public bool? IsInvoiced { get; set; }
        public int? ReceiptStatus { get; set; }

        [ForeignKey("SysUser")]
        public int? SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Warehouse InWarehouse { get; set; }
        public virtual Warehouse OutWarehouse { get; set; }
    }
}