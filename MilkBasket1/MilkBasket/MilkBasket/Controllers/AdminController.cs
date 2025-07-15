using Microsoft.AspNetCore.Mvc;
using System.Data;
using MilkBasket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using MilkBasket.Models.Admin;
using Microsoft.Data.SqlClient;
namespace MilkBasket.Controllers
{
    public class AdminController : Controller
    {
        db_Utility Util=new db_Utility();
       ClsUtility ClsUtil=new ClsUtility();
        public IActionResult Index()
        {
            return View();
        }

        #region Adm_Profile
        public IActionResult Adm_Profile()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
            //{
            //    return RedirectToAction("login", "Admin");
            //}
          //  string Adminid = //HttpContext.Session.GetString("Id");
            ViewBag.Adminid = 1;
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
            //{
            //    return RedirectToAction("login", "Admin");
            //}
            return View();
        }




        [HttpPost]
        public JsonResult SaveProfile(Adm_Profile obj)
        {
            String div = "";
            string query = @$" insert into MilkBasket_Adm_Profile (ProfileName,FlagActive,CreateUser,CreateDate)                                 values('{obj.ProfileName}','{obj.Status}','{obj.CreateUser}',GetDate())";
            string msg = Util.InsUpdt(query,Util.constring);
            if (msg == "Successfull")
            {
                DataSet ds = Util.Fill("Select ProfileId,ProfileName [Profile Name],case FlagActive when 1 then 'Active' else 'InActive' end as Status,('<a class=\"btn btn-sm\" href=\"#\"onclick=\"return DeletebyId('+Cast(ProfileId as varchar)+ ')\"><i style=\"color:red\" class=\"fa fa-trash\"></i></a>') as [Delete Profile] ,('<a class=\"Eedditrow\" href=\"#\"onclick=\"return EditbyId('+Cast(ProfileId as varchar)+ ')\"><i style=\"color:blue\" class=\"fa fa-edit\"></i></a>') as Edit  from MilkBasket_Adm_Profile  \r\n");
                DataTable dt = ds.Tables[0];
                div = ClsUtil.BindDiv(dt);
            }
            var Data = new { status = msg, Data = div };
            return Json(Data);
        }

        public JsonResult BindProfileDiv()
        {
            String div = "";
            string sql = "Select ProfileId,ProfileName [Profile Name],case FlagActive when 1 then 'Active' else 'InActive' end as Status,('<a class=\"btn btn-sm\" href=\"#\"onclick=\"return DeletebyId('+Cast(ProfileId as varchar)+ ')\"><i style=\"color:red\" class=\"fa fa-trash\"></i></a>') as [Delete Profile] ,('<a class=\"Eedditrow\" href=\"#\"onclick=\"return EditbyId('+Cast(ProfileId as varchar)+ ')\"><i style=\"color:blue\" class=\"fa fa-edit\"></i></a>') as Edit  from MilkBasket_Adm_Profile  \r\n";
            DataSet ds = Util.Fill(sql);
            DataTable dt = ds.Tables[0];
            div = ClsUtil.BindDiv(dt);

            return Json(div);
        }
        #endregion

        #region MenuMaster
        public IActionResult Mst_Menu()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
            //{
            //    return RedirectToAction("login", "Admin");
            //}
            //string Adminid = HttpContext.Session.GetString("Id");

