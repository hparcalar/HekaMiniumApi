namespace HekaMiniumApi.Models{
    public class FirmCategoryModel{
        public int Id { get; set; }
        public string FirmCategoryCode { get; set; }
        public string FirmCategoryName { get; set; }
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
    }
}