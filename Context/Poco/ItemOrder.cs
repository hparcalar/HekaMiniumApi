using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemOrder{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int ReceiptType { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? DeadlineDate { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public string DocumentNo { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public bool? IsWaybilled { get; set; }
        public int? ReceiptStatus { get; set; }

        public virtual Firm Firm { get; set; }
        public virtual Plant Plant { get; set; }
    }
}