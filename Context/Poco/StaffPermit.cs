using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class StaffPermit{
        public int Id { get; set; }
        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public string StaffPermitExplanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PermitStatus { get; set; }
        public int? PermitType { get; set; }
        public string StatusText { get; set; }

        public virtual SysUser Staff { get; set;}
    }
}