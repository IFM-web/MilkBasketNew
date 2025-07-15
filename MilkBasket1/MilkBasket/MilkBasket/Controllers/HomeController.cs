using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;

using MilkBasket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        public IActionResult Index()
        {
            
            string query = "exec udp_GetSociety @Locationautoid=20924";

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    ViewBag.data = dt;
                    
                }
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login obj)
        {
            //var ds = Util.Fill("exec udp_ValidateLogin @Userid ='" + obj.UName + "',@Password='" + obj.Password + "'");


           // string errorMessage = ds.Tables[0].Rows[0][1].ToString();


            if (obj.UName == "System")
            {
                if (obj.Password == "Admin@123")
                {
                    HttpContext.Session.SetString("UserName", obj.UName);
                    //HttpContext.Session.SetString("UserName", (obj.UName.ToString()).ToUpper());
                    //HttpContext.Session.SetString("password", (obj.Password.ToString()).ToUpper());

                    //string[] compcode = ds.Tables[0].Rows[0][0].ToString().Split(",");
                    //string[] compdesc = ds.Tables[0].Rows[0][1].ToString().Split(",");

                    //List<SelectListItem> list = new List<SelectListItem>();


                    //for (var i = 0; i < compcode.Length; i++)
                    //{
                    //    list.Add(new SelectListItem
                    //    {
                    //        Text = compdesc[i],
                    //        Value = compcode[i],
                    //    });
                    //}

                 

                   
                        return RedirectToAction("BranchLogin", "Home");
                    


                }
                else
                {
                    ViewBag.message = "Invailed UserName";
                }

            }
            else
            {
                ViewBag.message = "Invailed Password";
            }

            return View();
        }

        public IActionResult BranchLogin()

        {
            ViewBag.company = Util.PopulateDropDown("select CompanyCode,CompanyDesc from mstcompany where companycode in('MTS','Milk','FutureZone')",Util.constring);
            return View();
        }
        [HttpPost]
        public IActionResult BranchLogin(BranchLogin obj)
        {

    
            if(obj.BranchId != null)
            {
                HttpContext.Session.SetString("LocationAutoId", obj.BranchId);
                HttpContext.Session.SetString("CompanyCode", obj.CompanyId);
                HttpContext.Session.SetString("RegionId", obj.RegionId);
                ViewBag.company = Util.PopulateDropDown("select CompanyCode,CompanyDesc from mstcompany where companycode in('MTS','Milk','FutureZone')", Util.constring);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
          
            
        }




        public JsonResult Region(string Company)
        {
            var ds = Util.Fill("select HrLocationCode as Id,HrLocationDesc as Name from mstHrLocation where CompanyCode='" + Company + "'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult BindBranch(string Company,string Region)
        {
            var ds = Util.Fill("select LocationAutoID as Id,LocationDesc as Name from mstLocation where CompanyCode='" + Company + "' and HrLocationCode='"+Region+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
