using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProductionLine{
        public int Id { get; set; }
        public string ProductionLineCode { get; set; }
        public string ProductionLineName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public string Explanation { get; set; }

        public virtual Plant Plant { get; set; }
    }
}