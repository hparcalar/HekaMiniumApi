using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Forex{
        public int Id { get; set; }
        public string ForexCode { get; set; }
        public string ForexName { get; set; }
        public decimal? LiveRate { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }

        public virtual Plant Plant { get; set; }
    }
}