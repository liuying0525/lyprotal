using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DZAFCPortal.Entity.FTP;
using DZAFCPortal.Service.FTP;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web
{
    public partial class FtpDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var record_id = Request.Params["fileid"];
            //if (string.IsNullOrEmpty(record_id))
            //{
            //    Response.Write("参数缺失或不正确...");
            //    Response.End();
            //}


            //FtpFileService file_service = new FtpFileService();
            //var file = file_service.GenericService.GetModel(record_id);
            //if (file == null) {

            //    Response.Write("参数缺失或不正确...");
            //    Response.End();
            //}
            //var ftp_file_name = file.FileName;
            //var ftp_path = file.Path;

            var ftp_file_name = "down_load_attach.jpg";
            var ftp_path = "/部门111111/20190624_zhanxl.jpg";

            var ftp_helper = new FTPHelper("172.16.7.114", "dzafc\\zhanxl", "!!1234abcd", 7000);

            ftp_helper.Download("", "", "");
            //Stream fs = ftp_helper.GetDownloadFileStream(ftp_path);
            //byte[] bytes = new byte[4096];
            //fs.Read(bytes, 0, bytes.Length);
            //fs.Seek(0, SeekOrigin.Begin);
            //fs.Close();

            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(ftp_file_name, System.Text.Encoding.UTF8));
            //Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();


            //ClientScript.RegisterStartupScript(
            //    Page.GetType(),
            //    "",
            //    "<script language=javascript>window.opener = null;window.open('','_self');window.close();</script>"
            //    );
        }
    }
}