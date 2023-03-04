namespace HekaMiniumApi.Models{
    public class CurrentAccountReceiptModel{
        public int Id { get; set; }
        public int? ReceiptType { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? WorkingPeriodId { get; set; }
        public int? FirmId { get; set; }
        public string Explanation { get; set; }
    }
}