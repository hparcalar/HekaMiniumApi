using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectPhase{
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        public string PhaseTitle { get; set; }
        public string PhaseColor { get; set; }
        public string Explanation { get; set; }
        public int PhaseOrder { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PhaseStatus { get; set; }

        public virtual Project Project { get; set; }
    }
}