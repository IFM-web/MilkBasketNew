using GuardTour_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using System.Data;
using System.Net;

namespace GuardTour_API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        db_Utility util=new db_Utility();
        ClsUtility csutil = new ClsUtility();
        ResponseMassage Response;

        #region ForgetPassword
        [Route("ForgetPassword")]
        [HttpPost]

        public ResponseMassage ForgetPassword([FromForm] ForgetPassword obj )
        {
            try
            {
                //string ecnpwd = EncryptionHelper.Encrypt(obj.ConfirmPassword);
                //string oldpwden = EncryptionHelper.Encrypt(obj.OldPassword);

                var ds = util.Fill("exec Udp_Forgetpassword @username='" + obj.EmployeeCode.Trim() + "',@oldpwd='" + obj.OldPassword.Trim() + "',@confpwd='" + obj.ConfirmPassword.Trim() + "'");
                string errmsg = ds.Tables[0].Rows[0][1].ToString();
                if (errmsg == "Password Updated SuccessFully")
                {
                   return Response = new ResponseMassage
                    {
                        Massage = errmsg,
                        StatusCode = HttpStatusCode.OK
                    };
                   
                }
                else

                {
                    return Response = new ResponseMassage {
                        Massage = errmsg,
                        StatusCode = HttpStatusCode.NotFound };
                                         
                }
            }
            catch (Exception ex)
            {
                Response.Massage= ex.Message;
                return Response = new ResponseMassage
                {
                    Massage = ex.Message,
                    StatusCode = HttpStatusCode.ExpectationFailed
                };
            }
        }
        #endregion

        #region Employee Login     
        [HttpGet("EmployeeLogin")]
        public IActionResult Login(string EmployeeId, string EmployeePassword)
        {

            try
            {
                var ds = util.Fill("exec EmployeeLogin @EmpId='" + EmployeeId.Trim() + "',@password='" + EmployeePassword.Trim() + "'");
                var data = ds.Tables[0];

                return Content(JsonConvert.SerializeObject(data), "application/json");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        //#region fetchData
        //[Route("fetchData")]
        //[HttpPost]
        //public IActionResult fetchData(string? Data)
        //{
        //    string? data = ""; int flag = 0;
        //    try
        //    {
        //        if (Data == null)
        //        {

        //            data = Request.Form["jsonData"].ToString();
        //            string url = Request.Form["UrlEncript"].ToString();
        //            string encryption = util.cryption(url);
        //        }
        //        else
        //        {
        //            data = Data;
        //        }
        //        DataSet ds = util.Fill("exec Usp_AppApiProcedure N'" + data.Replace("'", "''") + "'");
        //        data = ds.Tables[0].Rows[0]["Data"].ToString();


        //        if (flag == 1)
        //        {
        //            DataTable mailds = JsonConvert.DeserializeObject<DataTable>(ds.Tables[0].Rows[0]["mail"].ToString());
        //            if (mailds.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < mailds.Rows.Count; i++)
        //                {

        //                    string OtpStatus = csutil.SendMailViaIIS_html(mailds.Rows[0]["from"].ToString(), mailds.Rows[0]["to"].ToString(), mailds.Rows[0]["cc"].ToString(), mailds.Rows[0]["bcc"].ToString(), mailds.Rows[0]["subject"].ToString(), "", mailds.Rows[0]["_body"].ToString(), null, mailds.Rows[0]["MAIL_PASSWORD"].ToString(), mailds.Rows[0]["Host"].ToString());
        //                }
        //            }
        //            Random _rdm = new Random();
        //            string genNum = _rdm.Next(1000, 9999).ToString();
        //            csutil.SMSAPInewwithmsg(ds.Tables[0].Rows[0]["sms"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        data = "{\"Message\":\"" + ex.Message + "\",\"Status\":\"error\",\"Data\":\"[]\"}";
        //    }

        //    return Content(data, "application/json");
        //}
        //#endregion

        #region GetTaskdetails

        [HttpGet("GetRouteEmpWise")]
        public IActionResult GetTaskdetails( string EmployeeId,string BranchId,string CompanyId)
        {
            var ds = util.Fill("exec API_Usp_GetTaskdetails @EmpId='"+EmployeeId+ "',@BranchId='"+BranchId+ "',@CompanyId='"+CompanyId+"'");
            if(ds.Tables.Count != 0)
            {
                return Content(JsonConvert.SerializeObject(ds.Tables[0]),"application/json");
            }
            else
            {
                return NotFound(new {Massage="Record Not Found",status="Error"});

            }
         
        }

        #endregion

        //#region GetCustomer

        //[HttpGet("GetCustomer")]
        //public IActionResult GetCustomer(string EmployeeId, string BranchId,string CompanyId)
        //{
        //    var ds = util.Fill("exec APP_Usp_GetCustomerByEmp  @EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
        //    }
        //    else
        //    {
        //        return NotFound(new { Massage = "Not Found", status = HttpStatusCode.NotFound });             

        //    }

        //}

        //#endregion

        //#region GetSite

        //[HttpGet("GetSite")]
        //public IActionResult GetSite(string EmployeeId,string CustomerId, string BranchId, string CompanyId)
        //{
        //    var ds = util.Fill("exec APP_Usp_GetSiteByCustomer  @CustomerId='" + CustomerId+"',@EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
        //    }
        //    else
        //    {
        //        return NotFound(new { Massage = "Not Found", status = HttpStatusCode.NotFound });

        //    }

        //}

        //#endregion

        #region GetAllPost
        [HttpGet("GetRoutePost")]
        public IActionResult GetRoutePost(string EmployeeId, string SiteId, string CompanyId, string BranchId)
        {
            var ds = util.Fill("exec API_Usp_GetPost @Action='All',@EmpId='" + EmployeeId + "',@SiteId='" + SiteId + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
            }
            else
            {
                return NotFound(new { Massage = "Record Not Found", Status = 404, });

            }
        }

        [HttpGet("GetPost")]
        public IActionResult GetPost(string PostId, string EmployeeId, string SiteId, string CompanyId, string BranchId)
        {
            var ds = util.Fill("exec API_Usp_GetPost @Action='Single',@EmpId='" + EmployeeId + "',@SiteId='" + SiteId + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "',@PostId='" + PostId + "'");

            if (ds.Tables[0].Rows.Count != 0)
            {
                return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
            }
            else
            {
                return NotFound(new
                {
                    Status = HttpStatusCode.NotFound,
                    Message = $"No post found with ID = {PostId}"
                });
            }
        }

        #endregion



    }
}
