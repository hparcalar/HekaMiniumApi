using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using HekaMiniumApi.Context;
using HekaMiniumApi.Models;
using HekaMiniumApi.Models.Operational;
using Microsoft.AspNetCore.Cors;
using HekaMiniumApi.Helpers;
using HekaMiniumApi.Business;
using HekaMiniumApi.Models.Constants;

namespace HekaMiniumApi.Controllers
{

  [Authorize]
  [ApiController]
  [Route("[controller]")]
  [EnableCors()]
  public class InvoiceController : HekaControllerBase
  {
    public InvoiceController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    [Route("List/{invoiceType}")]
    public IEnumerable<InvoiceModel> Get(int invoiceType) // invoice type > 100 -> SALES, < 100 PURCHASING
    {
      InvoiceModel[] data = new InvoiceModel[0];
      try
      {
        data = _context.Invoice.Where(d =>
            (invoiceType == 100 && d.ReceiptType > 100) || (invoiceType == 0 && d.ReceiptType < 100)
        ).Select(d => new InvoiceModel
        {
          Id = d.Id,
          ReceiptNo = d.ReceiptNo,
          ReceiptType = d.ReceiptType,
          ReceiptDate = d.ReceiptDate,
          FirmId = d.FirmId,
          FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
          FirmName = d.Firm != null ? d.Firm.FirmName : "",
          Explanation = d.Explanation,
          IsEInvoice = d.IsEInvoice,
          EInvoiceStatus = d.EInvoiceStatus,
          SubTotal = d.SubTotal,
          TaxTotal = d.TaxTotal,
          OverallTotal = d.OverallTotal,
          IsTaxIncluded = d.IsTaxIncluded,
          CurrentAccountReceiptId = d.CurrentAccountReceiptId,
          WorkingPeriodId = d.WorkingPeriodId,
          InvoiceTypeText = ConstInvoiceType.GetDesc(d.ReceiptType ?? 0),
        })
        .OrderByDescending(d => d.ReceiptDate)
        .ToArray();
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("{id}/{invoiceType}")]
    public InvoiceModel GetById(int id, int receiptType)
    {
      InvoiceModel data = new InvoiceModel();
      try
      {
        data = _context.Invoice.Where(d => d.Id == id).Select(d => new InvoiceModel
        {
          Id = d.Id,
          ReceiptNo = d.ReceiptNo,
          ReceiptType = d.ReceiptType,
          ReceiptDate = d.ReceiptDate,
          FirmId = d.FirmId,
          FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
          FirmName = d.Firm != null ? d.Firm.FirmName : "",
          Explanation = d.Explanation,
          IsEInvoice = d.IsEInvoice,
          EInvoiceStatus = d.EInvoiceStatus,
          SubTotal = d.SubTotal,
          TaxTotal = d.TaxTotal,
          OverallTotal = d.OverallTotal,
          IsTaxIncluded = d.IsTaxIncluded,
          CurrentAccountReceiptId = d.CurrentAccountReceiptId,
          WorkingPeriodId = d.WorkingPeriodId,
          InvoiceTypeText = ConstInvoiceType.GetDesc(d.ReceiptType ?? 0),
        }).FirstOrDefault();

        if (data != null && data.Id > 0)
        {
          data.InvoiceTypeList = receiptType > 100 ? ConstInvoiceType.Sales : ConstInvoiceType.Purchasing;
          data.Details = _context.InvoiceReceiptDetail.Where(d => d.InvoiceId == data.Id)
              .Select(d => new InvoiceReceiptDetailModel
              {
                Id = d.Id,
                InvoiceId = d.InvoiceId,
                ItemReceiptDetailId = d.ItemReceiptDetailId,
                ItemCode = d.ItemReceiptDetail.Item != null ? d.ItemReceiptDetail.Item.ItemCode : "",
                ItemName = d.ItemReceiptDetail.Item != null ? d.ItemReceiptDetail.Item.ItemName : "",
                BrandCode = d.ItemReceiptDetail.Brand != null ? d.ItemReceiptDetail.Brand.BrandCode : "",
                BrandName = d.ItemReceiptDetail.Brand != null ? d.ItemReceiptDetail.Brand.BrandName : "",
                BrandModelCode = d.ItemReceiptDetail.BrandModel != null ? d.ItemReceiptDetail.BrandModel.BrandModelCode : "",
                BrandModelName = d.ItemReceiptDetail.BrandModel != null ? d.ItemReceiptDetail.BrandModel.BrandModelName : "",
                UnitCode = d.ItemReceiptDetail.UnitType != null ? d.ItemReceiptDetail.UnitType.UnitTypeCode : "",
                UnitName = d.ItemReceiptDetail.UnitType != null ? d.ItemReceiptDetail.UnitType.UnitTypeName : "",
                ForexCode = d.ItemReceiptDetail.Forex != null ? d.ItemReceiptDetail.Forex.ForexCode : "",
                ProjectCode = d.ItemReceiptDetail.Project != null ? d.ItemReceiptDetail.Project.ProjectCode : "",
                ProjectName = d.ItemReceiptDetail.Project != null ? d.ItemReceiptDetail.Project.ProjectName : "",
                UnitPrice = d.ItemReceiptDetail.UnitPrice,
                ForexId = d.ItemReceiptDetail.ForexId,
                ForexRate = d.ItemReceiptDetail.ForexRate,
                ForexUnitPrice = d.ItemReceiptDetail.ForexUnitPrice,
                Quantity = d.ItemReceiptDetail.Quantity,
                TaxIncluded = d.ItemReceiptDetail.TaxIncluded,
                TaxRate = d.ItemReceiptDetail.TaxRate,
                TaxPrice = d.ItemReceiptDetail.TaxPrice,
                ForexTaxPrice = d.ItemReceiptDetail.ForexTaxPrice,
                SubTotal = d.ItemReceiptDetail.SubTotal,
                ForexSubTotal = d.ItemReceiptDetail.ForexSubTotal,
                OverallTotal = d.ItemReceiptDetail.OverallTotal,
                ForexOverallTotal = d.ItemReceiptDetail.ForexOverallTotal,
              }).ToArray();

          foreach (var item in data.Details)
          {
            item.OrderConsumes = _context.ItemOrderConsume.Where(d => d.ConsumerItemReceiptDetailId == item.Id)
              .Select(d => new ItemOrderConsumeModel
              {
                Id = d.Id,
                ConsumeDate = d.ConsumeDate,
                ConsumeNetQuantity = d.ConsumeNetQuantity,
                ConsumerItemReceiptDetailId = d.ConsumerItemReceiptDetailId,
                ContributeDate = d.ContributeDate,
                ContributeNetQuantity = d.ContributeNetQuantity,
                ContributerItemReceiptDetailId = d.ContributerItemReceiptDetailId,
                ItemOrderDetailId = d.ItemOrderDetailId,
              }).ToArray();
          }
        }
        else
        {
          if (data == null)
            data = new InvoiceModel();

          data.ReceiptType = receiptType;
          data.InvoiceTypeList = receiptType > 100 ? ConstInvoiceType.Sales : ConstInvoiceType.Purchasing;
          data.ReceiptNo = GetNextInvoiceNumber(receiptType);
          data.Details = new InvoiceReceiptDetailModel[0];
        }
      }
      catch
      {

      }

      return data;
    }

    private string GetNextInvoiceNumber(int invoiceType)
    {
      try
      {
        int nextNumber = 1;
        var lastRecord = _context.Invoice.Where(d => d.ReceiptType == invoiceType)
            .OrderByDescending(d => d.ReceiptNo).Select(d => d.ReceiptNo).FirstOrDefault();
        if (lastRecord != null && !string.IsNullOrEmpty(lastRecord))
          nextNumber = Convert.ToInt32(lastRecord) + 1;

        return string.Format("{0:000000}", nextNumber);
      }
      catch (System.Exception)
      {

      }

      return string.Empty;
    }
  }
}