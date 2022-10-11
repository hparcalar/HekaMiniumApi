using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ProjectFieldServiceAttachment{
        public int Id { get; set; }

        [ForeignKey("ProjectFieldService")]
        public int? ProjectFieldServiceId { get; set; }

        [ForeignKey("ProjectFieldServiceDetail")]
        public int? ProjectFieldServiceDetailId { get; set; }

        public byte[] FileContent { get; set; }
        public string FileHeader { get; set; }
        public string FileExtension { get; set; }

        public virtual ProjectFieldService ProjectFieldService { get; set; }
        public virtual ProjectFieldServiceDetail ProjectFieldServiceDetail { get; set; }
    }
}