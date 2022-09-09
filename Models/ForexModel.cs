namespace HekaMiniumApi.Models{
    public class ForexModel{
        public int Id { get; set; }
        public string ForexCode { get; set; }
        public string ForexName { get; set; }
        public decimal? LiveRate { get; set; }
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
    }
}