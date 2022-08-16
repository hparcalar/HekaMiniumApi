using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemUnit{
        public int Id { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        [ForeignKey("UnitType")]
        public int? UnitTypeId { get; set; }
        public bool? IsMainUnit { get; set; }
        public bool? IsDefaultUnit { get; set; }
        public decimal? Divider { get; set; }
        public decimal? Multiplier { get; set; }
        public int? OrderInItem { get; set; }

        public virtual Item Item { get; set; }
        public virtual UnitType UnitType { get; set; }
    }
}