            string sqlquery = "select isnull((select max(isnull(PrintOrder,1)) as PrintOrder from MilkBasket_Adm_Menu   where isnull(MenuParent,0) = 0),0) + 1 as PrintOrder";
            DataSet ds = Util.Fill(sqlquery);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                ViewBag.PrintOrderValue = Convert.ToDecimal(dt.Rows[0]["PrintOrder"]);
            }
            //ViewBag.Adminid = Adminid;
            return View();
        }

        [HttpPost]
        public JsonResult ParentMenu(MenuMaster MenuParents)
        {

            DataTable dt = new DataTable();
            DataSet ds = Util.Fill("select isnull((select max(isnull(PrintOrder,1)) as PrintOrder from MilkBasket_Adm_Menu   where isnull(MenuParent,0) = " + MenuParents.MenuParent + "),0) + 1 as PrintOrder   ");
            dt = ds.Tables[0];
            List<MenuMaster> MenuList = new List<MenuMaster>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MenuList.Add(new MenuMaster
                {
                    PrintOrder = Convert.ToDecimal(dr["PrintOrder"])

                });
            }
            return Json(MenuList);


        }



        [HttpPost]
        public IActionResult SaveMenuMaster(MenuMaster obj)
        {
            String div = "";
            string status = "";
            string msg = "";

            string query = @$"insert into MilkBasket_Adm_Menu ( MenuName,ShortName, ButtonView, ButtonAdds, ButtonModify, ButtonInquire, ButtonDelete,PageName, MenuParent ,Status ,PrintOrder , Url, UrlType ,CreateUser ,CreateDate,Parent)                                  values('{obj.MenuName}','{obj.ShortName}','{obj.ButtonView}','{obj.ButtonAdds}','{obj.ButtonModify}','{obj.ButtonInquire}','{obj.ButtonInquire}','{obj.PageName}','{obj.MenuParent}','{obj.Status}','{obj.PrintOrder}','{obj.menu_url}','{obj.type}','{obj.CreateUser}',GetDate(),'{obj.Parent}')";
            status = Util.InsUpdt(query,Util.constring);

            msg = Util.execQuery("exec SP_SetMenuOrder @id = 0");
            if (status == "Successfull")
            {
                string sql = "exec MilkBasket_Sp_ShowMenu";
                DataSet ds = Util.Fill(sql);
                DataTable dt = ds.Tables[0];
                div = ClsUtil.BindDiv(dt);
                msg = "Saved Successfully";
            }
            var Data = new { status = msg, Data = div };
            return Json(Data);
        }

        public JsonResult BindBlood()
        {

            string sqlquery = "exec USp_dropdown 'BindBlood', ''";
            DataSet ds = Util.Fill(sqlquery);
            List<SelectListItem> Category = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new SelectListItem { Text = dr["MenuName"].ToString(), Value = dr["MenuId"].ToString() });
            }
            ViewBag.Category = Category;
            return Json(Category);
        }

        public JsonResult BindMenuDiv()
        {
            String div = "";
            string sql = "exec MilkBasket_Sp_ShowMenu";
            DataSet ds = Util.Fill(sql);
            DataTable dt = ds.Tables[0];
            div = ClsUtil.BindDiv(dt);

            return Json(div);
        }

        #endregion

        #region MenuRights
        public IActionResult MenuRights()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
            //{
            //    return RedirectToAction("login", "Admin");
            //}
            string Adminid = "1"; //HttpContext.Session.GetString("Id");
            ViewBag.cid = 1; //HttpContext.Session.GetString("CompanyId");
            //ViewBag.Adminid = Adminid;
            string query = "select ProfileId,ProfileName from MilkBasket_Adm_Profile";
            //string query = "select ProfileId,ProfileName from Adm_Profile";
            DataSet ds1 = Util.Fill(query);
            List<SelectListItem> Websitelist = new List<SelectListItem>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                Websitelist.Add(new SelectListItem { Text = dr1["ProfileName"].ToString(), Value = dr1["ProfileId"].ToString() });
            }
            ViewBag.Websitelist = Websitelist;


            return View();
        }

        [HttpPost]
        public IActionResult SaveMenuRights([FromBody] List<Adm_MenuList> MenuRight)
        {
            string msg = "";
            string querry = "";
            querry = Util.InsUpdt("exec Usp_SaveAdminData '" + JsonConvert.SerializeObject(MenuRight) + "'", Util.constring);
            //querry = "exec Sp_SaveProfile '" + JsonConvert.SerializeObject(MenuRight) + "'";
            //msg = Util.execQuery(querry, Util.strElect);
            if (querry == "Successfull")
                msg = "Saved Successfully";
            var Data = new { status = msg, Data = "" };
            return Json(Data);
        }

        public IActionResult Logout()

        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public JsonResult BindMenuRightsDiv(int ProfileId = 1, int Hospitalid = 1)
        {
            String div = "";
            string sql = " exec Sp_GetMenuRights " + ProfileId + "";
            DataSet ds = Util.Fill(sql);
            DataTable dt = ds.Tables[0];
            div = ClsUtil.BindDiv(dt);

            return Json(div);
        }

        #endregion

        public IActionResult Adm_Progile()
        {
            return View();
        }


        //public async Task<PartialViewResult> LoadMenu()
        //{
        //    var menuItems = new List<MenuItem>();

           

        //    using (SqlConnection con = new SqlConnection(Util.constring))
        //    {
        //        string query = "SELECT MenuId, MenuName, SubMenuName, ProfileId, PrintOrder FROM Milkbs_Menu ORDER BY PrintOrder";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        con.Open();
        //        SqlDataReader rdr = await cmd.ExecuteReaderAsync();

        //        while (await rdr.ReadAsync())
        //        {
        //            menuItems.Add(new MenuItem
        //            {
        //                MenuId = rdr.GetInt32(0),
        //                MenuName = rdr.GetString(1),
        //                SubMenuName = rdr.IsDBNull(2) ? null : rdr.GetString(2),
        //                ProfileId = rdr.GetInt32(3),
        //                PrintOrder = rdr.GetInt32(4)
        //            });
        //        }
        //    }

        //    var groupedMenus = menuItems
        //        .GroupBy(m => m.MenuName)
        //        .Select(g => new MenuViewModel
        //        {
        //            MenuName = g.Key,
        //            SubMenus = g.Where(x => !string.IsNullOrEmpty(x.SubMenuName)).Select(x => x.SubMenuName).ToList()
        //        }).ToList();

        //    return PartialView("~/Views/Shared/_MenuPartial.cshtml", groupedMenus);
        //}



    }
}
