namespace HekaMiniumApi.Models
{
  public class CarDetailModel
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public int DetailCode { get; set; }
    public string CostExplanation { get; set; }
    public string Value { get; set; }
    public DateTime? DetailDate { get; set; }

    #region  VISUAL ELEMENTS
    public string DetailCodeText { get; set; }
    public string CarName { get; set; }
    #endregion
  }
}