using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context {
    public class SysRole{
        public int Id { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public int RoleAuthType { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoot { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public virtual Plant Plant { get; set; }
    }
}