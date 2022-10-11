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
using HekaMiniumApi.Models.Parameters;

namespace HekaMiniumApi.Controllers{

    [Authorize]
    [ApiController] 
    [Route("[controller]")]
    [EnableCors()]
     public class ItemOrderController : HekaControllerBase{
        public ItemOrderController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        [Route("List/{receiptType}")]
        public IEnumerable<ItemOrderModel> Get(int receiptType)
        {
            ItemOrderModel[] data = new ItemOrderModel[0];
            try
            {
                data = _context.ItemOrder.Where(d => d.ReceiptType == receiptType).Select(d => new ItemOrderModel{
                    Id = d.Id,
                    DeadlineDate = d.DeadlineDate,
                    ReceiptStatus = d.ReceiptStatus,
                    DocumentNo = d.DocumentNo,
                    ReceiptType = d.ReceiptType,
                    Explanation = d.Explanation,
                    IsWaybilled = d.IsWaybilled,
                    PlantId = d.PlantId,
                    ReceiptDate = d.ReceiptDate,
                    IsContracted = d.IsContracted,
                    ReceiptNo = d.ReceiptNo,
                    FirmId = d.FirmId,
                    ItemDemandId = d.ItemDemandId,
                    ItemDemandNo = d.ItemDemand != null ? d.ItemDemand.ReceiptNo : "",
                    FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                    FirmName = d.Firm != null ? d.Firm.FirmName : "",
                    StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" : 
                                    d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                                    d.ReceiptStatus == 2 ? "Sipariş verildi" :
                                    d.ReceiptStatus == 3 ? "Sipariş tamamlandı" :
                                    d.ReceiptStatus == 4 ? "İptal edildi" : "",
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
        [Route("Purchase/WaitingForApprove")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<ItemOrderDetailModel> GetPurchaseWaitingForApprove(){
            ItemOrderDetailModel[] data = new ItemOrderDetailModel[0];
            try
            {
                data = _context.ItemOrderDetail.Where(d => (d.ReceiptStatus ?? 0) == 0 || d.ReceiptStatus == 4).Select(d => new ItemOrderDetailModel{
                    Id = d.Id,
                    ReceiptDate = d.ItemOrder.ReceiptDate,
                    ReceiptNo = d.ItemOrder.ReceiptNo,
                    FirmCode = d.ItemOrder.Firm != null ? d.ItemOrder.Firm.FirmCode : "",
                    FirmName = d.ItemOrder.Firm != null ? d.ItemOrder.Firm.FirmName : "",
                            ReceiptStatus = d.ReceiptStatus,
                            Explanation = d.Explanation,
                            ItemOrderId = d.ItemOrderId,
                            ItemId = d.ItemId,
                            LineNumber = d.LineNumber,
                            NetQuantity = d.NetQuantity,
                            Quantity = d.Quantity,
                            IsContracted = d.IsContracted,
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
                            PartDimensions = d.PartDimensions,
                            PartNo = d.PartNo,
                            ItemDemandDetailId = d.ItemDemandDetailId,
                            OverallTotal = d.OverallTotal,
                            ProjectId = d.ProjectId,
                            SubTotal = d.SubTotal,
                            TaxIncluded = d.TaxIncluded,
                            TaxPrice = d.TaxPrice,
                            TaxRate =d.TaxRate,
                            ItemExplanation = d.ItemExplanation,
                            UnitPrice = d.UnitPrice,
                            FirmId = d.ItemOrder.FirmId,
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
                            StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" : 
                                    d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                                    d.ReceiptStatus == 2 ? "Sipariş verildi" :
                                    d.ReceiptStatus == 3 ? "Sipariş tamamlandı" :
                                    d.ReceiptStatus == 4 ? "İptal edildi" : "",
                })
                .OrderByDescending(d => d.ReceiptDate)
                .ToArray();

                foreach (var item in data)
                {
                    item.DemandConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == item.Id)
                            .Select(d => new ItemDemandConsumeModel{
                                Id = d.Id,
                                ConsumeDate = d.ConsumeDate,
                                ItemDemandDetailId = d.ItemDemandDetailId,
                                ItemOrderDetailId = d.ItemOrderDetailId,
                                ItemId = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemId : null,
                                DemandQuantity = d.ItemDemandDetail != null ? d.ItemDemandDetail.Quantity : 0,
                                ItemCode = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemCode : "",
                                ItemName = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemName : "",
                                ItemExplanation = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemExplanation : "",
                                PartNo = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartNo : "",
                                PartDimensions = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartDimensions : "",
                                ProjectName = d.ItemDemandDetail != null && d.ItemDemandDetail.ItemDemand.Project != null ? d.ItemDemandDetail.ItemDemand.Project.ProjectName : "",
                            }).ToArray();
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("Purchase/OpenDetails")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<ItemOrderDetailModel> GetPurchaseOpenDetails(){
            ItemOrderDetailModel[] data = new ItemOrderDetailModel[0];
            try
            {
                data = _context.ItemOrderDetail.Where(d => (d.ReceiptStatus ?? 0) == 2).Select(d => new ItemOrderDetailModel{
                    Id = d.Id,
                    ReceiptDate = d.ItemOrder.ReceiptDate,
                    ReceiptNo = d.ItemOrder.ReceiptNo,
                    FirmCode = d.ItemOrder.Firm != null ? d.ItemOrder.Firm.FirmCode : "",
                    FirmName = d.ItemOrder.Firm != null ? d.ItemOrder.Firm.FirmName : "",
                    ReceiptStatus = d.ReceiptStatus,
                    Explanation = d.Explanation,
                    ItemOrderId = d.ItemOrderId,
                    ItemId = d.ItemId,
                    LineNumber = d.LineNumber,
                    NetQuantity = d.NetQuantity,
                    Quantity = d.Quantity,
                    IsContracted = d.IsContracted,
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
                    PartDimensions = d.PartDimensions,
                    PartNo = d.PartNo,
                    FirmId = d.ItemOrder.FirmId,
                    ItemDemandDetailId = d.ItemDemandDetailId,
                    OverallTotal = d.OverallTotal,
                    ProjectId = d.ProjectId,
                    SubTotal = d.SubTotal,
                    TaxIncluded = d.TaxIncluded,
                    TaxPrice = d.TaxPrice,
                    TaxRate =d.TaxRate,
                    ItemExplanation = d.ItemExplanation,
                    UnitPrice = d.UnitPrice,
                    UsedNetQuantity = d.UsedNetQuantity,
                    BrandCode = d.Brand != null ? d.Brand.BrandCode : "",
                    BrandName = d.Brand != null ? d.Brand.BrandName : "",
                    BrandModelCode = d.BrandModel != null ? d.BrandModel.BrandModelCode : "",
                    BrandModelName = d.BrandModel != null ? d.BrandModel.BrandModelName : "",
                    DeadlineDate = d.ItemOrder.DeadlineDate,
                    ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                    ProjectCode = d.Project != null ? d.Project.ProjectCode : "",
                    ProjectName = d.Project != null ? d.Project.ProjectName : "",
                    ItemCode = d.Item != null ? d.Item.ItemCode : "",
                    ItemName = d.Item != null ? d.Item.ItemName : "",
                    UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                    UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                    StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" : 
                            d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                            d.ReceiptStatus == 2 ? "Sipariş verildi" :
                            d.ReceiptStatus == 3 ? "Sipariş tamamlandı" :
                            d.ReceiptStatus == 4 ? "İptal edildi" : "",
                })
                .OrderByDescending(d => d.ReceiptDate)
                .ToArray();

                foreach (var item in data)
                {
                    item.DemandConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == item.Id)
                            .Select(d => new ItemDemandConsumeModel{
                                Id = d.Id,
                                ConsumeDate = d.ConsumeDate,
                                ItemDemandDetailId = d.ItemDemandDetailId,
                                ItemOrderDetailId = d.ItemOrderDetailId,
                                ItemId = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemId : null,
                                DemandQuantity = d.ItemDemandDetail != null ? d.ItemDemandDetail.Quantity : 0,
                                ItemCode = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemCode : "",
                                ItemName = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemName : "",
                                ItemExplanation = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemExplanation : "",
                                PartNo = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartNo : "",
                                PartDimensions = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartDimensions : "",
                            }).ToArray();
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "WebUser")]
        [Route("Purchase/ApproveDetails")]
        [HttpPost]
        public BusinessResult ApproveOrderDetails(int[] detailId){
            BusinessResult result = new BusinessResult();

            try
            {
                List<int> _checkListOrderHeaders = new List<int>();

                foreach (var dId in detailId)
                {
                    var dbObj = _context.ItemOrderDetail.FirstOrDefault(d => d.Id == dId);
                    if (dbObj != null){
                        dbObj.ReceiptStatus = 1;

                        if (!_checkListOrderHeaders.Contains(dbObj.ItemOrderId ?? 0))
                            _checkListOrderHeaders.Add(dbObj.ItemOrderId ?? 0);
                    }
                }

                _context.SaveChanges();

                foreach(var item in _checkListOrderHeaders) {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckOrderHeader(item);
                            _nContext.SaveChanges();
                        }
                    }
                }

                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [Authorize(Policy = "WebUser")]
        [Route("MakeForward")]
        [HttpPost]
        public BusinessResult MakeForward(PostIdModel model){
            BusinessResult result = new BusinessResult();
            try
            {
                var dbDetails = _context.ItemOrderDetail.Where(d => d.ItemOrderId == model.Id).ToArray();
                foreach (var item in dbDetails)
                {
                    item.ReceiptStatus = 2;
                }
                _context.SaveChanges();

                using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                    using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                        bObj.CheckOrderHeader(model.Id);
                    }

                    _nContext.SaveChanges();
                }

                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        [Authorize(Policy = "WebUser")]
        [Route("Purchase/DenyDetails")]
        [HttpPost]
        public BusinessResult DenyOrderDetails(int[] detailId){
            BusinessResult result = new BusinessResult();

            try
            {
                List<int> _checkListOrderHeaders = new List<int>();

                foreach (var dId in detailId)
                {
                    var dbObj = _context.ItemOrderDetail.FirstOrDefault(d => d.Id == dId);
                    if (dbObj != null){
                        dbObj.ReceiptStatus = 4;

                        if (!_checkListOrderHeaders.Contains(dbObj.ItemOrderId ?? 0))
                            _checkListOrderHeaders.Add(dbObj.ItemOrderId ?? 0);
                    }
                }

                _context.SaveChanges();

                foreach(var item in _checkListOrderHeaders) {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckOrderHeader(item);
                            _nContext.SaveChanges();
                        }
                    }
                }

                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public ItemOrderModel GetById(int id)
        {
            ItemOrderModel data = new ItemOrderModel();
            try
            {
                data = _context.ItemOrder.Where(d => d.Id == id).Select(d => new ItemOrderModel{
                        Id = d.Id,
                        DeadlineDate = d.DeadlineDate,
                        ReceiptStatus = d.ReceiptStatus,
                        DocumentNo = d.DocumentNo,
                        ReceiptType = d.ReceiptType,
                        Explanation = d.Explanation,
                        IsWaybilled = d.IsWaybilled,
                        PlantId = d.PlantId,
                        ReceiptDate = d.ReceiptDate,
                        ReceiptNo = d.ReceiptNo,
                        IsContracted = d.IsContracted,
                        FirmId = d.FirmId,
                        ItemDemandId = d.ItemDemandId,
                        ItemDemandNo = d.ItemDemand != null ? d.ItemDemand.ReceiptNo : "",
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" : 
                                    d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                                    d.ReceiptStatus == 2 ? "Sipariş verildi" :
                                    d.ReceiptStatus == 3 ? "Sipariş tamamlandı" :
                                    d.ReceiptStatus == 4 ? "İptal edildi" : "",
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.Details = _context.ItemOrderDetail.Where(d => d.ItemOrderId == data.Id)
                        .Select(d => new ItemOrderDetailModel{
                            Id = d.Id,
                            ReceiptStatus = d.ReceiptStatus,
                            Explanation = d.Explanation,
                            ItemOrderId = d.ItemOrderId,
                            ItemId = d.ItemId,
                            LineNumber = d.LineNumber,
                            NetQuantity = d.NetQuantity,
                            Quantity = d.Quantity,
                            IsContracted = d.IsContracted,
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
                            PartDimensions = d.PartDimensions,
                            PartNo = d.PartNo,
                            ItemDemandDetailId = d.ItemDemandDetailId,
                            ItemOfferId = d.ItemOfferDetail != null ? d.ItemOfferDetail.ItemOfferId : null,
                            OverallTotal = d.OverallTotal,
                            ProjectId = d.ProjectId,
                            SubTotal = d.SubTotal,
                            TaxIncluded = d.TaxIncluded,
                            TaxPrice = d.TaxPrice,
                            TaxRate =d.TaxRate,
                            ItemExplanation = d.ItemExplanation,
                            UnitPrice = d.UnitPrice,
                            ItemOfferDetailId = d.ItemOfferDetailId,
                            ItemOfferNo = d.ItemOfferDetail != null ? d.ItemOfferDetail.ItemOffer.ReceiptNo : "",
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
                            StatusText = d.ReceiptStatus == 0 ? "Sipariş oluşturuldu" : 
                                    d.ReceiptStatus == 1 ? "Sipariş onaylandı" :
                                    d.ReceiptStatus == 2 ? "Sipariş verildi" :
                                    d.ReceiptStatus == 3 ? "Sipariş tamamlandı" :
                                    d.ReceiptStatus == 4 ? "İptal edildi" : "",
                        }).ToArray();
                
                    // fetch demand consumings
                    foreach (var item in data.Details)
                    {
                        item.DemandConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == item.Id)
                            .Select(d => new ItemDemandConsumeModel{
                                Id = d.Id,
                                ConsumeDate = d.ConsumeDate,
                                ItemDemandDetailId = d.ItemDemandDetailId,
                                ItemOrderDetailId = d.ItemOrderDetailId,
                                ItemId = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemId : null,
                                DemandQuantity = d.ItemDemandDetail != null ? d.ItemDemandDetail.Quantity : 0,
                                ItemCode = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemCode : "",
                                ItemName = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemName : "",
                                ItemExplanation = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemExplanation : "",
                                PartNo = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartNo : "",
                                PartDimensions = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartDimensions : "",
                            }).ToArray();
                    }
                }
                else{
                    if (data == null)
                        data = new ItemOrderModel();

                    data.ReceiptNo = GetNextOrderNumber();
                    data.Details = new ItemOrderDetailModel[0];
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        private string GetNextOrderNumber(){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.ItemOrder.OrderByDescending(d => d.ReceiptNo).Select(d => d.ReceiptNo).FirstOrDefault();
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
        public BusinessResult Post(ItemOrderModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                // check list
                List<int> _checkListForDemands = new List<int>();
                List<int> _checkListForOfferDetails = new List<int>();
                List<int> _checkListForOffers = new List<int>();

                if (!_context.Plant.Any(d => d.Id == (model.PlantId ?? 0)))
                    model.PlantId = null;

                var dbObj = _context.ItemOrder.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){

                    int nextRcNo = 1;
                    string lastRcNo = _context.ItemOrder.Where(d => d.PlantId == model.PlantId)
                        .OrderByDescending(d => d.ReceiptNo).Select(d => d.ReceiptNo).FirstOrDefault();

                    if (!string.IsNullOrEmpty(lastRcNo)){
                        nextRcNo = Convert.ToInt32(lastRcNo) + 1;
                    }

                    model.ReceiptNo = string.Format("{0:000000}", nextRcNo);

                    if (model.ReceiptDate == null)
                        model.ReceiptDate = DateTime.Now;

                    dbObj = new ItemOrder();
                    dbObj.ReceiptNo = model.ReceiptNo;
                    _context.ItemOrder.Add(dbObj);
                }

                // keep constants
                var currentRcNo = dbObj.ReceiptNo;
                model.MapTo(dbObj);

                // replace constants after auto mapping
                dbObj.ReceiptNo = currentRcNo;

                #region SAVE DETAILS
                var currentDetails = _context.ItemOrderDetail.Where(d => d.ItemOrderId == dbObj.Id).ToArray();

                var removedDetails = currentDetails.Where(d => !model.Details.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedDetails)
                {
                    // set demand detail status to created
                    if (item.ItemDemandDetailId != null){
                        var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == item.ItemDemandDetailId);
                        if (dbDemandDetail != null){
                            dbDemandDetail.DemandStatus = 0;
                        }
                    }

                    var _existingConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == item.Id).ToArray();
                    foreach (var consItem in _existingConsumes)
                    {
                        var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == consItem.ItemDemandDetailId);
                        if (dbDemandDetail != null){
                            dbDemandDetail.DemandStatus = 0;

                            if (!_checkListForDemands.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                _checkListForDemands.Add(dbDemandDetail.ItemDemandId ?? 0);
                        }

                        _context.ItemDemandConsume.Remove(consItem);
                    }

                    if (item.ItemOfferDetailId != null && !_checkListForOfferDetails.Contains(item.ItemOfferDetailId ?? 0)){
                        _checkListForOfferDetails.Add(item.ItemOfferDetailId ?? 0);
                    }

                    _context.ItemOrderDetail.Remove(item);
                }

                foreach (var item in model.Details)
                {
                    var dbDetail = _context.ItemOrderDetail.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new ItemOrderDetail();
                        _context.ItemOrderDetail.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.ItemOrder = dbObj;
                    dbDetail.IsContracted = dbObj.IsContracted;

                    // check & update offer detail status
                    if (item.ItemOfferDetailId != null && !_checkListForOfferDetails.Contains(item.ItemOfferDetailId ?? 0)){
                        _checkListForOfferDetails.Add(item.ItemOfferDetailId ?? 0);
                    }

                    // check & update demand detail status -- NEW --
                    if (item.DemandConsumes != null && item.DemandConsumes.Length > 0){
                        foreach (var consItem in item.DemandConsumes)
                        {
                            var _currentConsume = _context.ItemDemandConsume.FirstOrDefault(d => d.ItemDemandDetailId == consItem.ItemDemandDetailId
                                && d.ItemOrderDetailId == item.Id);
                            if (_currentConsume == null){
                                _currentConsume = new ItemDemandConsume();
                                _context.ItemDemandConsume.Add(_currentConsume);
                            }

                            consItem.MapTo(_currentConsume);
                            _currentConsume.ItemOrderDetail = dbDetail;

                            var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == consItem.ItemDemandDetailId);
                            if (dbDemandDetail != null) {
                                dbDemandDetail.DemandStatus = 2;

                                if (!_checkListForDemands.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                    _checkListForDemands.Add(dbDemandDetail.ItemDemandId ?? 0);
                            }
                        }
                    }
                    else{
                        var _existingConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == item.Id).ToArray();
                        foreach (var consItem in _existingConsumes)
                        {
                            var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == consItem.ItemDemandDetailId);
                            if (dbDemandDetail != null){
                                dbDemandDetail.DemandStatus = 0;

                                if (!_checkListForDemands.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                    _checkListForDemands.Add(dbDemandDetail.ItemDemandId ?? 0);
                            }

                            _context.ItemDemandConsume.Remove(consItem);
                        }
                    }

                }
                #endregion

                foreach (var item in _checkListForOfferDetails)
                {
                    var dbOfferDetail = _context.ItemOfferDetail.FirstOrDefault(d => d.Id == item);
                    if (dbOfferDetail != null){
                        if (dbOfferDetail.ItemOfferId != null && !_checkListForOffers.Contains(dbOfferDetail.ItemOfferId ?? 0))
                            _checkListForOffers.Add(dbOfferDetail.ItemOfferId ?? 0);
                    }
                }

                _context.SaveChanges();

                #region CHECK LISTS VALIDATION
                using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                    using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                        bObj.CheckOrderHeader(dbObj.Id);
                        _nContext.SaveChanges();
                    }
                }

                foreach (var item in _checkListForDemands)
                {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckDemandHeader(item);
                            _nContext.SaveChanges();
                        }
                    }
                }

                foreach (var item in _checkListForOfferDetails)
                {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckOfferDetail(item);
                            _nContext.SaveChanges();
                        }
                    }
                }

                foreach (var item in _checkListForOffers)
                {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckOfferHeader(item);
                            _nContext.SaveChanges();
                        }
                    }
                }
                #endregion

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
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                List<int> _checkListItemDemands = new List<int>();
                List<int> _checkListItemDemandDetails = new List<int>();
                List<int> _checkOfferDetails = new List<int>();
                List<int> _checkOfferHeaders = new List<int>();

