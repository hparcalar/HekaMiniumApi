using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOfferDetailDemand{
        public int Id { get; set; }

        [ForeignKey("ItemOfferDetail")]
        public int? ItemOfferDetailId { get; set; }

        [ForeignKey("ItemDemandDetail")]
        public int? ItemDemandDetailId { get; set; }

        public decimal? Quantity { get; set; }
        public int? DemandOrder { get; set; }

        public virtual ItemOfferDetail ItemOfferDetail { get; set; }
        public virtual ItemDemandDetail ItemDemandDetail { get; set; }
    }
}