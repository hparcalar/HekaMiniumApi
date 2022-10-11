namespace HekaMiniumApi.Models{
    public class WarehouseModel{
        public int Id { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public int? PlantId { get; set; }

        public int WarehouseType { get; set; }

        public bool IsActive { get; set; }
        public bool? AllowEntry { get; set; }
        public bool? AllowDelivery { get; set; }
    }
}