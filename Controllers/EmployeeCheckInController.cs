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
    public IEnumerable<EmployeeCheckInModel> Get(string startDate, string endDate)
    {
      DateTime dtStart = DateTime.Now.Date;
      DateTime dtEnd = DateTime.Now.Date.Add(new TimeSpan(23,59,59));

      if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate)){
        dtStart = DateTime.ParseExact(startDate, "dd.MM.yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr"));
        dtEnd = DateTime.ParseExact(endDate, "dd.MM.yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr"));
        dtEnd = dtEnd.Add(new TimeSpan(23,59,59));
      }

      EmployeeCheckInModel[] data = new EmployeeCheckInModel[0];
      try
      {

        // groupby - select - where
        data = _context.EmployeeCheckIn.Where(d => 
            d.ProcessType == 0 &&
            d.ProcessDate >= dtStart && d.ProcessDate <= dtEnd
          )
          .Select(d => new EmployeeCheckInModel{
            Id = d.Id,
            EmployeeId = d.EmployeeId,
            ProcessDate = d.ProcessDate,
            ProcessType = d.ProcessType,
            EmployeeName = d.Employee.EmployeeName,
            ExitDate = _context.EmployeeCheckIn.Where(m => m.ProcessType == 1 
              && m.EmployeeId == d.EmployeeId && m.ProcessDate > d.ProcessDate).Select(m => m.ProcessDate)
              .OrderBy(m => m)
              .FirstOrDefault(),
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

        /* var procType = 0;
        if (!(dbLastCheckIn == null || dbLastCheckIn.ProcessType == 1))
        {
          procType = 1;
        }
        
        var dbObj = new EmployeeCheckIn{
          EmployeeId = dbEmployee.Id,
          ProcessDate = model.ProcessDate,
          ProcessType = procType
        };
        _context.EmployeeCheckIn.Add(dbObj); */
        var MissFlag = false;
        var time = (model.ProcessDate - dbLastCheckIn.ProcessDate).Value.TotalHours;
        var procType = 0;
        if (dbLastCheckIn != null && time > 18 && dbLastCheckIn.ProcessType == 0)
        {
          if (!(dbLastCheckIn == null || dbLastCheckIn.ProcessType == 1))
          {
            procType = 1;
          }

          var dbMiss = new EmployeeCheckIn
          {
            EmployeeId = dbEmployee.Id,
            ProcessDate = dbLastCheckIn.ProcessDate.Value.Date.AddHours(18),
            ProcessType = procType
          };
          MissFlag = true;
          _context.EmployeeCheckIn.Add(dbMiss);
        }

        if (!(dbLastCheckIn == null || dbLastCheckIn.ProcessType == 1))
        {
          procType = 1;
        }

        if (MissFlag)
        {
          procType = 0;
        }

        var dbObj = new EmployeeCheckIn
        {
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