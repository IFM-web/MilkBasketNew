using Microsoft.AspNetCore.Mvc;
using System.Data;
using MilkBasket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using MilkBasket.Models.Admin;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;
using MilkBasket.AuthFilter;
namespace MilkBasket.Controllers
{
    public class AdminController : Controller
    {
      
       ClsUtility ClsUtil=new ClsUtility();

        protected readonly db_Utility Util;
        protected readonly string CompanyCode;
        protected readonly string LocationAutoId;
        protected readonly string RegionId;
        protected readonly string Delete;
        protected readonly string Edit;

        public AdminController(IHttpContextAccessor httpContextAccessor)
        {
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
        public IActionResult Index()
        {
            return View();
        }


        #region MenuRights
        [AuthenticationFilter]
        public IActionResult MenuRights(string? Id )
        {
            ViewBag.username = Util.PopulateDropDown("select TypeCode,TypeName from MB_UserType where status=1");

            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public IActionResult SaveMenuRights()
        {
            var data = Request.Form["data"];
            string status;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
            //using (SqlConnection conn = new SqlConnection(ConnectDB.ConnectionString))
            //using (SqlCommand cmd = new SqlCommand("Usp_Mb_MenuRightsSave", conn))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    // Parameters
            //    cmd.Parameters.AddWithValue("@data", dt);




            //    conn.Open();
            //    status = cmd.ExecuteNonQuery().ToString();

            //}
            using (SqlConnection conn = new SqlConnection(ConnectDB.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Usp_Mb_MenuRightsSave", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@data", dt);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "MenuRights";  

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);

                }
                return Json(JsonConvert.SerializeObject(result));
            }
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public JsonResult GetMenu(string GroupId)
        {
            
            string sql = "exec Usp_Mb_MenuRights '"+GroupId+"'";
            DataSet ds = Util.Fill(sql);
            DataTable dt = ds.Tables[0];
          

            return Json(JsonConvert.SerializeObject(dt));
        }

        #endregion
        [AuthenticationFilter]
        public IActionResult UserGroupList(string? MenuId)
        {
            return View();
        }
        [AuthenticationFilter]
        public IActionResult UserGroup()
        {
            return View();
        }

        public JsonResult SaveUserGroup(string Id,string Groupcode,string Groupname , int status)
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserType 'Insert',@id='{Id}',@TypeCode='{Groupcode}',@TypeName='{Groupname}',@Status='{status}',@Createby='{HttpContext.Session.GetString("UserName")}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult DeleteUserGroup(string Id)
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserType 'Delete',@TypeCode='{Id}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult showUserGroup()
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserType 'Select',@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        [AuthenticationFilter]
        public IActionResult UserRightsList(string? MenuId)
        {
            return View();
        }

        public JsonResult SaveUserRights()
        {
            var data = Request.Form["data"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
            //    var ds = Util.Fill(@$"exec Usp_MB_UserRights 'Insert',@Company='{Company}',@Branch='{Branch}',@Region='{Region}',@GroupId='{GroupId}'");
            using (SqlConnection conn = new SqlConnection(ConnectDB.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Usp_Mb_UserRightsSave", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@data", dt);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "UserRights";

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);

                }
                return Json(JsonConvert.SerializeObject(result));
            }
        }

        public JsonResult DeleteUserRights(string Id)
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserRights 'Delete',@GroupId='{Id}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult ShowRights()
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserRights 'Select' ,@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult ShowRightsDetails(string UserId)
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserRights 'GetDeailsUserRight',@GroupId='{UserId}' ,@Delete='{Delete}',@Edit='{Edit}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        [AuthenticationFilter]
        public IActionResult UserRights()
        {
            ViewBag.company = Util.PopulateDropDown("exec MB_DDL 'CompanyBind'", Util.constring);
            ViewBag.username = Util.PopulateDropDown("exec Usp_MB_UserCreation 'UserDDL',@UserId='"+HttpContext.Session.GetString("UserId")+"'", Util.constring);
            return View();
        }
        [AuthenticationFilter]
        public IActionResult UserCreationList(string MenuId)
        {
            return View();
        }
        [AuthenticationFilter]
        public IActionResult UserCreation(string? Id)
        {
            ViewBag.UserGroup = Util.PopulateDropDown("exec Usp_MB_UserType 'DLLUesrType'");
            ViewBag.Id = Id;
            return View();
        }
        public JsonResult SaveUserCreation(string Id,string UserId, string UserName, string Password, string GroupId,int Status)
        {
            string passwordenn = EncryptionHelper.Encrypt(Password);
            DataSet ds;
            if (Id == null || Id=="0")
            {
                ds = Util.Fill(@$"exec Usp_MB_UserCreation 'Insert',@UserId='{UserId}',@UserName='{UserName}',@UserGroup='{GroupId}',@Password='{passwordenn}',@IsActive='{Status}',@CreateBy='{HttpContext.Session.GetString("UserName")}'");
            }
            else
            {
                ds = Util.Fill(@$"exec Usp_MB_UserCreation 'Update',@UserId='{UserId}',@UserName='{UserName}',@UserGroup='{GroupId}',@Password='{passwordenn}',@IsActive='{Status}',@CreateBy='{HttpContext.Session.GetString("UserName")}'");
            }
           
         
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        public JsonResult GetUserCreation(string UserId)
        {
           
            var ds = Util.Fill(@$"exec Usp_MB_UserCreation 'GetUser',@UserId='{UserId}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        public JsonResult DeleteUsercreate(string Id)
        {
     
            var ds = Util.Fill(@$"exec Usp_MB_UserCreation 'Delete',@UserId='{Id}'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        public JsonResult showUsercreate()
        {
            var ds = Util.Fill(@$"exec Usp_MB_UserCreation 'Select',@Delete='{Delete}',@Edit='{Edit}'");
            if (HttpContext.Session.GetString("UserId") == "System")
            {


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string Password = row["Password"].ToString();
                    row["Password"] = EncryptionHelper.Decrypt(Password);
                }
            }
            else
            {
                ds.Tables[0].Columns.Remove("Password");
            }
            
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        
        }









    }
}
