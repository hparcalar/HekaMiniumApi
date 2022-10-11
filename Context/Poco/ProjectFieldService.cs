using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectFieldService{
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string DocumentNo { get; set; }
        
        [ForeignKey("SysUser")]
        public int? ServiceUserId { get; set; }
        public int? ServiceStatus { get; set; }

        public virtual Project Project { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}