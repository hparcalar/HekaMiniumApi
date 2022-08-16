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

     }
}