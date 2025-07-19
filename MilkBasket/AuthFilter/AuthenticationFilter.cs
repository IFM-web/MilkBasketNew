using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MilkBasket.AuthFilter
{

    public class AuthenticationFilter : ActionFilterAttribute
    {
        protected int MenuId = 0;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetString("LocationAutoId");
            if (context.ActionArguments.TryGetValue("Menuid", out var idValue))
            {
                 MenuId = Convert.ToInt32(idValue);
            }

            if (string.IsNullOrEmpty(userId))
            {
                var controller = (Controller)context.Controller;

                //context.Result = new RedirectResult("~/Home/Login");
                context.Result = new RedirectToActionResult("Login", "Home", null);

            }
            else
            {
                if (MenuId != 0)
                {
                    db_Utility uti = new db_Utility();
                    var ds1 = uti.Fill("exec Usp_MB_ActionRights @UserId='" + context.HttpContext.Session.GetString("UserId") + "',@MenuId='" + MenuId + "'");
                    if (ds1.Tables.Count > 0)
                    {
                        context.HttpContext.Session.SetString("MenuId", ds1.Tables[0].Rows[0]["MenuId"].ToString());
                        context.HttpContext.Session.SetString("View", ds1.Tables[0].Rows[0]["FlagView"].ToString());
                        context.HttpContext.Session.SetString("Add", ds1.Tables[0].Rows[0]["FlagAdd"].ToString());
                        context.HttpContext.Session.SetString("Edit", ds1.Tables[0].Rows[0]["FlagModify"].ToString());
                        context.HttpContext.Session.SetString("Delete", ds1.Tables[0].Rows[0]["FlagDelete"].ToString());
                    }
                }
                
                //    var ds = uti.Fill("select FlagAdd from GridRights where ProfileId='" + context.HttpContext.Session.GetString("ProfileId") + "'", uti.strElect);

                //    if (ds.Tables.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            context.HttpContext.Session.SetString("FlagAdd", ds.Tables[0].Rows[0][0].ToString());
                //        }

                //    }

                //}


            }
        }
    }
    
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               