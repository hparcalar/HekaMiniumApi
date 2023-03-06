using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class WorkingPeriod{
        public int Id { get; set; }
        public string Year { get; set; }
        public bool IsActive { get; set; }
    }
}