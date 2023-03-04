using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class CurrentAccountReceiptDetail{
        public int Id { get; set; }

        [ForeignKey("ExpenseCard")]
        public int? ExpenseId { get; set; }
        public decimal? debit { get; set; }
        public decimal? credit { get; set; }

        [ForeignKey("CurrentAccountReceipt")]
        public int? CurrentAccountReceiptId { get; set; }
        public virtual CurrentAccountReceipt CurrentAccountReceipt { get; set; }
        public virtual ExpenseCard ExpenseCard { get; set; }

    }
}