using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class AttachmentCategory{
        public int Id { get; set; }
        public string CategoryName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public virtual Plant Plant { get; set; }
    }
}