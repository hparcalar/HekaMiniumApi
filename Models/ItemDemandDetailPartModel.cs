namespace HekaMiniumApi.Models{
    public class ItemDemandDetailPartModel{
        public int Id { get; set; }
        public int? ItemDemandDetailId { get; set; }

        public int? LineNumber { get; set; }

        public string PartNo { get; set; }
        public decimal? PartHeight { get; set; }
        public decimal? PartQuantity { get; set; }
        public byte[] PartFile { get; set; }
        public string PartType { get; set; }
        public string FileType { get; set; }

        #region VISUAL ELEMENTS
        public string PartBase64 { get; set; }
        public int? AttachmentId { get; set; }
        #endregion
    }
}