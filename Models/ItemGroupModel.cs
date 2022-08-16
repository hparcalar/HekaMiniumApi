namespace HekaMiniumApi.Models{
    public class ItemGroupModel{
        public int Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string Explanation { get; set; }
        public string RecordIcon { get; set; }
        public int? ItemCategoryId { get; set; }

        public int? OrderInCategory { get; set; }
        public bool IsActive { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        #endregion
    }
}