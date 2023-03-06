using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Invoice{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int? ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public string Explanation { get; set; }
        public bool? IsEInvoice { get; set; }
        public bool? EInvoiceStatus { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? OverallTotal { get; set; }
        public bool? IsTaxIncluded { get; set; }

        [ForeignKey("CurrentAccountReceipt")]
        public int? CurrentAccountReceiptId { get; set; }

        [ForeignKey("WorkingPeriod")]
        public int? WorkingPeriodId { get; set; }

        public virtual Firm Firm { get; set; }
        public virtual CurrentAccountReceipt CurrentAccountReceipt { get; set; }
        public virtual WorkingPeriod WorkingPeriod { get; set; }
    }
}