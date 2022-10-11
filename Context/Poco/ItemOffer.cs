using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOffer{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string Explanation { get; set; }
        public int? OfferStatus { get; set; }
        public int? OfferType { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("SysUser")]
        public int? SysUserId { get; set; }

        public virtual Firm Firm { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}