using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class RouteDetail{
        public int Id { get; set; }

        [ForeignKey("Route")]
        public int? RouteId { get; set; }
        public int? LineNumber { get; set; }

        [ForeignKey("Process")]
        public int? ProcessId { get; set; }

        [ForeignKey("Machine")]
        public int? MachineId { get; set; }
        public string Explanation { get; set; }
        public int? ProcessCount { get; set; }

        public virtual Route Route { get; set; }
        public virtual Process Process { get; set; }
        public virtual Machine Machine { get; set; }
    }
}