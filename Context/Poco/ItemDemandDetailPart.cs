using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ItemDemandDetailPart{
        public int Id { get; set; }

        [ForeignKey("ItemDemandDetail")]
        public int? ItemDemandDetailId { get; set; }

        public int? LineNumber { get; set; }

        public string PartNo { get; set; }
        public decimal? PartHeight { get; set; }
        public decimal? PartQuantity { get; set; }
        public byte[] PartFile { get; set; }
        public string FileType { get; set; }
        public string PartType { get; set; }

        public virtual ItemDemandDetail ItemDemandDetail { get; set; }
    }
}