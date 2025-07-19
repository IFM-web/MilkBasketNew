using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using MilkBasket.AuthFilter;
using System.Text;
using Microsoft.Data.SqlClient;
using MilkBasket.Models;

namespace MilkBasket.Controllers
{
    [AuthenticationFilter]
    public class MasterController : Controller
    {
        protected readonly db_Utility Util;
        protected readonly string CompanyCode;
        protected readonly string LocationAutoId;
        protected readonly string RegionId;
        protected readonly string Delete;
        protected readonly string Edit;
        
        public MasterController(IHttpContextAccessor httpContextAccessor) {
            Util = new db_Utility();

            var session = httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                CompanyCode = session.GetString("CompanyCode");
                LocationAutoId = session.GetString("LocationAutoId");
                RegionId = session.GetString("RegionId");
                Delete = session.GetString("Delete");
                Edit = session.GetString("Edit");

               
            }
        }
       
        public IActionResult Menu_Master()
        {
            return View();
        }

        #region EmployeeMaster
        public IActionResult EmployeeMaster(string? Id)
        {
            string decodedId = Uri.UnescapeDataString(Id);
            if (LocationAutoId == null)
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
            ViewBag.EmpNo = decodedId;




            return View();
        }
        public JsonResult CheckEmployee(string EmployeeNo)
        {
            var dt = Util.Fill("exec udp_Check_DuplicateEmpGroupL @CompanyCode=N'" + CompanyCode + "',@EmployeeNumber='"+EmployeeNo+"'").Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public JsonResult InsertEmployeeMB(string empNo, string empName, string designation, string department, string areaID, string contactNo, string emailid,string EmpNoHid)
        {
            DataSet ds =new DataSet();

            if (EmpNoHid == null)
            {


                string doj = DateTime.Today.ToString("MM-dd-yyyy HH:mm");

                 ds = Util.Fill(@$"exec udp_InsertEmployeeMilkBasket @empNo='{empNo}',@empName='{empName}',@dOB='19-08-1988',@dOJ='{doj}',@designation='{designation}',@department='{department}',@areaID='{areaID}',@contactNo='{contactNo}',@baseLocationAutoID='{LocationAutoId}',@baseCompanyCode='{CompanyCode}',@emailid='{emailid}'");
            
            } if(EmpNoHid != null)
            {
                string doj = DateTime.Today.ToString("MM-dd-yyyy HH:mm");

                 ds = Util.Fill(@$"exec udp_UpdateEmployeeMB @empNo='{empNo}',@empName='{empName}',@dOB='19-08-1988',@dOJ='{doj}',@designation='{designation}',@department='{department}',@areaID='{areaID}',@contactNo='{contactNo}',@baseLocationAutoID='{LocationAutoId}',@baseCompanyCode='{CompanyCode}',@Email='{emailid}'");
            }
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public IActionResult EmployeeList(string? MenuId)
        {
           
            return View();
        }
        public JsonResult ShowEmployee(string EmpNo)
        {
            string today = DateTime.Today.ToString("MM-dd-yyyy HH:mm");
            var dt = Util.Fill("exec udpMstHRManagement_AreaMaster_GetAreaID @Locationautoid=N'" + LocationAutoId + "',@AreaIncharge='',@FromDate=N'" + today + "',@ToDate=N'" + today + "',@IsAreaIncharge=N'0'").Tables[0];
            var ds = Util.Fill(@$"exec Usp_MB_Employee_List @LocationAutoID='{LocationAutoId}',@AreaID='{dt.Rows[0]["AreaID"]}',@EmpNo='{EmpNo}',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        public JsonResult DeleteEmployee(string EmpId)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            var ds = Util.Fill(@$"exec udp_deleteEmpMappinggridMB1 @Locationautoid=N'{LocationAutoId}',@AutoId='{EmpId}',@UserName='{UserName}'");
            return Json("Employee Deleted");
        }

        public JsonResult GetEmployee(string AreaId,string EmpNo)
        {
            var ds = Util.Fill(@$"exec udpMstHr_GetEmpDetailGroupLSearch @LocationAutoID=N'{LocationAutoId}',@AreaID='{AreaId}',@EmpNo='{EmpNo}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        #endregion

        #region Society Master

        public IActionResult SocietyMaster(string? MenuId, string? Id)
        {
            ViewBag.SocietyCode = Id
;            return View();
        }
        public IActionResult SocietyList(string? MenuId)
        {
            return View();
        }

        [HttpPost]
        public JsonResult InsertSociety(string SocietyCode, string SocietyName, string Lat, string Lon, string SocietyCodeHid)
        {
            DataSet ds = new DataSet();

            if (SocietyCodeHid == null)
            {


              

                ds = Util.Fill(@$"exec udp_CreateSociety @societyCode='{SocietyCode}',@societyName='{SocietyName}',@latitude='{Lat}',@longitude='{Lon}',@LocationAutoId='{LocationAutoId}'");

            }
            if (SocietyCodeHid != null)
            {
              
                ds = Util.Fill(@$"exec udp_UpdateSociety @societyCode='{SocietyCode}',@societyName='{SocietyName}',@latitude='{Lat}',@longitude='{Lon}',@LocationAutoId='{LocationAutoId}'");
            }
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult DeleteSociety(string Id)
        {
            var message = "";
            var ds = Util.Fill(@$"exec udp_deleteSocietyMB @AutoId='{Id}',@LocationAutoId='{LocationAutoId}'");
            if(ds.Tables.Count == 0)
            {
                message = "Society Deleted !";
            }
            return Json(JsonConvert.SerializeObject(new[] { new { Massage = message} }));
        }
      

        public JsonResult GetSociety(string SocietyCode)
        {
            var ds = Util.Fill(@$"exec udp_SearchSociety @LocationAutoId=N'{LocationAutoId}',@EmployeeCode='{SocietyCode}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult ShowSociety()
        {
            
                 var  ds = Util.Fill(@$"exec Usp_MilkBS_ListProc 'SocietyList',@LocationAutoId='{LocationAutoId}',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        #endregion

        #region Cluster Master
        public IActionResult ClusterMaster(string? MenuId)
        {
            return View();
        }
        public IActionResult ClusterList(string? MenuId)
        {
            return View();
        }

        public IActionResult InsertCluster(string ClusterName)
        {
            DataSet ds = new DataSet();

            //if (SocietyCodeHid == null)
            //{

                ds = Util.Fill(@$"exec udp_insertCluster @clusterName='{ClusterName}',@LocationAutoId='{LocationAutoId}'");

            //}
            //if (SocietyCodeHid != null)
            //{

            //    ds = Util.Fill(@$"exec udp_UpdateSociety @societyCode='{SocietyCode}',@societyName='{SocietyName}',@latitude='{Lat}',@longitude='{Lon}',@LocationAutoId='{LocationAutoId}'");
            //}
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult ShowCluster()
        {

            var ds = Util.Fill(@$"exec Usp_MilkBS_ListProc 'ClusterList',@LocationAutoId='{LocationAutoId}',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult DeleteCluester(string Id)
        {
            var message = "";
            var ds = Util.Fill(@$"exec udp_deleteClusterMB @AutoId='{Id}',@LocationAutoId='{LocationAutoId}'");
            if (ds.Tables.Count == 0)
            {
                message = "Cluster Deleted !";
            }
            return Json(JsonConvert.SerializeObject(new[] { new { Massage = message } }));
        }
        #endregion


        #region SocietyMapToEmployee

        public IActionResult SocietyMapToEmployeeList(string? MenuId)
        {
            return View();
        }
        public IActionResult SocietyMapToEmployee( string? Id) {
            string decodedId = Uri.UnescapeDataString(Id);
            ViewBag.Society = Util.PopulateDropDown("exec udp_GetSocietyList @LocationAutoId='" + LocationAutoId + "'");
            ViewBag.EmpCode = decodedId;
            return View();
        }

        public JsonResult InsertSocietyMapToEmployee(string societyCode, string InsocietyCode, string OutsocietyCode, string EmpCodeHid)
        {
            string message;
            if (EmpCodeHid == null)
            {
                var ds = Util.Fill(@$"exec udp_InsertEmpSocietyMapping @societyCode='{@societyCode}',@InsocietyCode='{InsocietyCode}',@OutsocietyCode='{OutsocietyCode}',@LocationAutoId='{LocationAutoId}'");
                message = "Emp Society Mapped";
                return Json(JsonConvert.SerializeObject( new[] { new { Massage = message } }));
            }
            else
            {
                message = "Emp Society Maping Updated !";
                var ds = Util.Fill(@$"exec udp_UpdateEmpSocietyMapping @societyCode='{@societyCode}',@InsocietyCode='{InsocietyCode}',@OutsocietyCode='{OutsocietyCode}',@LocationAutoId='{LocationAutoId}'");
                return Json(JsonConvert.SerializeObject(new[] { new { Massage = message } }));
            }
        }

        public JsonResult DeleteEmpSociety(string Id)
        {
            var message = "";
            var ds = Util.Fill(@$"exec udp_deleteEmpSocietymappMB @AutoId='{Id}',@LocationAutoId='{LocationAutoId}'");
            if (ds.Tables.Count == 0)
            {
                message = "Emp Society Mapping Deleted !";
            }
            return Json(JsonConvert.SerializeObject(new[] { new { Massage = message } }));
        }

        public JsonResult ShowEmpSociety()
        {

            var ds = Util.Fill(@$"exec Usp_MilkBS_ListProc 'EmpSocietyMapping',@LocationAutoId='{LocationAutoId}',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult GetEmpSociety(string EmpCode)
        {
            var ds = Util.Fill(@$"exec udp_SearchSocietyMapping @LocationAutoId=N'{LocationAutoId}',@EmployeeCode='{EmpCode}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        #endregion


        #region Password Master

        public IActionResult Password(string? MenuId)
        {
            ViewBag.username = Util.PopulateDropDown("exec udp_GetMBLogin2");
            return View();
        }
        [HttpPost]
        public JsonResult UpdatePassword(string Password,string UserId)
        {
            string Passenc = EncryptionHelper.Encrypt(Password);
            var ds = Util.Fill("exec udp_updateMbLoginNew @Password='" + Passenc + "',@userid='"+UserId+"'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        #endregion

        #region CopyData Cluster and Society
        public IActionResult CopyClusterData()
        {
            ViewBag.company = Util.PopulateDropDown("exec MB_DDL 'Company',@Id='" + HttpContext.Session.GetString("UserId") + "'", Util.constring);
            return View();
        }

       
         public JsonResult ShowCopyClusterData()
        {
            var ds = Util.Fill(@$"exec Usp_MilkBS_ListProc 'ClusterListForCopy',@LocationAutoId='{LocationAutoId}',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

       public JsonResult SaveCopyClusterData()
        {
            var data = Request.Form["data"];
            int LocationId = Convert.ToInt32(Request.Form["LocationAutoId"]);
            DataTable dt= JsonConvert.DeserializeObject<DataTable>(data);
            using (SqlConnection conn = new SqlConnection(ConnectDB.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Usp_Mb_CopyClusterDataSave", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LocationAutoId", LocationId);
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@data", dt);
                
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "BulkClusterUpload";

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                   da.Fill(result);

                }
                return Json(JsonConvert.SerializeObject(result));
            }
        }

        #endregion

        #region CopyData Cluster and Society
        public IActionResult CopySocietyData()
        {
            ViewBag.company = Util.PopulateDropDown("exec MB_DDL 'Company',@Id='" + HttpContext.Session.GetString("UserId") + "'", Util.constring);
            return View();
        }


        public JsonResult ShowCopySocietyData()
        {
            var ds = Util.Fill(@$"exec Usp_MilkBS_ListProc 'SocietyListForCopy',@LocationAutoId='{LocationAutoId}',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult SaveCopySocietyData()
        {
            var data = Request.Form["data"];
            int LocationId = Convert.ToInt32(Request.Form["LocationAutoId"]);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
            using (SqlConnection conn = new SqlConnection(ConnectDB.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Usp_Mb_CopySocietyDataSave", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LocationAutoId", LocationId);
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@data", dt);

                //tvpParam.SqlDbType = SqlDbType.Structured;
                //tvpParam.TypeName = "BulkClusterUpload";

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);

                }
                return Json(JsonConvert.SerializeObject(result));
            }
        }
        #endregion

    }
}
