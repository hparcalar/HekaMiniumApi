using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class UnitType{
        public int Id { get; set; }
        public string UnitTypeCode { get; set; }
        public string UnitTypeName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }

        public virtual Plant Plant { get; set; }
    }
}