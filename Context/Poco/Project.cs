using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Project{
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }

        [ForeignKey("ProjectCategory")]
        public int? ProjectCategoryId { get; set; }

        [ForeignKey("ProjectPhaseTemplate")]
        public int? ProjectPhaseTemplateId { get; set; }

        public string ResponsiblePerson { get; set; }
        public string ResponsibleInfo { get; set; }
        public string FirmLocation { get; set; }
        public decimal? Budget { get; set; }
        public int? ProjectStatus { get; set; }
        public string Explanation { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual ProjectCategory ProjectCategory { get; set; }
        public virtual ProjectPhaseTemplate ProjectPhaseTemplate { get; set; }
    }
}