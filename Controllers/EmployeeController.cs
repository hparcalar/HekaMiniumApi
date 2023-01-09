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

namespace HekaMiniumApi.Controllers
{

  [Authorize]
  [ApiController]
  [Route("[controller]")]
  [EnableCors()]
  public class EmployeeController : HekaControllerBase
  {
    public EmployeeController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    public IEnumerable<EmployeeModel> Get()
    {
      EmployeeModel[] data = new EmployeeModel[0];
      try
      {
        data = _context.Employee.Select(d => new EmployeeModel
        {
          Id = d.Id,
          EmployeeCode = d.EmployeeCode,
          EmployeeName = d.EmployeeName,
          EmployeeCardNo = d.EmployeeCardNo,
          EmployeeHourlyWage = d.EmployeeHourlyWage,
          EmployeePhone = d.EmployeePhone,
          EmployeeAddress = d.EmployeeAddress,
          DepartmentId = d.DepartmentId,
        }).ToArray();
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("{id}")]
    public EmployeeModel Get(int id)
    {
      EmployeeModel data = new EmployeeModel();
      try
      {
        data = _context.Employee.Where(d => d.Id == id).Select(d => new EmployeeModel
        {
          Id = d.Id,
          EmployeeCode = d.EmployeeCode,
          EmployeeName = d.EmployeeName,
          EmployeeCardNo = d.EmployeeCardNo,
          EmployeeHourlyWage = d.EmployeeHourlyWage,
          EmployeePhone = d.EmployeePhone,
          EmployeeAddress = d.EmployeeAddress,
          DepartmentId = d.DepartmentId,
        }).FirstOrDefault();

        if (data == null)
        {
          data = new EmployeeModel();
          data.EmployeeCode = Convert.ToInt32(GetNextNumber());
        }
      }
      catch
      {

      }

      return data;
    }

    [Authorize(Policy = "WebUser")]
    [HttpPost]
    public BusinessResult Post(EmployeeModel model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.Employee.FirstOrDefault(d => d.Id == model.Id);
        if (dbObj == null)
        {
          dbObj = new Employee();
          _context.Employee.Add(dbObj);
        }

        if (_context.Employee.Any(d => d.EmployeeCode == model.EmployeeCode && d.Id != model.Id))
          throw new Exception("Bu personel koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

        model.MapTo(dbObj);

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
        var dbObj = _context.Employee.FirstOrDefault(d => d.Id == id);
        if (dbObj == null)
          throw new Exception("");

        _context.Employee.Remove(dbObj);

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
    private string GetNextNumber()
    {
      try
      {
        int nextNumber = 1;
        var lastRecord = _context.Employee.OrderByDescending(d => d.EmployeeCode).Select(d => d.EmployeeCode).FirstOrDefault();
        if (lastRecord != null && !string.IsNullOrEmpty(lastRecord.ToString()))
          nextNumber = Convert.ToInt32(lastRecord) + 1;

        return string.Format("{0:0}", nextNumber);
      }
      catch (System.Exception)
      {

      }

      return string.Empty;
    }
  }
}