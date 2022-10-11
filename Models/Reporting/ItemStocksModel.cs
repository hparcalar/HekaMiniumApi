namespace HekaMiniumApi.Models.Reporting{
    public class ItemStocksModel{
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int? WarehouseId { get; set; }
        public int? ReceiptType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public decimal? InQuantity { get; set; }
        public decimal? OutQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
    }
}