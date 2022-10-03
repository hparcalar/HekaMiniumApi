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
    public class StaffPermitController : HekaControllerBase{
        public StaffPermitController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<StaffPermitModel> Get()
        {
            StaffPermitModel[] data = new StaffPermitModel[0];
            try
            {
                data = _context.StaffPermit.Select(d => new StaffPermitModel{
                    Id = d.Id,
                    StaffId = d.StaffId,
                    StaffPermitExplanation = d.StaffPermitExplanation,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    PermitStatus = d.PermitStatus,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public StaffPermitModel Get(int id)
        {
            StaffPermitModel data = new StaffPermitModel();
            try
            {
                data = _context.StaffPermit.Where(d => d.Id == id).Select(d => new StaffPermitModel{
                    Id = d.Id,
                    StaffId = d.StaffId,
                    StaffPermitExplanation = d.StaffPermitExplanation,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    PermitStatus = d.PermitStatus,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(StaffPermitModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.StaffPermit.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new StaffPermit();
                    _context.StaffPermit.Add(dbObj);
                }

                if (_context.StaffPermit.Any(d => d.StaffId == model.StaffId && d.StaffPermitExplanation == model.StaffPermitExplanation && d.Id != model.Id))
                    throw new Exception("Bu personel için aynı açıklamayla izin isteği zaten mevcut. Farklı bir açıklama giriniz.");

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
                var dbObj = _context.StaffPermit.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.StaffPermit.Remove(dbObj);

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