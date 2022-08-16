using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Route{
        public int Id { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public string Explanation { get; set; }
        public decimal? RoutePrice { get; set; }

        [ForeignKey("Forex")]
        public int? ForexId { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Forex Forex { get; set; }
    }
}