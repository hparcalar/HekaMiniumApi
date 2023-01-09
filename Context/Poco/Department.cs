using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Department{
        public int Id { get; set; }
        public int? DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int PlantId { get; set; }
    }
}