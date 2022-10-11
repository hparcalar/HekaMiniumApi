using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Machine{
        public int Id { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ProductionLine")]
        public int? ProductionLineId { get; set; }
        public string Explanation { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public DateTime? InventoryDate { get; set; }
        public DateTime? ProductionDate { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual ProductionLine ProductionLine { get; set; }
    }
}