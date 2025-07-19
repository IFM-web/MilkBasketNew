using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using NPOI.OpenXml4Net.OPC.Internal.Unmarshallers;
using NPOI.SS.Formula.Functions;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc.Filters;
using MilkBasket.AuthFilter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MilkBasket.Controllers
{
    [AuthenticationFilter]
    public class ReportController : Controller
    {
        protected readonly db_Utility Util;
        protected readonly string CompanyCode;
        protected readonly string LocationAutoId;
        protected readonly string RegionId;

        public ReportController(IHttpContextAccessor httpContextAccessor)
        {
            Util = new db_Utility();

            var session = httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                CompanyCode = session.GetString("CompanyCode");
                LocationAutoId = session.GetString("LocationAutoId");
                RegionId = session.GetString("RegionId");

            }


        }
        public async Task<IActionResult> PhotoAttendance(string? MenuId 
            )
        {   
            return View();
        }

        public JsonResult ShowEmployeeAttendence(string date)
        {
            var ds = Util.Fill(@$"exec udp_GetEmployeeAttendanceMBNew @LocationAutoId='{LocationAutoId}',@FromDate='{date}',@ToDate='{date}',@EmployeeNumber='All'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public IActionResult SuperAttendence(string? MenuId)
        {
            ViewBag.Cluester = Util.PopulateDropDown("exec udp_GetStandardCluster @LocationAutoId='"+LocationAutoId+"'");
            return View();
        }

        public JsonResult ShowSuperAttendence(string FromDate, string ToDate, string EmployeeNumber, string Cluster)
        {
            var ds = Util.Fill(@$"exec udp_GetEmployeeSuperAttendanceWithCluster @LocationAutoId='{LocationAutoId}',@FromDate='{FromDate}',@ToDate='{ToDate}',@EmployeeNumber='{EmployeeNumber}',@ShiftCode='{Cluster}'");
            var data = new
            {
                Dt =ds.Tables[0],
                Dt2 = ds.Tables[1],
            };
            return Json(JsonConvert.SerializeObject(data));
        }

        public IActionResult FaceRegister(string? MenuId)
        {
            return View();
        }


        public IActionResult ShowFaceRegister(string EmployeeNumber)
        {
            var ds = Util.Fill(@$"exec udp_GetEmployeeFaceRegisteredwithoutImage @LocationAutoId='{LocationAutoId}',@EmployeeNumber='{EmployeeNumber}'");
            var data = new
            {
                Dt = ds.Tables[0],
                Dt2 = ds.Tables[1],
            };
            return Json(JsonConvert.SerializeObject(data));
        
        }

        public JsonResult DeleteFaceRegister(string EmpId)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            var ds = Util.Fill(@$"exec udp_deleteEmpFaceMB @Locationautoid=N'{LocationAutoId}',@AutoId='{EmpId}'");
            return Json("FaceRegister Deleted");
        }
        public IActionResult PayrollCalculation()
        {
            return View();
        }

        public JsonResult ShowPayrollCalculation(string Year ,string Month)
        {
            var ds = Util.Fill(@$"exec udp_GetAttendanceMusterWithShift_NewPortal @LocationAutoId='{LocationAutoId}',@Year='{Year}',@Month='{Month}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
          
        }
        public IActionResult AllHubsPunchingnumbers(string? MenuId)
        {
            return View();
        }

        public JsonResult ShowAllHubsPunchingnumbers(string Date)
        {
            var ds = Util.Fill(@$"exec udp_GetConsolidateCheckIns @Date='{Date}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public IActionResult PTwiseTimebrackets()
        {
            return View();
        }
        public JsonResult ShowAPTwiseTimebrackets(string Date)
        {
            var ds = Util.Fill(@$"exec udp_GetSlotWiseReportMB @Date='{Date}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public IActionResult PayrollCalculationVendorWise(string? MenuId)
        {
            return View();
        }
        public JsonResult ShowPayrollCalculationVendorWise(string Year, string Month)
        {
            var ds = Util.Fill(@$"exec udp_GetAttendanceMusterWithShift_NewPortal_VendorWise @LocationAutoId='{LocationAutoId}',@Year='{Year}',@Month='{Month}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
    }
}
