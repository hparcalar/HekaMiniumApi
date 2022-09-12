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

namespace HekaMiniumApi.Controllers{

    [Authorize]
    [ApiController] 
    [Route("[controller]")]
    [EnableCors()]
     public class ItemReceiptController : HekaControllerBase{
        public ItemReceiptController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        [Route("List/{receiptType}")]
        public IEnumerable<ItemReceiptModel> Get(int receiptType) // receipt type > 100 -> SALES, < 100 PURCHASING
        {
            ItemReceiptModel[] data = new ItemReceiptModel[0];
            try
            {
                data = _context.ItemReceipt.Where(d => 
                    (receiptType == 100 && d.ReceiptType > 100) || (receiptType == 0 && d.ReceiptType < 100)
                ).Select(d => new ItemReceiptModel{
                    Id = d.Id,
                    ReceiptStatus = d.ReceiptStatus,
                    DocumentNo = d.DocumentNo,
                    ReceiptType = d.ReceiptType,
                    Explanation = d.Explanation,
                    PlantId = d.PlantId,
                    ReceiptDate = d.ReceiptDate,
                    ReceiptNo = d.ReceiptNo,
                    FirmId = d.FirmId,
                    FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                    FirmName = d.Firm != null ? d.Firm.FirmName : "",
                    InWarehouseId = d.InWarehouseId,
                    IsInvoiced = d.IsInvoiced,
                    OutWarehouseId = d.OutWarehouseId,
                    WarehouseCode = d.InWarehouse != null ? d.InWarehouse.WarehouseCode : "",
                    WarehouseName = d.InWarehouse != null ? d.InWarehouse.WarehouseName : "",
                    // StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" : 
                    //                 d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                    //                 d.ReceiptStatus == 2 ? "Sipariş tamamlandı" :
                    //                 d.ReceiptStatus == 3 ? "İptal edildi" : "",
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
        [Route("{id}/{receiptType}")]
        public ItemReceiptModel GetById(int id, int receiptType)
        {
            ItemReceiptModel data = new ItemReceiptModel();
            try
            {
                data = _context.ItemReceipt.Where(d => d.Id == id).Select(d => new ItemReceiptModel{
                        Id = d.Id,
                        ReceiptStatus = d.ReceiptStatus,
                        DocumentNo = d.DocumentNo,
                        ReceiptType = d.ReceiptType,
                        Explanation = d.Explanation,
                        PlantId = d.PlantId,
                        ReceiptDate = d.ReceiptDate,
                        ReceiptNo = d.ReceiptNo,
                        FirmId = d.FirmId,
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        InWarehouseId = d.InWarehouseId,
                        IsInvoiced = d.IsInvoiced,
                        OutWarehouseId = d.OutWarehouseId,
                        WarehouseCode = d.InWarehouse != null ? d.InWarehouse.WarehouseCode : "",
                        WarehouseName = d.InWarehouse != null ? d.InWarehouse.WarehouseName : "",
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.Details = _context.ItemReceiptDetail.Where(d => d.ItemReceiptId == data.Id)
                        .Select(d => new ItemReceiptDetailModel{
                            Id = d.Id,
                            ReceiptStatus = d.ReceiptStatus,
                            Explanation = d.Explanation,
                            ItemReceiptId = d.ItemReceiptId,
                            ItemId = d.ItemId,
                            LineNumber = d.LineNumber,
                            NetQuantity = d.NetQuantity,
                            Quantity = d.Quantity,
                            UnitId = d.UnitId,
                            AlternatingQuantity = d.AlternatingQuantity,
                            BrandId = d.BrandId,
                            BrandModelId = d.BrandModelId,
                            DiscountPrice = d.DiscountPrice,
                            DiscountRate = d.DiscountRate,
                            ForexDiscountPrice = d.ForexDiscountPrice,
                            ForexId = d.ForexId,
                            ForexOverallTotal = d.ForexOverallTotal,
                            ForexRate = d.ForexRate,
                            ForexSubTotal =d.ForexSubTotal,
                            ForexTaxPrice = d.ForexTaxPrice,
                            ForexUnitPrice = d.ForexUnitPrice,
                            GrossQuantity = d.GrossQuantity,
                            OverallTotal = d.OverallTotal,
                            ProjectId = d.ProjectId,
                            SubTotal = d.SubTotal,
                            TaxIncluded = d.TaxIncluded,
                            TaxPrice = d.TaxPrice,
                            TaxRate =d.TaxRate,
                            UnitPrice = d.UnitPrice,
                            UsedNetQuantity = d.UsedNetQuantity,
                            BrandCode = d.Brand != null ? d.Brand.BrandCode : "",
                            BrandName = d.Brand != null ? d.Brand.BrandName : "",
                            BrandModelCode = d.BrandModel != null ? d.BrandModel.BrandModelCode : "",
                            BrandModelName = d.BrandModel != null ? d.BrandModel.BrandModelName : "",
                            ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                            ProjectCode = d.Project != null ? d.Project.ProjectCode : "",
                            ProjectName = d.Project != null ? d.Project.ProjectName : "",
                            ItemCode = d.Item != null ? d.Item.ItemCode : "",
                            ItemName = d.Item != null ? d.Item.ItemName : "",
                            UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                            UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                        }).ToArray();
                }
                else{
                    if (data == null)
                        data = new ItemReceiptModel();

                    data.ReceiptNo = GetNextReceiptNumber(receiptType);
                    data.Details = new ItemReceiptDetailModel[0];
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        private string GetNextReceiptNumber(int receiptType){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.ItemReceipt.Where(d => d.ReceiptType == receiptType)
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
        public BusinessResult Post(ItemReceiptModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                // after check list
                List<int> orderDetailsWillBeChecked = new List<int>();
                List<int> receiptDetailsWillBeChecked = new List<int>();

                var dbObj = _context.ItemReceipt.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    model.ReceiptNo = GetNextReceiptNumber(model.ReceiptType);

                    dbObj = new ItemReceipt();
                    dbObj.ReceiptNo = model.ReceiptNo;
                    _context.ItemReceipt.Add(dbObj);
                }

                // keep constants
                var currentRcNo = dbObj.ReceiptNo;
                model.MapTo(dbObj);

                // replace constants after auto mapping
                dbObj.ReceiptNo = currentRcNo;

                #region SAVE DETAILS
                var currentDetails = _context.ItemReceiptDetail.Where(d => d.ItemReceiptId == dbObj.Id).ToArray();

                var removedDetails = currentDetails.Where(d => !model.Details.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedDetails)
                {
                    // check if there are any consumers of this receipt detail
                    if (_context.ItemReceiptConsume.Any(d => d.ConsumedReceiptDetailId == item.Id))
                        throw new Exception("Bu irsaliye kalemini kullanan başka fişler mevcut. Önce onları silmelisiniz.");

                    // set orders consumed/contributed to be free to consume again
                    var consumings = _context.ItemOrderConsume.Where(d => d.ContributerItemReceiptDetailId == item.Id || d.ConsumerItemReceiptDetailId == item.Id).ToArray();
                    foreach (var cons in consumings)
                    {
                        if (cons.ItemOrderDetailId != null && !orderDetailsWillBeChecked.Contains(cons.ItemOrderDetailId ?? 0))
                            orderDetailsWillBeChecked.Add(cons.ItemOrderDetailId ?? 0);

                        _context.ItemOrderConsume.Remove(cons);
                    }

                    _context.ItemReceiptDetail.Remove(item);
                }

                foreach (var item in model.Details)
                {
                    var dbDetail = _context.ItemReceiptDetail.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new ItemReceiptDetail();
                        _context.ItemReceiptDetail.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.ItemReceipt = dbObj;

                    // check receipts which consume this receipt detail if any not enough quantity exists after new quantity of this receipt detail
                    decimal? consumersTotal = _context.ItemReceiptConsume.Where(d => d.ConsumedReceiptDetailId == item.Id).Sum(d => d.ConsumeNetQuantity);
                    if ((consumersTotal ?? 0) > 0 && (consumersTotal ?? 0) > item.Quantity)
                        throw new Exception("Bu irsaliye kalemini kullanıp miktarı daha fazla olan fişler mevcut. Yeni miktar " + 
                            string.Format("{0:N2}", (consumersTotal ?? 0)) + " altında olamaz.");

                    // update orders consumed/contributed to be calculated again
                    var consumings = _context.ItemOrderConsume.Where(d => d.ContributerItemReceiptDetailId == item.Id || d.ConsumerItemReceiptDetailId == item.Id).ToArray();
                    foreach (var cons in consumings)
                    {
                        if (cons.ContributerItemReceiptDetailId == item.Id)
                            cons.ContributeNetQuantity = item.Quantity;
                        if (cons.ConsumerItemReceiptDetailId == item.Id &&
                            _context.ItemOrderConsume.Where(d => d.ConsumerItemReceiptDetailId == item.Id).Count() == 1)
                            cons.ConsumeNetQuantity = item.Quantity;

                        if (cons.ItemOrderDetailId != null && !orderDetailsWillBeChecked.Contains(cons.ItemOrderDetailId ?? 0))
                            orderDetailsWillBeChecked.Add(cons.ItemOrderDetailId ?? 0);
                    }

                    // update item receipts consumed
                    var receiptConsumed = _context.ItemReceiptConsume.Where(d => d.ConsumerReceiptDetailId == item.Id).ToArray();
                    foreach (var cons in receiptConsumed)
                    {
                        if (_context.ItemReceiptConsume.Where(d => d.ConsumerReceiptDetailId == item.Id).Count() == 1)
                            cons.ConsumeNetQuantity = item.Quantity;

                        if (cons.ConsumedReceiptDetailId != null && !receiptDetailsWillBeChecked.Contains(cons.ConsumerReceiptDetailId ?? 0))
                            receiptDetailsWillBeChecked.Add(cons.ConsumerReceiptDetailId ?? 0);
                    }
                }
                #endregion

                _context.SaveChanges();

                // apply after check
                using (HekaMiniumSchema checkContext = SchemaFactory.CreateContext()){
                    using (ItemManagementBO bObj = new ItemManagementBO(checkContext)){
                        foreach (var rdId in receiptDetailsWillBeChecked)
                        {
                            bObj.CheckReceiptDetail(rdId);
                        }
                    }

                    using (OrderManagementBO bObj = new OrderManagementBO(checkContext)){
                        foreach (var odId in orderDetailsWillBeChecked)
                        {
                            bObj.CheckOrderDetail(odId);
                        }
                    }

                    checkContext.SaveChanges();
                }

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

        [Authorize(Policy = "WebUser")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                // after check list
                List<int> orderDetailsWillBeChecked = new List<int>();

                var dbObj = _context.ItemReceipt.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen irsaliye bilgisi bulunamadı.");

                var details = _context.ItemReceiptDetail.Where(d => d.ItemReceiptId == id).ToArray();
                foreach (var item in details)
                {
                    // check if there are any consumers of this receipt detail
                    if (_context.ItemReceiptConsume.Any(d => d.ConsumedReceiptDetailId == item.Id))
                        throw new Exception("Bir irsaliye kalemini kullanan başka fişler mevcut. Önce onları silmelisiniz.");

                    // set orders consumed/contributed to be free to consume again
                    var consumings = _context.ItemOrderConsume.Where(d => d.ContributerItemReceiptDetailId == item.Id || d.ConsumerItemReceiptDetailId == item.Id).ToArray();
                    foreach (var cons in consumings)
                    {
                        if (cons.ItemOrderDetailId != null && !orderDetailsWillBeChecked.Contains(cons.ItemOrderDetailId ?? 0))
                            orderDetailsWillBeChecked.Add(cons.ItemOrderDetailId ?? 0);

                        _context.ItemOrderConsume.Remove(cons);
                    }

                    _context.ItemReceiptDetail.Remove(item);
                }

                _context.ItemReceipt.Remove(dbObj);

                _context.SaveChanges();
                result.Result=true;
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