using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using MilkBasket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using MilkBasket.AuthFilter;
namespace MilkBasket.Controllers

{
    public class HomeController : Controller
    {
        db_Utility Util = new db_Utility();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AuthenticationFilter]
        public IActionResult Index()
        {
            string locationId = HttpContext.Session.GetString("LocationAutoId");
            var Date = DateTime.Now.ToString("yyyy-MM-dd");
          string  Cluester = Util.Fill("exec udp_GetStandardCluster @LocationAutoId='" + locationId + "'").Tables[0].Rows[0][0].ToString();
            var ds = Util.Fill(@$"exec udp_GetEmployeeSuperAttendanceWithCluster @LocationAutoId='{locationId}',@FromDate='{Date}',@ToDate='{Date}',@EmployeeNumber='{null}',@ShiftCode='All'");
            ViewBag.TotalEmp = ds.Tables[1].Rows[0][0];
            ViewBag.TotalPresent = ds.Tables[1].Rows[0][1];
            ViewBag.TotalLatePunch = ds.Tables[1].Rows[0][2];
            ViewBag.TotalAbsent = ds.Tables[1].Rows[0][3];

            return View();
        }

        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login obj)
        {
            var password=EncryptionHelper.Encrypt(obj.Password);
            var ds = Util.Fill("exec Usp_MB_ValidateLogin @Userid ='" + obj.UName + "',@Password='" + password + "'");


            if (ds.Tables.Count>0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    HttpContext.Session.SetString("UserName", ds.Tables[0].Rows[0]["UserName"].ToString());
                    HttpContext.Session.SetString("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
                    HttpContext.Session.SetString("UserGroup", ds.Tables[0].Rows[0]["UserGroup"].ToString());
                   

                    return RedirectToAction("BranchLogin", "Home");
                }
                else
                {
                    ViewBag.message = ds.Tables[0].Rows[0]["Message"];
                }

            }
            else
            {
                ViewBag.message = "Server Side Error";
            }

            return View();
        }
        [HttpGet]
        public IActionResult BranchLogin()

        {
            ViewBag.company = Util.PopulateDropDown("exec MB_DDL 'Company',@Id='"+HttpContext.Session.GetString("UserId")+"'", Util.constring);
            
                return View();
        }
        [HttpPost]
        public IActionResult BranchLogin(BranchLogin obj)
        {
            if (obj.CompanyId != null && obj.BranchId != null && obj.RegionId != null)
            {


                HttpContext.Session.SetString("LocationAutoId", obj.BranchId);
                HttpContext.Session.SetString("CompanyCode", obj.CompanyId);
                HttpContext.Session.SetString("RegionId", obj.RegionId);

                ViewBag.company = Util.PopulateDropDown("exec MB_DDL 'Company',@Id='" + HttpContext.Session.GetString("UserId") + "'", Util.constring);

                var ds = Util.Fill("select distinct CompanyDesc,LocationDesc,r.HrLocationDesc from mstCompany a join mstLocation b on a.CompanyCode=b.CompanyCode join mstHrLocation r on a.companycode=r.CompanyCode  and b.HrLocationCode=r.HrLocationCode where a.CompanyCode  in ('MTS','Milk','FutureZone') and  b.LocationAutoID='" + obj.BranchId + "'");
                HttpContext.Session.SetString("CompanyName", ds.Tables[0].Rows[0]["CompanyDesc"].ToString());
                HttpContext.Session.SetString("BranchName", ds.Tables[0].Rows[0]["LocationDesc"].ToString());
                HttpContext.Session.SetString("RegionName", ds.Tables[0].Rows[0]["HrLocationDesc"].ToString());


                return RedirectToAction("Index", "Home");


            }
            else
            {
                return RedirectToAction("Login", "Home");

            }

        }
        public JsonResult Region(string Company)
        {
            var ds = Util.Fill("exec MB_DDL 'Region',@Id='" + Company + "',@Id3='"+HttpContext.Session.GetString("UserId")+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult RegionBind(string Company)
        {
            var ds = Util.Fill("exec MB_DDL 'Regionbind',@Id='" + Company + "',@Id3='"+HttpContext.Session.GetString("UserId")+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult BindBranch(string Company,string Region)
        {
            var ds = Util.Fill("exec MB_DDL 'Branch' ,@Id='" + Company + "',@Id2='"+Region+"',@Id3='"+HttpContext.Session.GetString("UserId")+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult BindBranchUserrights(string Company,string Region,string UserId)
        {
            var ds = Util.Fill("exec MB_DDL 'BranchUserRights' ,@Id='" + Company + "',@Id2='"+Region+"',@Id3='"+UserId+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult BindBranchbind(string Company,string Region)
        {
            var ds = Util.Fill("exec MB_DDL 'Branchbind' ,@Id='" + Company + "',@Id2='"+Region+"',@Id3='"+HttpContext.Session.GetString("UserId")+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("ErrorPage");
        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
