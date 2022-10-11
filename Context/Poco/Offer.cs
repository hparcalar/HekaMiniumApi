using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Offer{
        public int Id { get; set; }
        public string OfferCode { get; set; }
        public int OfferType { get; set; }
        public DateTime? OfferDate { get; set; }
        public string DocumentNo { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public int OfferStatus { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("Forex")]

        public int? ForexId { get; set; }

        public decimal? SubTotalPrice { get; set; }
        public decimal? TaxTotalPrice { get; set; }
        public decimal? DiscountTotalPrice { get; set; }
        public decimal? OverallTotalPrice { get; set; }

        [ForeignKey("CreatedUser")]
        public int? CreatedUserId { get; set; }

        public virtual SysUser CreatedUser { get; set; }
        public virtual Project Project { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Forex Forex { get; set; }
    }
}