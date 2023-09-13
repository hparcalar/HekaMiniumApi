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
            (invoiceType == 100 && d.ReceiptType > 100) || (invoiceType == 1 && d.ReceiptType < 100)
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
    public InvoiceModel GetById(int id, int invoiceType)
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
          data.InvoiceTypeList = invoiceType > 100 ? ConstInvoiceType.Sales : ConstInvoiceType.Purchasing;
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

          data.ReceiptType = invoiceType;
          data.InvoiceTypeList = invoiceType > 100 ? ConstInvoiceType.Sales : ConstInvoiceType.Purchasing;
          data.ReceiptNo = GetNextInvoiceNumber(invoiceType);
          data.Details = new InvoiceReceiptDetailModel[0];
        }
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("OpenDetails")]
    [Authorize(Policy = "WebUser")]
    public IEnumerable<ItemReceiptDetailModel> GetReceiptOpenDetails()
    {
      ItemReceiptDetailModel[] data = new ItemReceiptDetailModel[0];
      try
      {
        data = _context.ItemReceiptDetail.Select(d => new ItemReceiptDetailModel
        {
          Id = d.Id,
          ReceiptDate = d.ItemReceipt.ReceiptDate,
          ReceiptNo = d.ItemReceipt.ReceiptNo,
          FirmId = d.ItemReceipt.Firm != null ? d.ItemReceipt.Firm.Id : 0,
          FirmCode = d.ItemReceipt.Firm != null ? d.ItemReceipt.Firm.FirmCode : "",
          FirmName = d.ItemReceipt.Firm != null ? d.ItemReceipt.Firm.FirmName : "",
          ReceiptStatus = d.ReceiptStatus,
          Explanation = d.Explanation,
          //ItemOrderId = d.ItemOrderId,
          ItemId = d.ItemId,
          LineNumber = d.LineNumber,
          NetQuantity = d.NetQuantity,
          Quantity = d.Quantity,
          //IsContracted = d.IsContracted,
          UnitId = d.UnitId,
          AlternatingQuantity = d.AlternatingQuantity,
          BrandId = d.BrandId,
          BrandModelId = d.BrandModelId,
          DiscountPrice = d.DiscountPrice,
          DiscountRate = d.DiscountRate,
          ForexDiscountPrice = d.ForexDiscountPrice,
          ForexId = d.ItemOrderDetail.ForexId,
          ForexOverallTotal = d.ForexOverallTotal,
          ForexRate = d.ItemOrderDetail.ForexRate,
          ForexSubTotal = d.ForexSubTotal,
          ForexTaxPrice = d.ForexTaxPrice,
          ForexUnitPrice = d.ForexUnitPrice,
          GrossQuantity = d.GrossQuantity,
          PartDimensions = d.PartDimensions,
          PartNo = d.PartNo,
          //FirmId = d.ItemOrder.FirmId,
          ItemDemandDetailId = d.ItemDemandDetailId,
          OverallTotal = d.ItemOrderDetail.OverallTotal,
          ProjectId = d.ProjectId,
          SubTotal = d.SubTotal,
          TaxIncluded = d.TaxIncluded,
          TaxPrice = d.TaxPrice,
          TaxRate = d.ItemOrderDetail.TaxRate,
          //ItemExplanation = d.ItemExplanation,
          UnitPrice = d.ItemOrderDetail.UnitPrice,
          UsedNetQuantity = d.UsedNetQuantity,
          //DenialExplanation = d.DenialExplanation,
          BrandCode = d.Brand != null ? d.Brand.BrandCode : "",
          BrandName = d.Brand != null ? d.Brand.BrandName : "",
          BrandModelCode = d.BrandModel != null ? d.BrandModel.BrandModelCode : "",
          BrandModelName = d.BrandModel != null ? d.BrandModel.BrandModelName : "",
          //DeadlineDate = d.ItemOrder.DeadlineDate,
          ForexCode = d.ItemOrderDetail.Forex != null ? d.ItemOrderDetail.Forex.ForexCode : "",
          /* ProjectCode = d.ItemOrderDetail.Project != null ? d.ItemOrderDetail.Project.ProjectCode : "",
          ProjectName = d.ItemOrderDetail.Project != null ? d.ItemOrderDetail.Project.ProjectName : "", */
          ItemCode = d.Item != null ? d.Item.ItemCode : "",
          ItemName = d.Item != null ? d.Item.ItemName : "",
          UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
          UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
          /* StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" :
                            d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                            d.ReceiptStatus == 2 ? "Sipariş iletildi" :
                            d.ReceiptStatus == 3 ? "Sipariş tamamlandı" :
                            d.ReceiptStatus == 4 ? "İptal edildi" :
                            d.ReceiptStatus == 5 ? "Kısmi teslim alındı" : "", */
        })
        .OrderByDescending(d => d.ReceiptNo)
        .ToArray();
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

    [Authorize(Policy = "WebUser")]
    [HttpPost]
    public BusinessResult Post(InvoiceModel model){
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.Invoice.FirstOrDefault(d => d.Id == model.Id);
        if (dbObj == null){
          dbObj = new Invoice();
          dbObj.ReceiptNo = GetNextInvoiceNumber(model.ReceiptType ?? 0);
          _context.Invoice.Add(dbObj);
        }
        model.MapTo(dbObj);
        _context.SaveChanges();
        foreach (var item in model.Details)
          {
            var dbItem = _context.InvoiceReceiptDetail.FirstOrDefault(d => d.Id == item.Id);
            if (dbItem == null){
              dbItem = new InvoiceReceiptDetail();
              _context.InvoiceReceiptDetail.Add(dbItem);
            }
            item.MapTo(dbItem);
            dbItem.InvoiceId = dbObj.Id;
          }
        
        var caReceipt = _context.CurrentAccountReceipt.FirstOrDefault(d => d.Id == model.Id);
        if (caReceipt == null){
          caReceipt = new CurrentAccountReceipt();
          _context.CurrentAccountReceipt.Add(caReceipt);
        }
        model.MapTo(caReceipt);
        _context.SaveChanges();
        foreach (var item in model.Details){
          var caItem = _context.CurrentAccountReceiptDetail.FirstOrDefault(d => d.Id == item.Id);
          if (caItem == null){
            caItem = new CurrentAccountReceiptDetail();
            _context.CurrentAccountReceiptDetail.Add(caItem);
          }
          item.MapTo(caItem);
          caItem.CurrentAccountReceiptId = dbObj.Id;
        }

        _context.SaveChanges();
        result.Result=true;
        result.RecordId = dbObj.Id;
      }
      catch (System.Exception ex)
      {
        result.Result=false;
        result.ErrorMessage = ex.Message;
      }

      return result;
    }
  }
}