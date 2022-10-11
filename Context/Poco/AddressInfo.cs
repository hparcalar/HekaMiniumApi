using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class AddressInfo{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string OpenAddress { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string DoorNo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Gsm { get; set; }
        public string Email { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public virtual Plant Plant { get; set; }
    }
}