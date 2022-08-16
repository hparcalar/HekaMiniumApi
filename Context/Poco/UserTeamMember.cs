using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class UserTeamMember{
        public int Id { get; set; }

        [ForeignKey("UserTeam")]
        public int? UserTeamId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public virtual UserTeam UserTeam { get; set; }
        public virtual SysUser User { get; set; }
    }
}