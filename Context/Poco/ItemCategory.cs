using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemCategory{
        public int Id { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string Explanation { get; set; }
        public string RecordIcon { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public bool IsActive { get; set; }
        public virtual Plant Plant { get; set; }
    }
}