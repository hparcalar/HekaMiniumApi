namespace HekaMiniumApi.Models{
    public class FirmModel{
        public int Id { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string CommercialTitle { get; set; }
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public int? FirmCategoryId { get; set; }
        public int FirmType { get; set; }
        public int? AddressInfoId { get; set; }
        public bool? IsEInvoice { get; set; }
        public bool? IsEWaybill { get; set; }
        
        public string EInvoiceEndpoint { get; set; }
        public string EInvoiceLogin { get; set; }
        public string EInvoicePassword { get; set; }

        public string EWaybillEndpoint { get; set; }
        public string EWaybillLogin { get; set; }
        public string EWaybillPassword { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCategoryCode { get; set; }
        public string FirmCategoryName { get; set; }
        public string AddressText { get; set; }
        public string PhoneText { get; set; }
        public string AuthorText { get; set; }
        public string EmailText { get; set; }
        #endregion
    }
}