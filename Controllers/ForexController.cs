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
     public class ForexController : HekaControllerBase{
        public ForexController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<ForexModel> Get()
        {
            ForexModel[] data = new ForexModel[0];
            try
            {
                data = _context.Forex.Select(d => new ForexModel{
                    Id = d.Id,
                    ForexCode = d.ForexCode,
                    ForexName = d.ForexName,
                    IsActive = d.IsActive,
                    LiveRate = d.LiveRate,
                    PlantId = d.PlantId,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ForexModel Get(int id)
        {
            ForexModel data = new ForexModel();
            try
            {
                data = _context.Forex.Where(d => d.Id == id).Select(d => new ForexModel{
                        Id = d.Id,
                        ForexCode = d.ForexCode,
                        ForexName = d.ForexName,
                        IsActive = d.IsActive,
                        LiveRate = d.LiveRate,
                        PlantId = d.PlantId,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(ForexModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Forex.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Forex();
                    _context.Forex.Add(dbObj);
                }

                if (_context.Forex.Any(d => d.ForexCode == model.ForexCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu döviz cinsine ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

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
                var dbObj = _context.Forex.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Forex.Remove(dbObj);

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