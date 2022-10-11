using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOfferFirmPrice{
        public int Id { get; set; }

        [ForeignKey("ItemOfferDetail")]
        public int? ItemOfferDetailId { get; set; }

        [ForeignKey("ItemOffer")]
        public int? ItemOfferId { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }

        [ForeignKey("Forex")]
        public int? ForexId { get; set; }

        public decimal? ForexRate { get; set; }

        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? SubForexTotal { get; set; }
        public bool? TaxIncluded { get; set; }
        public int? TaxRate { get; set; }
        public decimal? OverallTotal { get; set; }
        public decimal? OverallForexTotal { get; set; }

        public virtual ItemOffer ItemOffer { get; set; }
        public virtual ItemOfferDetail ItemOfferDetail { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Forex Forex { get; set; }

    }
}