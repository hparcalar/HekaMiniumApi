using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectPhaseTemplateDetail{
        public int Id { get; set; }

        [ForeignKey("ProjectPhaseTemplate")]
        public int? ProjectPhaseTemplateId { get; set; }

        public string PhaseTitle { get; set; }
        public string PhaseColor { get; set; }
        public string Explanation { get; set; }
        public int PhaseOrder { get; set; }

        public virtual ProjectPhaseTemplate ProjectPhaseTemplate { get; set; }
    }
}