using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class FirmCategory{
        public int Id { get; set; }
        public string FirmCategoryCode { get; set; }
        public string FirmCategoryName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }

        public virtual Plant Plant { get; set; }
    }
}