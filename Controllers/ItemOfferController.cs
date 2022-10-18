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
     public class ItemOfferController : HekaControllerBase{
        public ItemOfferController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        [Route("List/{offerType}")]
        public IEnumerable<ItemOfferModel> Get(int offerType)
        {
            ItemOfferModel[] data = new ItemOfferModel[0];
            try
            {
                data = _context.ItemOffer.Where(d => d.OfferType == offerType).Select(d => new ItemOfferModel{
                    Id = d.Id,
                    OfferStatus = d.OfferStatus,
                    OfferType = d.OfferType,
                    Explanation = d.Explanation,
                    PlantId = d.PlantId,
                    ReceiptDate = d.ReceiptDate,
                    ReceiptNo = d.ReceiptNo,
                    SysUserId = d.SysUserId,
                    UserCode = d.SysUser != null ? d.SysUser.UserCode : "",
                    UserName = d.SysUser != null ? d.SysUser.UserName : "",
                    FirmId = d.FirmId,
                    // FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                    // FirmName = d.Firm != null ? d.Firm.FirmName : "",
                    FirmName = String.Join(", ",d.ItemOfferFirmOptions.Select(m => m.Firm.FirmName).ToArray()),
                    StatusText = d.OfferStatus == 0 ? "Teklif oluşturuldu" : 
                                    d.OfferStatus == 3 ? "Sipariş verildi" : "",
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
        [Route("{id}")]
        public ItemOfferModel GetById(int id)
        {
            ItemOfferModel data = new ItemOfferModel();
            try
            {
                data = _context.ItemOffer.Where(d => d.Id == id).Select(d => new ItemOfferModel{
                        Id = d.Id,
                        OfferStatus = d.OfferStatus,
                        OfferType = d.OfferType,
                        Explanation = d.Explanation,
                        PlantId = d.PlantId,
                        ReceiptDate = d.ReceiptDate,
                        ReceiptNo = d.ReceiptNo,
                        SysUserId = d.SysUserId,
                        UserCode = d.SysUser != null ? d.SysUser.UserCode : "",
                        UserName = d.SysUser != null ? d.SysUser.UserName : "",
                        FirmId = d.FirmId,
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        StatusText = d.OfferStatus == 0 ? "Teklif oluşturuldu" : 
                                        d.OfferStatus == 3 ? "Sipariş verildi" : "",
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.Details = _context.ItemOfferDetail.Where(d => d.ItemOfferId == data.Id)
                        .Select(d => new ItemOfferDetailModel{
                            Id = d.Id,
                            OfferStatus = d.OfferStatus,
                            Explanation = d.Explanation,
                            ItemOfferId = d.ItemOfferId,
                            AcceptedFirmId = d.AcceptedFirmId,
                            ItemId = d.ItemId,
                            LineNumber = d.LineNumber,
                            NetQuantity = d.NetQuantity,
                            Quantity = d.Quantity,
                            ForexId = d.ForexId,
                            ForexRate = d.ForexRate,
                            UnitPrice = d.UnitPrice,
                            SubTotal = d.SubTotal,
                            SubForexTotal = d.SubForexTotal,
                            TaxIncluded = d.TaxIncluded,
                            TaxRate = d.TaxRate,
                            OverallTotal = d.OverallTotal,
                            OverallForexTotal = d.OverallForexTotal,
                            ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                            ForexName = d.Forex != null ? d.Forex.ForexName : "",
                            UnitId = d.UnitId,
                            PartDimensions = d.PartDimensions,
                            PartNo = d.PartNo,
                            ItemExplanation = d.ItemExplanation,
                            ItemCode = d.Item != null ? d.Item.ItemCode : "",
                            ItemName = d.Item != null ? d.Item.ItemName : "",
                            UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                            UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                            StatusText = d.OfferStatus == 0 ? "Teklif oluşturuldu" : 
                                        d.OfferStatus == 3 ? "Sipariş verildi" : "",
                        }).ToArray();
                
                    // fetch demand details
                    foreach (var item in data.Details)
                    {
                        item.Demands = _context.ItemOfferDetailDemand.Where(d => d.ItemOfferDetailId == item.Id)
                            .Select(d => new ItemOfferDetailDemandModel{
                                Id = d.Id,
                                ItemDemandDetailId = d.ItemDemandDetailId,
                                ItemOfferDetailId = d.ItemOfferDetailId,
                                Quantity = d.Quantity,
                                DemandOrder = d.DemandOrder,
                                DemandQuantity = d.ItemDemandDetail != null ? d.ItemDemandDetail.Quantity : null,
                                ItemExplanation = d.ItemDemandDetail != null ? d.ItemDemandDetail.ItemExplanation : "",
                                ItemName = d.ItemDemandDetail != null && d.ItemDemandDetail.Item != null ? d.ItemDemandDetail.Item.ItemName : "",
                                ProjectName = d.ItemDemandDetail != null && d.ItemDemandDetail.ItemDemand.Project != null ? 
                                    d.ItemDemandDetail.ItemDemand.Project.ProjectName : "",
                                PartDimensions = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartDimensions : "",
                                PartNo = d.ItemDemandDetail != null ? d.ItemDemandDetail.PartNo : "",
                            }).ToArray();
                    }

                    // fetch related orders
                    foreach (var item in data.Details)
                    {
                        item.OrderDetail = _context.ItemOrderDetail.Where(d => d.ItemOfferDetailId == item.Id)
                            .Select(d => new ItemOrderDetailModel{
                                Id = d.Id,
                                ItemOrderId = d.ItemOrderId,
                                ReceiptNo = d.ItemOrder.ReceiptNo,
                            }).FirstOrDefault();
                    }

                    // fetch firm options
                    data.FirmOptions = _context.ItemOfferFirmOption.Where(d => d.ItemOfferId == data.Id)
                        .Select(d => new ItemOfferFirmOptionModel{
                            Id = d.Id,
                            FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                            FirmId = d.FirmId,
                            FirmName = d.Firm != null ? d.Firm.FirmName : "",
                            FirmOrder = d.FirmOrder,
                            ItemOfferId = d.ItemOfferId,
                        }).ToArray();

                    // fetch firm prices
                    foreach (var item in data.Details)
                    {
                        item.FirmPrices = _context.ItemOfferFirmPrice.Where(d => d.ItemOfferId == data.Id && d.ItemOfferDetailId == item.Id)
                            .Select(d => new ItemOfferFirmPriceModel{
                                Id = d.Id,
                                FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                                FirmId = d.FirmId,
                                FirmName = d.Firm != null ? d.Firm.FirmName : "",
                                ItemOfferDetailId = d.ItemOfferDetailId,
                                ItemOfferId = d.ItemOfferId,
                                ForexId = d.ForexId,
                                ForexRate = d.ForexRate,
                                UnitPrice = d.UnitPrice,
                                SubTotal = d.SubTotal,
                                SubForexTotal = d.SubForexTotal,
                                TaxIncluded = d.TaxIncluded,
                                TaxRate = d.TaxRate,
                                OverallTotal = d.OverallTotal,
                                OverallForexTotal = d.OverallForexTotal,
                                ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                                ForexName = d.Forex != null ? d.Forex.ForexName : "",
                            }).ToArray();
                    }
                }
                else{
                    if (data == null)
                        data = new ItemOfferModel();

                    data.ReceiptNo = GetNextOfferNumber();
                    data.Details = new ItemOfferDetailModel[0];
                    data.FirmOptions = new ItemOfferFirmOptionModel[0];
                    data.FirmPrices = new ItemOfferFirmPriceModel[0];
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        private string GetNextOfferNumber(){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.ItemOffer.OrderByDescending(d => d.ReceiptNo).Select(d => d.ReceiptNo).FirstOrDefault();
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
        public BusinessResult Post(ItemOfferModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                // check list
                List<int> _checkListForDemandDetails = new List<int>();
                List<int> _checkListForDemands = new List<int>();

                if (!_context.Plant.Any(d => d.Id == (model.PlantId ?? 0)))
                    model.PlantId = null;

                var dbObj = _context.ItemOffer.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    model.ReceiptNo = GetNextOfferNumber();

                    dbObj = new ItemOffer();
                    dbObj.ReceiptNo = model.ReceiptNo;
                    _context.ItemOffer.Add(dbObj);
                }

                // keep constants
                var currentRcNo = dbObj.ReceiptNo;
                model.MapTo(dbObj);

                // replace constants after auto mapping
                dbObj.ReceiptNo = currentRcNo;

                #region SAVE DETAILS
                var currentDetails = _context.ItemOfferDetail.Where(d => d.ItemOfferId == dbObj.Id).ToArray();

                var removedDetails = currentDetails.Where(d => !model.Details.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedDetails)
                {
                    var _existingConsumes = _context.ItemOfferDetailDemand.Where(d => d.ItemOfferDetailId == item.Id).ToArray();
                    foreach (var consItem in _existingConsumes)
                    {
                        var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == consItem.ItemDemandDetailId);
                        if (dbDemandDetail != null){
                            dbDemandDetail.DemandStatus = 0;

                            if (!_checkListForDemandDetails.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                _checkListForDemandDetails.Add(dbDemandDetail.ItemDemandId ?? 0);
                        }

                        _context.ItemOfferDetailDemand.Remove(consItem);
                    }

                    _context.ItemOfferDetail.Remove(item);
                }

                foreach (var item in model.Details)
                {
                    var dbDetail = _context.ItemOfferDetail.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new ItemOfferDetail();
                        _context.ItemOfferDetail.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.ItemOffer = dbObj;

                    #region SAVE FIRM PRICES
                    var currentFirmPrices = _context.ItemOfferFirmPrice.Where(d => d.ItemOfferId == dbObj.Id && d.ItemOfferDetailId == dbDetail.Id).ToArray();

                    var removedFirmPrices = currentFirmPrices.Where(d => !item.FirmPrices.Any(m => m.Id == d.Id)).ToArray();
                    foreach (var priceItem in removedFirmPrices)
                    {
                        _context.ItemOfferFirmPrice.Remove(priceItem);
                    }

                    foreach (var priceItem in item.FirmPrices)
                    {
                        var dbFirmPrice = _context.ItemOfferFirmPrice.FirstOrDefault(d => d.Id == priceItem.Id);
                        if (dbFirmPrice == null){
                            dbFirmPrice = new ItemOfferFirmPrice();
                            _context.ItemOfferFirmPrice.Add(dbFirmPrice);
                        }

                        priceItem.MapTo(dbFirmPrice);
                        dbFirmPrice.ItemOffer = dbObj;
                        dbFirmPrice.ItemOfferDetail = dbDetail;
                    }
                    #endregion

                    // check & update demand detail status -- NEW --
                    if (item.Demands != null && item.Demands.Length > 0){
                        foreach (var consItem in item.Demands)
                        {
                            var _currentConsume = _context.ItemOfferDetailDemand.FirstOrDefault(d => d.ItemDemandDetailId == consItem.ItemDemandDetailId
                                && d.ItemOfferDetailId == item.Id);
                            if (_currentConsume == null){
                                _currentConsume = new ItemOfferDetailDemand();
                                _context.ItemOfferDetailDemand.Add(_currentConsume);
                            }

                            consItem.MapTo(_currentConsume);
                            _currentConsume.ItemOfferDetail = dbDetail;

                            var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == consItem.ItemDemandDetailId);
                            if (dbDemandDetail != null) {
                                dbDemandDetail.DemandStatus = 5; // offered status

                                if (!_checkListForDemandDetails.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                    _checkListForDemandDetails.Add(dbDemandDetail.ItemDemandId ?? 0);
                            }
                        }
                    }
                    else{
                        var _existingConsumes = _context.ItemOfferDetailDemand.Where(d => d.ItemOfferDetailId == item.Id).ToArray();
                        foreach (var consItem in _existingConsumes)
                        {
                            var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == consItem.ItemDemandDetailId);
                            if (dbDemandDetail != null){
                                dbDemandDetail.DemandStatus = 0;

                                if (!_checkListForDemandDetails.Contains(dbDemandDetail.ItemDemandId ?? 0))
                                    _checkListForDemandDetails.Add(dbDemandDetail.ItemDemandId ?? 0);
                            }

                            _context.ItemOfferDetailDemand.Remove(consItem);
                        }
                    }
                }
                #endregion

                #region SAVE FIRM OPTIONS
                 var currentFirmOptions = _context.ItemOfferFirmOption.Where(d => d.ItemOfferId == dbObj.Id).ToArray();

                var removedFirmOptions = currentFirmOptions.Where(d => !model.FirmOptions.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedFirmOptions)
                {
                    _context.ItemOfferFirmOption.Remove(item);
                }

                foreach (var item in model.FirmOptions)
                {
                    var dbFirmOption = _context.ItemOfferFirmOption.FirstOrDefault(d => d.Id == item.Id);
                    if (dbFirmOption == null){
                        dbFirmOption = new ItemOfferFirmOption();
                        _context.ItemOfferFirmOption.Add(dbFirmOption);
                    }

                    item.MapTo(dbFirmOption);
                    dbFirmOption.ItemOffer = dbObj;
                }
                #endregion

                foreach (var item in _checkListForDemandDetails)
                {
                    var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == item);
                    if (dbDemandDetail != null){
                        if (dbDemandDetail.ItemDemandId != null && !_checkListForDemands.Contains(dbDemandDetail.ItemDemandId ?? 0))
                            _checkListForDemands.Add(dbDemandDetail.ItemDemandId ?? 0);
                    }
                }

                _context.SaveChanges();

                #region CHECK LISTS VALIDATION
                foreach (var item in _checkListForDemandDetails)
                {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckDemandDetail(item);
                            _nContext.SaveChanges();
                        }
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
                // check lists
                List<int> _checkListItemDemandDetails = new List<int>();
                List<int> _checkListItemDemands = new List<int>();

                var dbObj = _context.ItemOffer.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen teklif bilgisi bulunamadı.");

                // delete firm options
                var firmOptions = _context.ItemOfferFirmOption.Where(d => d.ItemOfferId == id).ToArray();
                foreach (var item in firmOptions)
                {
                    _context.ItemOfferFirmOption.Remove(item);
                }

                // delete firm prices
                var firmPrices = _context.ItemOfferFirmPrice.Where(d => d.ItemOfferId == id).ToArray();
                foreach (var item in firmPrices)
                {
                    _context.ItemOfferFirmPrice.Remove(item);
                }

                var details = _context.ItemOfferDetail.Where(d => d.ItemOfferId == id).ToArray();
                foreach (var item in details)
                {
                    // check any orders  exists
                    if (_context.ItemOrderDetail.Any(d => d.ItemOfferDetailId == item.Id))
                        throw new Exception("Bu teklif ile eşleşen siparişler mevcut. Önce onları silmelisiniz.");

                    _context.ItemOfferDetail.Remove(item);

                    // add demand details to check list 
                    var demandConsumes = _context.ItemOfferDetailDemand.Where(d => d.ItemOfferDetailId == item.Id).ToArray();
                    foreach (var dCons in demandConsumes)
                    {
                        if (dCons.ItemDemandDetailId != null && !_checkListItemDemandDetails.Contains(dCons.ItemDemandDetailId ?? 0)){
                            _checkListItemDemandDetails.Add(dCons.ItemDemandDetailId ?? 0);
                        }

                        _context.ItemOfferDetailDemand.Remove(dCons);
                    }
                }

                
                // fill demand check list
                foreach (var item in _checkListItemDemandDetails)
                {
                    var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == item);
                    if (dbDemandDetail != null){
                        if (dbDemandDetail.ItemDemandId != null && !_checkListItemDemands.Contains(dbDemandDetail.ItemDemandId ?? 0))
                            _checkListItemDemands.Add(dbDemandDetail.ItemDemandId ?? 0);
                    }
                }

                _context.ItemOffer.Remove(dbObj);
                _context.SaveChanges();

                #region CHECK LISTS VALIDATION
                foreach (var item in _checkListItemDemandDetails)
                {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckDemandDetail(item);
                            _nContext.SaveChanges();
                        }
                    }
                }

                foreach (var item in _checkListItemDemands)
                {
                    using (HekaMiniumSchema _nContext = SchemaFactory.CreateContext()){
                        using (OrderManagementBO bObj = new OrderManagementBO(_nContext)){
                            bObj.CheckDemandHeader(item);
                            _nContext.SaveChanges();
                        }
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