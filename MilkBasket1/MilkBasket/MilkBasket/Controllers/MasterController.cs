using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace MilkBasket.Controllers
{
    public class MasterController : Controller
    {
        protected readonly db_Utility Util;
        protected readonly string CompanyCode;
        protected readonly string LocationAutoId;
        protected readonly string RegionId;
        
        public MasterController(IHttpContextAccessor httpContextAccessor) {
            Util = new db_Utility();

            var session = httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                CompanyCode = session.GetString("CompanyCode");
                LocationAutoId = session.GetString("LocationAutoId");
                RegionId = session.GetString("RegionId");
               
            }
        }
       
        public IActionResult Menu_Master()
        {
            return View();
        }

        public IActionResult EmployeeMaster()
        {

            if(LocationAutoId == null)
            {
                return RedirectToAction("Login", "Home");
            }
             string today  = DateTime.Today.ToString("MM-dd-yyyy HH:mm");


            ViewBag.Designation = Util.PopulateDropDown("exec udpMst_Designation_GetAll @CompanyCode='"+CompanyCode+"'");
            var dt = Util.Fill("exec udpMstHRManagement_AreaMaster_GetAreaID @Locationautoid=N'"+LocationAutoId+"',@AreaIncharge='',@FromDate=N'"+today+"',@ToDate=N'"+today+"',@IsAreaIncharge=N'0'").Tables[0];

            var dt2 = Util.Fill("exec udpMst_Department_GetAll @CompanyCode=N'" + CompanyCode + "'").Tables[0];

            ViewBag.DepartmentCode = dt2.Rows[0]["DepartmentCode"];
            ViewBag.AreaID = dt.Rows[0]["AreaID"];
            ViewBag.AreaDesc = dt.Rows[0]["AreaDesc"];




            return View();
        }
        public JsonResult CheckEmployee(string EmployeeNo)
        {
            var dt = Util.Fill("exec udp_Check_DuplicateEmpGroupL @CompanyCode=N'" + CompanyCode + "',@EmployeeNumber='"+EmployeeNo+"'").Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public JsonResult InsertEmployeeMB(string empNo, string empName, string designation, string department, string areaID, string contactNo, string emailid)
        {
            
            string doj = DateTime.Today.ToString("MM-dd-yyyy HH:mm");

            var ds = Util.Fill(@$"exec udp_InsertEmployeeMilkBasket @empNo='{empNo}',@empName='{empName}',@dOB='19-08-198',@dOJ='{doj}',@designation='{designation}',@department='{department}',@areaID='{areaID}',@contactNo='{contactNo}',@baseLocationAutoID='{LocationAutoId}',@baseCompanyCode='{CompanyCode}',@emailid='{emailid}'");

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public IActionResult EmployeeList()
        {
            return View();
        }
        public JsonResult ShowEmployee(string EmpNo)
        {
            string today = DateTime.Today.ToString("MM-dd-yyyy HH:mm");
            var dt = Util.Fill("exec udpMstHRManagement_AreaMaster_GetAreaID @Locationautoid=N'" + LocationAutoId + "',@AreaIncharge='',@FromDate=N'" + today + "',@ToDate=N'" + today + "',@IsAreaIncharge=N'0'").Tables[0];
            var ds = Util.Fill(@$"exec udpMstHr_GetEmpDetailGroupLSearch @LocationAutoID='{LocationAutoId}',@AreaID='{dt.Rows[0]["AreaID"]}',@EmpNo='{EmpNo}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        public JsonResult DeleteEmployee(string EmpId)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            var ds = Util.Fill(@$"exec udp_deleteEmpMappinggridMB1 @Locationautoid=N'{LocationAutoId}',@AutoId='{EmpId}',@UserName='{UserName}'");
            return Json("Employee Deleted");
        }

    }
}
