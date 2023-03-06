using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class InvoiceReceiptDetail{
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int? InvoiceId { get; set; }

        [ForeignKey("ItemReceiptDetail")]
        public int? ItemReceiptDetailId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual ItemReceiptDetail ItemReceiptDetail { get; set; }
    }
}