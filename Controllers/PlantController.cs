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
    public class PlantController : HekaControllerBase{
        public PlantController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<PlantModel> Get()
        {
            PlantModel[] data = new PlantModel[0];
            try
            {
                data = _context.Plant.Select(d => new PlantModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    PlantCode = d.PlantCode,
                    PlantName = d.PlantName,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public PlantModel Get(int id)
        {
            PlantModel data = new PlantModel();
            try
            {
                data = _context.Plant.Where(d => d.Id == id).Select(d => new PlantModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        PlantCode = d.PlantCode,
                        PlantName = d.PlantName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(PlantModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Plant.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Plant();
                    _context.Plant.Add(dbObj);
                }

                if (_context.Plant.Any(d => d.PlantCode == model.PlantCode && d.Id != model.Id))
                    throw new Exception("Plant could not be found.");

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
                var dbObj = _context.Plant.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Plant.Remove(dbObj);

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