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
using HekaMiniumApi.Models.Reporting;
using Microsoft.AspNetCore.Cors;
using HekaMiniumApi.Helpers;
using HekaMiniumApi.Business;
using HekaMiniumApi.Models.Constants;

namespace HekaMiniumApi.Controllers{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class WarehouseController : HekaControllerBase{
        public WarehouseController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<WarehouseModel> Get()
        {
            WarehouseModel[] data = new WarehouseModel[0];
            try
            {
                data = _context.Warehouse.Select(d => new WarehouseModel{
                    Id = d.Id,
                    AllowDelivery = d.AllowDelivery,
                    AllowEntry = d.AllowEntry,
                    IsActive = d.IsActive,
                    PlantId = d.PlantId,
                    WarehouseCode = d.WarehouseCode,
                    WarehouseName = d.WarehouseName,
                    WarehouseType = d.WarehouseType,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public WarehouseModel Get(int id)
        {
            WarehouseModel data = new WarehouseModel();
            try
            {
                data = _context.Warehouse.Where(d => d.Id == id).Select(d => new WarehouseModel{
                        Id = d.Id,
                        AllowDelivery = d.AllowDelivery,
                        AllowEntry = d.AllowEntry,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        WarehouseCode = d.WarehouseCode,
                        WarehouseName = d.WarehouseName,
                        WarehouseType = d.WarehouseType,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(WarehouseModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Warehouse.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Warehouse();
                    _context.Warehouse.Add(dbObj);
                }

                if (_context.Warehouse.Any(d => d.WarehouseCode == model.WarehouseCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu depo koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                model.MapTo(dbObj);

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

        [Authorize(Policy = "WebUser")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Warehouse.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Warehouse.Remove(dbObj);

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

        [Authorize(Policy = "WebUser")]
        [HttpGet]
        [Route("ItemStocks")]
        public ItemStocksModel[] GetItemStocks(){
            ItemStocksModel[] data = new ItemStocksModel[0];

            try
            {
                data = _context.ItemReceiptDetail.Where(d => d.ItemId != null && d.ItemReceipt.InWarehouseId != null)
                    .GroupBy(d => new ItemStocksModel {
                        Id = (d.ItemId ?? 0) * (d.ItemReceipt.InWarehouseId ?? 0),
                        ItemId = d.ItemId ?? 0,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        WarehouseCode = d.ItemReceipt.InWarehouse.WarehouseCode,
                        WarehouseName = d.ItemReceipt.InWarehouse.WarehouseName,
                        WarehouseId = d.ItemReceipt.InWarehouseId,
                    }).Select(d => new ItemStocksModel{
                        Id = d.Key.Id,
                        ItemId = d.Key.ItemId,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        WarehouseId = d.Key.WarehouseId,
                        WarehouseCode = d.Key.WarehouseCode,
                        WarehouseName = d.Key.WarehouseName,
                        InQuantity = d.Where(m => m.ItemReceipt.ReceiptType < 100).Sum(m => m.Quantity) ?? 0,
                        OutQuantity = d.Where(m => m.ItemReceipt.ReceiptType > 100).Sum(m => m.Quantity) ?? 0,
                    }).ToArray();

                foreach (var item in data)
                {
                    item.TotalQuantity = item.InQuantity - item.OutQuantity;
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        [Route("Stocktaking")]
        public BusinessResult Stocktaking(ItemReceiptModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                int? creatorId = HekaHelpers.GetUserId(Request.HttpContext);
                if (model.Id <= 0)
                    model.SysUserId = creatorId;
                
                var dbObj = _context.Stocktaking.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Stocktaking();
                    dbObj.StocktakingDate = model.ReceiptDate;
                    dbObj.StocktakingType = model.ReceiptType;
                    dbObj.StocktakingNo = GetNextReceiptNumber(model.ReceiptType);
                    _context.Stocktaking.Add(dbObj);
                }
                model.MapTo(dbObj);
                _context.SaveChanges();
                foreach (var item in model.Details)
                    {
                        var dbItem = _context.StocktakingDetail.FirstOrDefault(d => d.Id == item.Id);
                        if (dbItem == null){
                            dbItem = new StocktakingDetail();
                            _context.StocktakingDetail.Add(dbItem);
                        }
                        item.MapTo(dbItem);
                        dbItem.StocktakingId = dbObj.Id;
                    }
                if(model.inItems != null){
                    model.Details = model.inItems;
                    model.ReceiptType = 4;
                    using (ReceiptManagementBO bObj = new ReceiptManagementBO(_context)){
                        result = bObj.SaveItemReceipt(model);
                    }   
                }
                if(model.outItems != null){
                    model.Details = model.outItems;
                    model.ReceiptType = 108;
                    using (ReceiptManagementBO bObj = new ReceiptManagementBO(SchemaFactory.CreateContext())){
                        result = bObj.SaveItemReceipt(model);
                    } 
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

        private string GetNextReceiptNumber(int receiptType){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.Stocktaking.Where(d => d.StocktakingType == receiptType)
                    .OrderByDescending(d => d.StocktakingNo).Select(d => d.StocktakingNo).FirstOrDefault();
                if (lastRecord != null && !string.IsNullOrEmpty(lastRecord))
                    nextNumber = Convert.ToInt32(lastRecord) + 1;

                return string.Format("{0:000000}", nextNumber);
            }
            catch (System.Exception)
            {
                
            }

            return string.Empty;
        }

        [HttpGet]
        [Route("Stocktaking")]
        public IEnumerable<StocktakingModel> GetStocktaking()
        {
            StocktakingModel[] data = new StocktakingModel[0];
            try
            {
                data = _context.Stocktaking.Select(d => new StocktakingModel{
                    Id = d.Id,
                    StocktakingNo = d.StocktakingNo,
                    StocktakingType = d.StocktakingType,
                    StocktakingDate = d.StocktakingDate,
                    Explanation = d.Explanation,
                    PlantId = d.PlantId,
                    InWarehouseId = d.InWarehouseId,
                    OutWarehouseId = d.OutWarehouseId,
                    StocktakingStatus = d.StocktakingStatus,
                    SysUserId = d.SysUserId,
                    WarehouseCode = d.InWarehouse != null ? d.InWarehouse.WarehouseCode : "",
                    WarehouseName = d.InWarehouse != null ? d.InWarehouse.WarehouseName : "",
                    StocktakingTypeText = ConstReceiptType.GetDesc(d.StocktakingType),
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Stocktaking/{id}")]
        public StocktakingModel GetStocktaking(int id)
        {
            StocktakingModel data = new StocktakingModel();
            try
            {
                data = _context.Stocktaking.Where(d => d.Id == id).Select(d => new StocktakingModel{
                    Id = d.Id,
                    StocktakingNo = d.StocktakingNo,
                    //StocktakingType = d.StocktakingType,
                    StocktakingDate = d.StocktakingDate,
                    Explanation = d.Explanation,
                    PlantId = d.PlantId,
                    InWarehouseId = d.InWarehouseId,
                    OutWarehouseId = d.OutWarehouseId,
                    StocktakingStatus = d.StocktakingStatus,
                    SysUserId = d.SysUserId,
                    WarehouseCode = d.InWarehouse != null ? d.InWarehouse.WarehouseCode : "",
                    WarehouseName = d.InWarehouse != null ? d.InWarehouse.WarehouseName : "",
                    //StocktakingTypeText = ConstReceiptType.GetDesc(d.StocktakingType),
                    }).FirstOrDefault();
                
                if(data != null){
                    data.Details = _context.StocktakingDetail.Where(d => d.StocktakingId == id).Select(d => new StocktakingDetailModel{
                        Id = d.Id,
                        StocktakingId = d.StocktakingId,
                        LineNumber = d.LineNumber,
                        ItemId = d.ItemId,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        Explanation = d.Explanation,
                    }).ToArray();
                }
            }
            catch
            {
                
            }
            
            return data;
        }
    }
}