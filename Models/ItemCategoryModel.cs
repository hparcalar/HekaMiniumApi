namespace HekaMiniumApi.Models{
    public class ItemCategoryModel{
        public int Id { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string Explanation { get; set; }
        public string RecordIcon { get; set; }
        public int? PlantId { get; set; }

        public bool IsActive { get; set; }
    }
}