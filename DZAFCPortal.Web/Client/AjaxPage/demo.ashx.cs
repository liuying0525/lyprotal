using DZAFCPortal.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Client.AjaxPage
{
    /// <summary>
    /// demo 的摘要说明
    /// </summary>
    public class demo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var ftp_helper = new FTPHelper("172.16.7.114", "dzafc\\zhanxl", "!!1234abcd", 7000);
            ftp_helper.GetDownloadFileStream("ftp://172.16.7.114:7000/部门111111/20190624_zhanxl.jpg");
            //ftp_helper.Download("/部门111111/", "20190624_zhanxl.jpg", "D:\\ftp_download.jpg");
            //ftp_helper.DownloadToClient("ftp://172.16.7.114:7000/部门111111/20190624_zhanxl.jpg", "down_load_attach.jpg");
            //var files = context.Request.Files;
            //for (int i = 0; i < files.Count; i++)
            //{
            //    //check if exists
            //    bool res = ftp_helper.CheckFileExist("/部门111111/", files[i].FileName);
            //    if (res)
            //    {
            //        ftp_helper.DeleteFile("/部门111111/", files[i].FileName);
            //    }

            //    ftp_helper.UploadHttpPostFile(files[i], "/部门111111/", files[i].FileName);
            //}
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(new { res = "success" }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}