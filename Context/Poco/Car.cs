using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context
{
  public class Car
  {
    public Car(){
      this.CarDetails = new HashSet<CarDetail>();
    }
    public int Id { get; set; }
    public string PlateNo { get; set; }
    public string Model { get; set; }
    public string ModelYear { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<CarDetail> CarDetails { get; set; }

  }
}