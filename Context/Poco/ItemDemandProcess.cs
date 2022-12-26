using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemDemandProcess{
        public int Id { get; set; }

        [ForeignKey("ItemDemandDetail")]
        public int? ItemDemandDetailId { get; set; }

        [ForeignKey("Process")]
        public int? ProcessId { get; set; }

        public int? ProcessOrder { get; set; }
        public int? ProcessStatus { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("SysUser")]
        public int? AssignedUserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ItemDemandDetail ItemDemandDetail { get; set; }
        public virtual Process Process { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}