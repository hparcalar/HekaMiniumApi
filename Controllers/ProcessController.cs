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
    public class ProcessController : HekaControllerBase{
        public ProcessController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<ProcessModel> Get()
        {
            ProcessModel[] data = new ProcessModel[0];
            try
            {
                data = _context.Process
                .Select(d => new ProcessModel{
                    Id = d.Id,
                    EstimatedDuration = d.EstimatedDuration,
                    ForexId = d.ForexId,
                    IsActive = d.IsActive,
                    PlantId =d.PlantId,
                    ProcessCode = d.ProcessCode,
                    ProcessName = d.ProcessName,
                    ProcessOrder = d.ProcessOrder,
                    UnitPrice = d.UnitPrice,
                }
                ).OrderBy(d => d.ProcessOrder).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("{id}")]
        public ProcessModel Get(int id)
        {
            ProcessModel data = new ProcessModel();
            try
            {
                data = _context.Process.Where(d => d.Id == id).Select(d => new ProcessModel{
                        Id = d.Id,
                        EstimatedDuration = d.EstimatedDuration,
                        ForexId = d.ForexId,
                        IsActive = d.IsActive,
                        PlantId =d.PlantId,
                        ProcessCode = d.ProcessCode,
                        ProcessName = d.ProcessName,
                        ProcessOrder = d.ProcessOrder,
                        UnitPrice = d.UnitPrice,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(ProcessModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Process.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Process();
                    _context.Process.Add(dbObj);
                }

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
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Process.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Process.Remove(dbObj);

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