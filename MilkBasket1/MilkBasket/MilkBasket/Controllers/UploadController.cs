using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MilkBasket.Controllers
{
    public class UploadController : Controller
    {
        db_Utility Utility = new db_Utility();
        [HttpPost]
        public ActionResult Import1()
        {
            string UserId = HttpContext.Session.GetString("Id");
            string sqlQuery = string.Empty;
            IFormFile file = Request.Form.Files[0];
            string TypeValue = Request.Form["TypeValue"].ToString();
            string UploadTypeName = Request.Form["TypeName"].ToString();
            string folderName = "wwwroot";
            string extension = Path.GetExtension(file.FileName);
            string filename = Path.GetFileNameWithoutExtension(file.FileName);
            string status = string.Empty;
            string Div = "";
            DataTable dt =new DataTable();
            DataSet ds=new DataSet();
            //string webRootPath = HostingEnvironment.WebRootPath;
            //string webRootPath = _hostingEnvironment.WebRootPath;
            string webRootPath = filename + extension;
            string newPath = Path.Combine(folderName);
            //string httpfile = HttpWebRequest.Create(newPath).ToString();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file != null)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        //int nosh = hssfwb.NumberOfSheets;
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;

                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        dt.Columns.Add(cell.ToString());
                        // dt.Columns.Add("RC" + col.ToString());
                    }
                    for (int rw = (sheet.FirstRowNum + 1); rw <= sheet.LastRowNum; rw++) //Read Excel File
                    {
                        DataRow dr = dt.NewRow();
                        IRow row = sheet.GetRow(rw);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int col = row.FirstCellNum; col < cellCount; col++)
                        {
                            if (row.GetCell(col) != null)

                                //dr[col.ToString()] = dr[row.GetCell(col).ToString()];
                                dr[col] = row.GetCell(col).ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                    String JsonData = JsonConvert.SerializeObject(dt);
                    string Query = "exec UploadData N'" + JsonData.Replace("'", "''") + "'," + Convert.ToInt32(TypeValue) + "," + Convert.ToInt32(UserId) + "," + Convert.ToInt32(HttpContext.Session.GetString("CompanyId")) + "";

                    ds = Utility.UploadExcel(Query, Utility.constring);
                    DataTable dt1 = ds.Tables[0];
                    
                }
            }
            return Json(Div.ToString());

        }
    }
}
