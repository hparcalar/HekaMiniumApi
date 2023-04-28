namespace HekaMiniumApi.Models{
    public class StocktakingModel{
        public int Id { get; set; }
        public string StocktakingNo { get; set; }
        public int StocktakingType { get; set; }
        public DateTime? StocktakingDate { get; set; }
        public string Explanation { get; set; }
        public int? PlantId { get; set; }
        public int? InWarehouseId { get; set; }
        public int? OutWarehouseId { get; set; }
        public int? StocktakingStatus { get; set; }
        public int? SysUserId { get; set; }

        #region VISUAL ELEMENTS
        public StocktakingDetailModel[] Details { get; set; }
        public ReceiptTypeModel[] ReceiptTypeList { get; set; }
        public string StatusText { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string StocktakingTypeText { get; set; }
        public StocktakingDetailModel[] inItems { get; set; }
        public StocktakingDetailModel[] outItems { get; set; }
        #endregion
    }
}