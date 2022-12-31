using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Attachment{
        public int Id { get; set; }
        public Nullable<int> RecordId { get; set; }
        public Nullable<int> RecordType { get; set; }

        [ForeignKey("AttachmentCategory")]
        public Nullable<int> AttachmentCategoryId { get; set; }
        public bool? IsOfferDoc { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public byte[] FileContent { get; set; }
        public string PartNo { get; set; }
        public string SubParts { get; set; }

        public virtual AttachmentCategory AttachmentCategory { get; set; }
    }
}