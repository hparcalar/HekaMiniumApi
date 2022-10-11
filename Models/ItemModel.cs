namespace HekaMiniumApi.Models{
    public class ItemModel{
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Explanation { get; set; }
        public string RecordIcon { get; set; }
        public int? DefaultTaxRate { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemGroupId { get; set; }
        public int? PlantId { get; set; }
        public int? BrandId { get; set; }
        public int? BrandModelId { get; set; }
        public string SerialNo { get; set; }
        public string Barcode { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string BrandModelCode { get; set; }
        public string BrandModelName { get; set; }
        #endregion
    }
}