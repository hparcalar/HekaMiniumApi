using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemReceiptConsume{
        public int Id { get; set; }

        [ForeignKey("ConsumedReceiptDetail")]
        public int? ConsumedReceiptDetailId { get; set; }

        [ForeignKey("ConsumerReceiptDetail")]
        public int? ConsumerReceiptDetailId { get; set; }
        public DateTime? ConsumeDate { get; set; }
        public decimal? ConsumeNetQuantity { get; set; }

        public virtual ItemReceiptDetail ConsumedReceiptDetail { get; set; }
        public virtual ItemReceiptDetail ConsumerReceiptDetail { get; set; }
    }
}