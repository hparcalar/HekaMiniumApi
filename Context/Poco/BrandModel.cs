using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class BrandModel{
        public int Id { get; set; }

        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public string BrandModelCode { get; set; }
        public string BrandModelName { get; set; }
        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }
    }
}