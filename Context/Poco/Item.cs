using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Item{
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Explanation { get; set; }
        public string RecordIcon { get; set; }
        public int? DefaultTaxRate { get; set; }

        [ForeignKey("ItemCategory")]
        public int? ItemCategoryId { get; set; }

        [ForeignKey("ItemGroup")]
        public int? ItemGroupId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("Brand")]
        public int? BrandId { get; set; }

        [ForeignKey("BrandModel")]
        public int? BrandModelId { get; set; }
        public string SerialNo { get; set; }
        public string Barcode { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual BrandModel BrandModel { get; set; }
    }
}