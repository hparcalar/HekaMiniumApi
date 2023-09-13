using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context
{
  public class CarDetail
  {
    public int Id { get; set; }
    
    [ForeignKey("Car")]
    public int CarId { get; set; }
    public int DetailCode { get; set; }
    public string CostExplanation { get; set; }
    public string Value { get; set; }
    public DateTime? DetailDate { get; set; }

    public virtual Car Car { get; set; }
  }
}