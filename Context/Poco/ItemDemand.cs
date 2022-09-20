using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemDemand {
        public int Id { get; set; }

        public string ReceiptNo { get; set; }

        public DateTime? ReceiptDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        public bool? IsOrdered { get; set; }
        public int? DemandStatus { get; set; }

        [ForeignKey("SysUser")]
        public int? SysUserId { get; set; }

        public bool? IsContracted { get; set; }

        public virtual SysUser SysUser { get; set; }

        public virtual Project Project { get; set; }

        public virtual Plant Plant { get; set; }
    }
}