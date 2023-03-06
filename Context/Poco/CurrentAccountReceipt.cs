using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class CurrentAccountReceipt{

        public int Id { get; set; }
        public int? ReceiptType { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }

        [ForeignKey("WorkingPeriod")]
        public int? WorkingPeriodId { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public string Explanation { get; set; }

        public virtual WorkingPeriod WorkingPeriod { get; set; }
        public virtual Firm Firm { get; set; }

    }
}