using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Stocktaking{
        public int Id { get; set; }
        public string StocktakingNo { get; set; }
        public int StocktakingType { get; set; }
        public DateTime? StocktakingDate { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("InWarehouse")]
        public int? InWarehouseId { get; set; }

        [ForeignKey("OutWarehouse")]
        public int? OutWarehouseId { get; set; }
        public int? StocktakingStatus { get; set; }

        [ForeignKey("SysUser")]
        public int? SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Warehouse InWarehouse { get; set; }
        public virtual Warehouse OutWarehouse { get; set; }
    }
}