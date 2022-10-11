namespace HekaMiniumApi.Models{
    public class ProjectFieldServiceAttachmentModel{
        public int Id { get; set; }
        public int? ProjectFieldServiceId { get; set; }
        public int? ProjectFieldServiceDetailId { get; set; }

        public byte[] FileContent { get; set; }
        public string FileHeader { get; set; }
        public string FileExtension { get; set; }

        #region VISUAL ELEMENTS
        public string ContentBase64 { get; set; }
        #endregion
    }
}