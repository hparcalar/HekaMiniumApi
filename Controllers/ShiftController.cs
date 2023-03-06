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
  public class ShiftController : HekaControllerBase
  {
    public ShiftController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    public IEnumerable<EmployeeCheckInModel> Get(string startDate, string endDate)
    {
      DateTime dtStart = DateTime.Now.Date;
      DateTime dtEnd = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));

      if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
      {
        dtStart = DateTime.ParseExact(startDate, "dd.MM.yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr"));
        dtEnd = DateTime.ParseExact(endDate, "dd.MM.yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr"));
        dtEnd = dtEnd.Add(new TimeSpan(23, 59, 59));
      }

      EmployeeCheckInModel[] data = new EmployeeCheckInModel[0];
      try
      {
        data = _context.EmployeeCheckIn.Where(d =>
            d.ProcessType == 0 &&
            d.ProcessDate >= dtStart && d.ProcessDate <= dtEnd
          )
          .Select(d => new EmployeeCheckInModel
          {
            Id = d.Id,
            EmployeeId = d.EmployeeId,
            ProcessDate = d.ProcessDate,
            ProcessType = d.ProcessType,
            EmployeeName = d.Employee.EmployeeName,
            ExitDate = _context.EmployeeCheckIn.Where(m => m.ProcessType == 1
              && m.EmployeeId == d.EmployeeId && m.ProcessDate > d.ProcessDate).Select(m => m.ProcessDate)
              .OrderBy(m => m)
              .FirstOrDefault(),
            /* TotalHour = (_context.EmployeeCheckIn.Where(m => m.ProcessType == 1
              && m.EmployeeId == d.EmployeeId && m.ProcessDate > d.ProcessDate).Select(m => m.ProcessDate)
              .OrderBy(m => m)
              .FirstOrDefault() - d.ProcessDate).Value.TotalMinutes, */
          }).ToArray();
        foreach(var item in data){
          if(item.ExitDate != null){
            item.TotalHour = (item.ExitDate.Value.TimeOfDay > new TimeSpan(17,45,00) && 
            item.ExitDate.Value.TimeOfDay < new TimeSpan(18,20,00)) &&
            (item.ProcessDate.Value.TimeOfDay > new TimeSpan(08,35,00) &&
            item.ProcessDate.Value.TimeOfDay < new TimeSpan(09,15,00)) ?
            (new TimeSpan(18,00,00) - new TimeSpan(09,00,00)).TotalMinutes :
            (item.ExitDate - item.ProcessDate).Value.TotalMinutes;
          }
        }
      }
      catch
      {

      }
      return data;
    }
  }
}