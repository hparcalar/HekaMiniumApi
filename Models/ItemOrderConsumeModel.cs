namespace HekaMiniumApi.Models{
    public class ItemOrderConsumeModel{
        public int Id { get; set; }
        public int? ItemOrderDetailId { get; set; }
        public int? ContributerItemReceiptDetailId { get; set; } // any existing item receipts to complete this order is contributer
        public int? ConsumerItemReceiptDetailId { get; set; } // any created item receipts to complete this order is consumer
        public decimal? ContributeNetQuantity { get; set; }
        public decimal? ConsumeNetQuantity { get; set; }
        public DateTime? ContributeDate { get; set; }
        public DateTime? ConsumeDate { get; set; }
    }
}