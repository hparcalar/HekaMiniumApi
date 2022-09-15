namespace HekaMiniumApi.Models{
    public class ProjectModel{
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int? PlantId { get; set; }
        public int? FirmId { get; set; }
        public int? ProjectCategoryId { get; set; }
        public int? ProjectPhaseTemplateId { get; set; }

        public string ResponsiblePerson { get; set; }
        public string ResponsibleInfo { get; set; }
        public string FirmLocation { get; set; }
        public decimal? Budget { get; set; }
        public string MeetingExplanation { get; set; }
        public string CriticalExplanation { get; set; }
        public int? ProjectStatus { get; set; }
        public string Explanation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int? ProfitRate { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? ForexRate { get; set; }
        public decimal? OfferForexPrice { get; set; }


        public int? Quantity { get; set; }
        public int? ForexId { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string ProjectCategoryCode { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectStatusText { get; set; }
        public string ForexCode { get; set; }
        public string ForexName { get; set; }
        public ProjectCostItemModel[] CostItems { get; set; }
        #endregion
    }
}