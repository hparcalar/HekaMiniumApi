using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemGroup{
        public int Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string Explanation { get; set; }
        public string RecordIcon { get; set; }

        [ForeignKey("ItemCategory")]
        public int? ItemCategoryId { get; set; }

        public int? OrderInCategory { get; set; }
        public bool IsActive { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
    }
}