                var dbObj = _context.ItemOrder.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen sipariş bilgisi bulunamadı.");

                var details = _context.ItemOrderDetail.Where(d => d.ItemOrderId == id).ToArray();
                foreach (var item in details)
                {
                    // check any order consume exists
                    if (_context.ItemOrderConsume.Any(d => d.ItemOrderDetailId == item.Id))
                        throw new Exception("Bu sipariş ile eşleşen irsaliyeler mevcut. Önce onları silmelisiniz.");

                    _context.ItemOrderDetail.Remove(item);

                    if (item.ItemOfferDetailId != null && !_checkOfferDetails.Contains(item.ItemOfferDetailId ?? 0)){
                        _checkOfferDetails.Add(item.ItemOfferDetailId ?? 0);

                        var dbOfferDetail = _context.ItemOfferDetail.FirstOrDefault(d => d.Id == (item.ItemOfferDetailId ?? 0));
                        if (dbOfferDetail != null){
                            if (!_checkOfferHeaders.Contains(dbOfferDetail.ItemOfferId ?? 0))
                                _checkOfferHeaders.Add(dbOfferDetail.ItemOfferId ?? 0);
                        }
                    }

                    // add demand details to check list 
                    var demandConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == item.Id).ToArray();
                    foreach (var dCons in demandConsumes)
                    {
                        if (dCons.ItemDemandDetailId != null){
                            var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == dCons.ItemDemandDetailId);
                            if (dbDemandDetail != null){
                                // dbDemandDetail.DemandStatus = 0; -- OLD
                                if (!_checkListItemDemands.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                        _checkListItemDemands.Add(dbDemandDetail.ItemDemandId ?? 0);

                                if (!_checkListItemDemandDetails.Contains(dbDemandDetail.Id))
                                    _checkListItemDemandDetails.Add(dbDemandDetail.Id);
                            }
                        }

                        _context.ItemDemandConsume.Remove(dCons);
                    }
                }

                _context.ItemOrder.Remove(dbObj);
                _context.SaveChanges();

                # region CHECK LIST APPLY
                using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                    using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                        foreach (var item in _checkOfferDetails)
                        {
                            bObj.CheckOfferDetail(item);
                        }

                        _nContext.SaveChanges();
                    }
                }   

                using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                    using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                        foreach (var item in _checkOfferHeaders)
                        {
                            bObj.CheckOfferHeader(item);
                        }

                        _nContext.SaveChanges();
                    }
                }   

                using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                    using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                        foreach (var item in _checkListItemDemandDetails)
                        {
                            bObj.CheckDemandDetail(item);
                        }

                        _nContext.SaveChanges();
                    }
                }   

                using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                    using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                        foreach (var item in _checkListItemDemands)
                        {
                            bObj.CheckDemandHeader(item);
                        }

                        _nContext.SaveChanges();
                    }
                }   
                #endregion
                
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