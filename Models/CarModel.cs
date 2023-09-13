namespace HekaMiniumApi.Models
{
  public class CarModel
  {
    public int Id { get; set; }
    public string PlateNo {get; set;}
    public string Model {get; set;}
    public string ModelYear {get; set;}

    #region  VISUAL ELEMENTS
      public string InspectionDate {get; set;}
      public string InsuranceDate {get; set;}
      public string CurrentMileage { get; set; }
      public string MaintenanceMileage { get; set; }
      public string[] MonthlyFuelCost { get; set; }
      public string[] MonthlyMileage { get; set; }
      public int DetailCode { get; set; }
      public DateTime? DetailDate { get; set; }
      public string Value { get; set; }
      public CarDetailModel[] Details { get; set; }
    #endregion
  }
}