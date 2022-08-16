using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context {
    public class SysUser{
        public int Id { get; set; }
        public string UserCode { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public string Password { get; set; }
        public string DefaultLanguage { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("SysRole")]
        public int? SysRoleId { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual SysRole SysRole { get; set; }
    }
}