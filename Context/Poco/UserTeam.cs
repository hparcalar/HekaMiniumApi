using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class UserTeam{
        public int Id { get; set; }
        public string TeamName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("LeaderUser")]
        public int? LeaderUserId { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual SysUser LeaderUser { get; set; }
    }
}