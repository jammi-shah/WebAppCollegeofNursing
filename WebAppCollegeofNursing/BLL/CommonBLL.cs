using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class CommonBLL
    {
        public static string FileProfileUpload(HttpPostedFileBase file, string dirPath)
        {
            string filePath = "";
            string extension = (System.IO.Path.GetExtension(file.FileName)).ToLower();
            string fileName = DateTime.Now.ToString("ddmmssffff") + extension;

            int fileSize = file.ContentLength;
            if (fileSize < 4194304)
            {

                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    filePath = dirPath + fileName;
                    file.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                }
                else
                {
                    return "Format";
                }
            }
            else
            {
                return "Size";

            }
            return filePath;

        }
        public static string FileUpload(HttpPostedFileBase file)
        {
            string filePath = "";

            string dirPath = "~/Files/StudyMaterial/";
            string extension = (System.IO.Path.GetExtension(file.FileName)).ToLower();
            string fileName = DateTime.Now.ToString("ddmmssffff") + extension.ToLower();

            int fileSize = file.ContentLength;
            if (fileSize < 444194304)
            {
                
                if (extension == ".mp4" || extension == ".avi" || extension == ".wmv" || extension == ".3gp" || extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx" || extension == ".ppt" || extension == ".pptx" || extension == ".txt")
                {
                    filePath = dirPath + fileName;
                    file.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                }
                else
                {
                    return "Format";
                }
            }
            else
            {
                return "Size";

            }
            return filePath;

        }

        public static void DownloadFile(string FilePath)
        {
            string filePath =HttpContext.Current.Server.MapPath(FilePath);
            WebClient webClient = new WebClient();
            string Extension = Path.GetExtension(FilePath);
            string fileNam = Path.GetFileName(FilePath);
            Byte[] BinaryReader = webClient.DownloadData(filePath);
            if (BinaryReader != null)
            {
                HttpContext.Current.Response.Clear();
                if (Extension == ".pdf")
                {
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileNam);
                }
                else if (Extension == ".docx" || Extension == ".doc")
                {
                    HttpContext.Current.Response.ContentType = "application/word";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                }
                else if (Extension == ".xls" || Extension.ToLower() == ".xlsx")
                {
                    HttpContext.Current.Response.ContentType = "application/excel";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                }
                else if (Extension == ".pptx")
                {
                    HttpContext.Current.Response.ContentType = "application/Powerpoint";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                }
                else if (Extension == ".mp4")
                {
                    HttpContext.Current.Response.ContentType = "video/mp4";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                }
                else if (Extension == ".avi")
                {
                    HttpContext.Current.Response.ContentType = "video/x-msvideo";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                    
                }
                else if (Extension == ".wmv")
                {
                    HttpContext.Current.Response.ContentType = "video/x-ms-wmv";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                }
                else if (Extension == ".3gp")
                {
                    HttpContext.Current.Response.ContentType = "video/3gpp";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileNam);
                }
                else if (Extension == ".jpg" || Extension.ToLower() == ".jpeg")

                {
                    HttpContext.Current.Response.ContentType = "image/jpeg";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileNam);
                }
                else if (Extension == ".png")
                {
                    HttpContext.Current.Response.ContentType = "image/png";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileNam);
                }
                HttpContext.Current.Response.BinaryWrite(BinaryReader);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
           
        }
    }
}
