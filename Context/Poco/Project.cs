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
        public string MeetingExplanation { get; set; }
        public string CriticalExplanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? ForexRate { get; set; }
        public decimal? OfferForexPrice { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? TotalForexCost { get; set; }

        [ForeignKey("Forex")]
        public int? ForexId { get; set; }
        public int? ProfitRate { get; set; }
        public decimal? OfferPrice { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual ProjectCategory ProjectCategory { get; set; }
        public virtual ProjectPhaseTemplate ProjectPhaseTemplate { get; set; }
        public virtual Forex Forex { get; set; }
    }
}