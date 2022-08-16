using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectTask{
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        [ForeignKey("ProjectPhase")]
        public int? ProjectPhaseId { get; set; }

        [ForeignKey("AssigneeUser")]
        public int? AssigneeId { get; set; }

        [ForeignKey("UserTeam")]
        public int? UserTeamId { get; set; }

        public string TaskName { get; set; }
        public string Explanation { get; set; }
        public int TaskStatus { get; set; }

        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Project Project { get; set; }
        public virtual ProjectPhase ProjectPhase { get; set; }
        public virtual SysUser AssigneeUser { get; set; }
        public virtual UserTeam UserTeam { get; set; }
    }
}