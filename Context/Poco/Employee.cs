using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Employee{
        public int Id { get; set; }
        public int? EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCardNo { get; set; }
        public decimal? EmployeeHourlyWage { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeAddress { get; set; }
        public DateTime? DateOfStart { get; set; }
        public DateTime? DateOfEnd { get; set; }
        
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}