using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOrderConsume{
        public int Id { get; set; }

        [ForeignKey("ItemOrderDetail")]
        public int? ItemOrderDetailId { get; set; }

        [ForeignKey("ContributerReceiptDetail")]
        public int? ContributerItemReceiptDetailId { get; set; } // any existing item receipts to complete this order is contributer

        [ForeignKey("ConsumerReceiptDetail")]
        public int? ConsumerItemReceiptDetailId { get; set; } // any created item receipts to complete this order is consumer
        public decimal? ContributeNetQuantity { get; set; }
        public decimal? ConsumeNetQuantity { get; set; }
        public DateTime? ContributeDate { get; set; }
        public DateTime? ConsumeDate { get; set; }

        public virtual ItemOrderDetail ItemOrderDetail { get; set; }
        public virtual ItemReceiptDetail ContributerReceiptDetail { get; set; }
        public virtual ItemReceiptDetail ConsumerReceiptDetail { get; set; }
    }
}