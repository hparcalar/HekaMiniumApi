using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Plant{
        public int Id { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public bool IsActive { get; set; }
    }
}