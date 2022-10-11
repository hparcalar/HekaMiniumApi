using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Brand{
        public int Id { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public string RecordIcon { get; set; }

        public virtual Plant Plant { get; set; }
    }
}