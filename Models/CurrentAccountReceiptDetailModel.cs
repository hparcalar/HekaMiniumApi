namespace HekaMiniumApi.Models{
    public class CurrentAccountReceiptDetailModel{
        public int Id { get; set; }
        public int? ExpenseId { get; set; }
        public decimal? debit { get; set; }
        public decimal? credit { get; set; }
        public int? CurrentAccountReceiptId { get; set; }
    }
}