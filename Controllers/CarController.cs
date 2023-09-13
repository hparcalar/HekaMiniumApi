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
  public class CarController : HekaControllerBase
  {
    public CarController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    public IEnumerable<CarModel> Get()
    {
      CarModel[] data = new CarModel[0];
      try
      {
        data = _context.Car.Select(d => new CarModel
        {
          Id = d.Id,
          PlateNo = d.PlateNo,
          Model = d.Model,
          ModelYear = d.ModelYear,

          InsuranceDate = d.CarDetails.Where(m => m.DetailCode == 9).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          CurrentMileage = d.CarDetails.Where(m => m.DetailCode == 1).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          InspectionDate = d.CarDetails.Where(m => m.DetailCode == 3).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          MaintenanceMileage = d.CarDetails.Where(m => m.DetailCode == 2).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          MonthlyFuelCost = d.CarDetails.Where(m => m.DetailCode == 4 && m.DetailDate.Value.Month == DateTime.Now.Month).Select(x=> x.Value).ToArray(),
          MonthlyMileage = d.CarDetails.Where(m => m.DetailCode == 1 && m.DetailDate.Value.Month == DateTime.Now.Month).Select(x => x.Value).ToArray()
        }).ToArray();
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("{id}")]
    public CarModel Get(int id)
    {
      CarModel data = new CarModel();
      try
      {
        data = _context.Car.Where(d => d.Id == id).Select(d => new CarModel
        {
          Id = d.Id,
          PlateNo = d.PlateNo,
          Model = d.Model,
          ModelYear = d.ModelYear,

          InspectionDate = d.CarDetails.Where(m => m.DetailCode == 3).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          InsuranceDate = d.CarDetails.Where(m => m.DetailCode == 9).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          CurrentMileage = d.CarDetails.Where(m => m.DetailCode == 1).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
          MaintenanceMileage = d.CarDetails.Where(m => m.DetailCode == 2).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault(),
        }).FirstOrDefault();

        if(data != null && data.Id > 0){
          data.Details = _context.CarDetail.Where(d => d.CarId == data.Id && d.DetailCode > 3 && d.DetailCode < 9)
            .Select(d => new CarDetailModel{
              Id = d.Id,
              CarId = d.CarId,
              DetailCode = d.DetailCode,
              DetailDate = d.DetailDate,
              CostExplanation = d.CostExplanation,
              DetailCodeText = (d.DetailCode) == 4 ? "Yakıt Maliyeti" :
                                d.DetailCode == 5 ? "Bakım Maliyeti" :
                                d.DetailCode == 6 ? "Vergi Maliyeti" :
                                d.DetailCode == 7 ? "Ceza Maliyeti" :
                                d.DetailCode == 8 ? "Diğer Giderler" : "",
              Value = d.Value,
            }).OrderByDescending(x => x.DetailDate).ToArray();
        }
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("GetAll/{start}/{end}")]
    public IEnumerable<CarDetailModel> AllDetails(DateTime start, DateTime end)
    {
      CarDetailModel[] data = new CarDetailModel[0];
      try
      {
        data = _context.CarDetail.Where(x => x.DetailDate > start && x.DetailDate < end).Select(d => new CarDetailModel
        {
          Id = d.Id,
          CarId = d.CarId,
          DetailCode = d.DetailCode,
          DetailDate = d.DetailDate,
          CostExplanation = d.CostExplanation,
          DetailCodeText = (d.DetailCode) == 1 ? "Güncel Kilometre" :
                            d.DetailCode == 2 ? "Bakım Kilometresi" :
                            d.DetailCode == 3 ? "Muayene Tarihi" :
                            d.DetailCode == 4 ? "Yakıt Maliyeti" :
                            d.DetailCode == 5 ? "Bakım Maliyeti" :
                            d.DetailCode == 6 ? "Vergi Maliyeti" :
                            d.DetailCode == 7 ? "Ceza Maliyeti" :
                            d.DetailCode == 8 ? "Diğer Giderler" :
                            d.DetailCode == 9 ? "Sigorta Tarihi" : "",
          CarName = d.Car.Model,
          Value = d.Value
        }).OrderByDescending(x => x.DetailDate).ToArray();
      }
      catch
      {

      }
      return data;
    }

    [Authorize(Policy = "WebUser")]
    [HttpPost]
    public BusinessResult Post(CarModel model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.Car.FirstOrDefault(d => d.Id == model.Id);
        var flag = true;
        if (dbObj == null)
        {
          dbObj = new Car();
          _context.Car.Add(dbObj);
          flag = false;
        }

        model.MapTo(dbObj);
        _context.SaveChanges();

        if(flag == false){
          var dbcMileage = new CarDetail();
          _context.CarDetail.Add(dbcMileage);
          
          dbcMileage.CarId = dbObj.Id;
          dbcMileage.DetailCode = 1;
          dbcMileage.DetailDate = DateTime.Now;
          dbcMileage.Value = model.CurrentMileage;

          var dbmMileage = new CarDetail();
          _context.CarDetail.Add(dbmMileage);
          
          dbmMileage.CarId = dbObj.Id;
          dbmMileage.DetailCode = 2;
          dbmMileage.DetailDate = DateTime.Now;
          dbmMileage.Value = model.MaintenanceMileage;

          var dbInspection = new CarDetail();
          _context.CarDetail.Add(dbInspection);
          
          dbInspection.CarId = dbObj.Id;
          dbInspection.DetailCode = 3;
          dbInspection.DetailDate = DateTime.Now;
          dbInspection.Value = model.InspectionDate;

          var dbInsurance = new CarDetail();
          _context.CarDetail.Add(dbInsurance);
          
          dbInsurance.CarId = dbObj.Id;
          dbInsurance.DetailCode = 9;
          dbInsurance.DetailDate = DateTime.Now;
          dbInsurance.Value = model.InsuranceDate;

          _context.SaveChanges();
        }

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
    [HttpPost]
    [Route("Add")]
    public BusinessResult AddCost(CarDetailModel model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.CarDetail.FirstOrDefault(d => d.Id == model.Id);
        if (dbObj == null)
        {
          dbObj = new CarDetail();
          _context.CarDetail.Add(dbObj);
        }

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
    [HttpPost]
    [Route("EditCar")]
    public BusinessResult EditCar(CarModel model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.Car.FirstOrDefault(d => d.Id == model.Id);
        if (dbObj == null)
        {
          dbObj = new Car();
          _context.Car.Add(dbObj);
        }

        model.MapTo(dbObj);
        _context.SaveChanges();

        var lastInspectionDate = _context.CarDetail.Where(x => x.CarId == model.Id && x.DetailCode == 3).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault();
        if(model.InspectionDate != lastInspectionDate){
          var dbInspection = new CarDetail();
          _context.CarDetail.Add(dbInspection);

          dbInspection.CarId = dbObj.Id;
          dbInspection.DetailCode = 3;
          dbInspection.DetailDate = DateTime.Now;
          dbInspection.Value = model.InspectionDate;
        }

        var lastInsuranceDate = _context.CarDetail.Where(x => x.CarId == model.Id && x.DetailCode == 9).OrderByDescending(m => m.Id).Select(m => m.Value).FirstOrDefault();
        if(model.InsuranceDate != lastInsuranceDate){
          var dbInsurance = new CarDetail();
          _context.CarDetail.Add(dbInsurance);

          dbInsurance.CarId = dbObj.Id;
          dbInsurance.DetailCode = 9;
          dbInsurance.DetailDate = DateTime.Now;
          dbInsurance.Value = model.InsuranceDate;
        }
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
        var dbObj = _context.Car.FirstOrDefault(d => d.Id == id);
        if (dbObj == null)
          throw new Exception("");

        _context.Car.Remove(dbObj);

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