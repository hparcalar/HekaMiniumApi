namespace HekaMiniumApi.Models{
    public class ProjectCategoryModel{
        public int Id { get; set; }
        public string ProjectCategoryCode { get; set; }
        public string ProjectCategoryName { get; set; }
        public bool IsActive { get; set; }
        public int? PlantId { get; set; }
    }
}