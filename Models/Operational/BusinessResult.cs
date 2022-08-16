namespace HekaMiniumApi.Models.Operational{
    public class BusinessResult{
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
        public string InfoMessage { get; set; }
        public string Token { get; set; }
        public int RecordId { get; set; }
        public string AdditionalData { get; set; }
    }
}