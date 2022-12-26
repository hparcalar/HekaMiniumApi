namespace HekaMiniumApi.Models{
    public class AttachmentModel{
        public int Id { get; set; }
        public Nullable<int> RecordId { get; set; }
        public Nullable<int> RecordType { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public byte[] FileContent { get; set; }
        public bool? IsOfferDoc { get; set; }
    }
}