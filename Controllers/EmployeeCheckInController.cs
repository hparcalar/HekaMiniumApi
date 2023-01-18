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
using HekaMiniumApi.Models.Parameters;

namespace HekaMiniumApi.Controllers
{

  [Authorize]
  [ApiController]
  [Route("[controller]")]
  [EnableCors()]
  public class EmployeeCheckInController : HekaControllerBase
  {
    public EmployeeCheckInController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    public IEnumerable<EmployeeCheckInModel> Get()
    {
      EmployeeCheckInModel[] data = new EmployeeCheckInModel[0];
      try
      {
        data = _context.EmployeeCheckIn.Select(d => new EmployeeCheckInModel
        {
          Id = d.Id,
          EmployeeId = d.EmployeeId,
          ProcessDate = d.ProcessDate,
          ProcessType = d.ProcessType,
          EmployeeName = d.Employee.EmployeeName,
        }).ToArray();
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("{id}")]
    public EmployeeCheckInModel Get(int id)
    {
      EmployeeCheckInModel data = new EmployeeCheckInModel();
      try
      {
        data = _context.EmployeeCheckIn.Where(d => d.Id == id).Select(d => new EmployeeCheckInModel
        {
          Id = d.Id,
          EmployeeId = d.EmployeeId,
          ProcessDate = d.ProcessDate,
          ProcessType = d.ProcessType,
        }).FirstOrDefault();
      }
      catch
      {

      }

      return data;
    }

    [Authorize(Policy = "WebUser")]
    [HttpPost]
    [AllowAnonymous]
    public BusinessResult Post(EmployeeCheckInPrm model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbEmployee = _context.Employee.FirstOrDefault(d => d.EmployeeCardNo == model.CardNo);
        var dbLastCheckIn = _context.EmployeeCheckIn.Where(d => d.EmployeeId == dbEmployee.Id).OrderByDescending(d => d.ProcessDate).FirstOrDefault();

        var procType = 0;
        if (!(dbLastCheckIn == null || dbLastCheckIn.ProcessType == 1))
        {
          procType = 1;
        }
        
        var dbObj = new EmployeeCheckIn{
          EmployeeId = dbEmployee.Id,
          ProcessDate = model.ProcessDate,
          ProcessType = procType
        };
        _context.EmployeeCheckIn.Add(dbObj);

        _context.SaveChanges();
        result.Result = true;
        result.RecordId = dbObj.Id;
      }
      catch (System.Exception ex)
      {
        result.Result = false;
        result.ErrorMessage = ex.Message;
      }

      return result;
    }

    [Authorize(Policy = "WebUser")]
    [HttpDelete("{id}")]
    public BusinessResult Delete(int id)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.EmployeeCheckIn.FirstOrDefault(d => d.Id == id);
        if (dbObj == null)
          throw new Exception("");

        _context.EmployeeCheckIn.Remove(dbObj);

        _context.SaveChanges();
        result.Result = true;
      }
      catch (System.Exception ex)
      {
        result.Result = false;
        result.ErrorMessage = ex.Message;
      }

      return result;
    }
  }
}