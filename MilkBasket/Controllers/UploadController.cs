using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MilkBasket.Controllers
{
    public class UploadController : Controller
    {
        db_Utility Utility = new db_Utility();
        [HttpPost]
        public ActionResult Import()
        {
           
            string sqlQuery = string.Empty;
            IFormFile file = Request.Form.Files[0];  
            string Type = Request.Form["Type"].ToString();
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
                    string companyCode = HttpContext.Session.GetString("CompanyCode");
                    string locationId = HttpContext.Session.GetString("LocationAutoId");

                    string procedureName = "";
                    if(Type == "EmployeeUpload")
                    {
                        procedureName = "udp_BulkEmployeeUploadSAWithEmail";
                    }
                    else if(Type == "SocietyUpload")
                    {
                        procedureName = "udp_BulkSocietyUploadSA";
                    }
                    else if(Type=="Cluster")
                    {
                        procedureName = "udp_BulkClusterUploadSA";
                    }
                    else if (Type == "EmpSocietyMapping")
                    {
                        procedureName = "udp_BulkEmpSocietyMappingUploadSA";
                    }
                  

                    using (SqlConnection conn = new SqlConnection(Utility.constring))
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters
                        cmd.Parameters.AddWithValue("@EmployeeData", dt);
                        string[] excludedTypes = { "SocietyUpload", "Cluster", "EmpSocietyMapping" };

                        if (!excludedTypes.Contains(Type))
                        
                        {
                            cmd.Parameters.AddWithValue("@CompanyCode", companyCode);
                        }
                      
                        cmd.Parameters.AddWithValue("@LocationAutoID", locationId);

                        conn.Open();
                        status = cmd.ExecuteNonQuery().ToString();
                        
                    }
                   


                }
            }
            return Json(status);

        }

        public JsonResult DownLoadForMat(string ActoinType)
        {
            var ds = Utility.Fill("exec Usp_MilkBS_ListProc '"+ActoinType+"','0'");
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
    }
}
