using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Process{
        public int Id { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public decimal? EstimatedDuration { get; set; }
        public decimal? UnitPrice { get; set; }

        [ForeignKey("Forex")]
        public int? ForexId { get; set; }

        public int? ProcessOrder { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Forex Forex { get; set; }
    }
}