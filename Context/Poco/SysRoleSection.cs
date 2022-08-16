using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class SysRoleSection{
        public int Id { get; set; }

        [ForeignKey("SysRole")]
        public int? SysRoleId { get; set; }

        public string SectionKey { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }

        public virtual SysRole SysRole { get; set; }
    }
}