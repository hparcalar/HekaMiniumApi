using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOfferDetail{
        public int Id { get; set; }

        [ForeignKey("ItemOffer")]
        public int? ItemOfferId { get; set; }

        public int? LineNumber { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        [ForeignKey("UnitType")]
        public int? UnitId { get; set; }

        [ForeignKey("AcceptedFirm")]
        public int? AcceptedFirmId { get; set; }

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

        public decimal? Quantity { get; set; }
        public int? OfferStatus { get; set; }

        public decimal? NetQuantity { get; set; }
        public string ItemExplanation { get; set; }
        public string Explanation { get; set; }
        public string PartNo { get; set; }
        public string PartDimensions { get; set; }

        public virtual ItemOffer ItemOffer { get; set; }
        public virtual Item Item { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual Firm AcceptedFirm { get; set; }
        public virtual Forex Forex { get; set; }
    }
}