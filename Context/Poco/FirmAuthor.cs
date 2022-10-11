using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class FirmAuthor{
        public int Id { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gsm { get; set; }
        public int? OrderInFirm { get; set; }

        public virtual Firm Firm { get; set; }
    }
}