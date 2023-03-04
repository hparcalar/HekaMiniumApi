using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class FirmPeriodStatus{
        public int Id { get; set; }

        [ForeignKey("Firm")]
        public int FirmId { get; set; }

        [ForeignKey("WorkingPeriod")]
        public int WorkingPeriodId { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }

        public virtual Firm Firm { get; set; }
        public virtual WorkingPeriod WorkingPeriod { get; set; }
    }
}