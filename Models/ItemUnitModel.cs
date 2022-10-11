namespace HekaMiniumApi.Models{
    public class ItemUnitModel{
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public int? UnitTypeId { get; set; }
        public bool? IsMainUnit { get; set; }
        public bool? IsDefaultUnit { get; set; }
        public decimal? Divider { get; set; }
        public decimal? Multiplier { get; set; }
        public int? OrderInItem { get; set; }

    }
}