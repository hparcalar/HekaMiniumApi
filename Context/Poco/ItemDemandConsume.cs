using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemDemandConsume{
        public int Id { get; set; }

        [ForeignKey("ItemDemandDetail")]
        public int ItemDemandDetailId { get; set; }

        [ForeignKey("ItemOrderDetail")]
        public int? ItemOrderDetailId { get; set; }

        public decimal? Quantity { get; set; }

        public DateTime? ConsumeDate { get; set; }

        public virtual ItemDemandDetail ItemDemandDetail { get; set; }
        public virtual ItemOrderDetail ItemOrderDetail { get; set; }
    }
}