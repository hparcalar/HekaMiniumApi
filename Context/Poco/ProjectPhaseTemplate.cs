using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectPhaseTemplate{
        public int Id { get; set; }
        public string PhaseTemplateCode { get; set; }
        public string PhaseTemplateName { get; set; }

        [ForeignKey("ProjectCategory")]
        public int? ProjectCategoryId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual ProjectCategory ProjectCategory { get; set; }
    }
}