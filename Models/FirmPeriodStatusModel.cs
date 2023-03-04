namespace HekaMiniumApi.Models{
    public class FirmPeriodStatusModel{
        public int Id { get; set; }
        public int FirmId { get; set; }
        public int WorkingPeriodId { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
    }
}