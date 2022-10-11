using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class Firm{
        public int Id { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string CommercialTitle { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }

        [ForeignKey("FirmCategory")]
        public int? FirmCategoryId { get; set; }
        public int FirmType { get; set; }

        [ForeignKey("AddressInfo")]
        public int? AddressInfoId { get; set; }
        public bool? IsEInvoice { get; set; }
        public bool? IsEWaybill { get; set; }
        
        public string EInvoiceEndpoint { get; set; }
        public string EInvoiceLogin { get; set; }
        public string EInvoicePassword { get; set; }

        public string EWaybillEndpoint { get; set; }
        public string EWaybillLogin { get; set; }
        public string EWaybillPassword { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual FirmCategory FirmCategory { get; set; }
        public virtual AddressInfo AddressInfo { get; set; }
    }
}