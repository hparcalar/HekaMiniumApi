namespace HekaMiniumApi.Models{
    public class ProcessModel{
        public int Id { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public decimal? EstimatedDuration { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ForexId { get; set; }

        /*
        1: Cutting
        */
        public int? ProcessType { get; set; }

        public int? ProcessOrder { get; set; }
    }
}