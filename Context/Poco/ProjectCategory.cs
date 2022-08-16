using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectCategory{
        public int Id { get; set; }
        public string ProjectCategoryCode { get; set; }
        public string ProjectCategoryName { get; set; }
        public bool IsActive { get; set; }
        
        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public virtual Plant Plant { get; set; }
    }
}