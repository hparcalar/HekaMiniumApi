using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOfferFirmOption{
        public int Id { get; set; }

        [ForeignKey("ItemOffer")]
        public int? ItemOfferId { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public int? FirmOrder { get; set; }

        public virtual ItemOffer ItemOffer { get; set; }
        public virtual Firm Firm { get; set; }
    }
}