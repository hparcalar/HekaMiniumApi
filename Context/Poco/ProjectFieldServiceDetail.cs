using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectFieldServiceDetail{
        public int Id { get; set; }

        [ForeignKey("ProjectFieldService")]
        public int? ProjectFieldServiceId { get; set; }

        public int? LineNumber { get; set; }
        public string WorkExplanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? ServiceStatus { get; set; }

        public virtual ProjectFieldService ProjectFieldService { get; set; }
    }
}