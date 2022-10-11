using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Warehouse{
        public int Id { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public int WarehouseType { get; set; }

        public bool IsActive { get; set; }
        public bool? AllowEntry { get; set; }
        public bool? AllowDelivery { get; set; }

        public virtual Plant Plant { get; set; }
    }
}