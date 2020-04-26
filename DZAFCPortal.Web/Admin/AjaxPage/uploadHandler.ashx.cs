using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Admin.AjaxPage
{
    /// <summary>
    /// uploadHandler 的摘要说明
    /// </summary>
    public class uploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //var result3 = new NySoftland.Core.Result();
            //result3.IsSucess = true;
            //result3.Datas = "/Scripts/Admin/images/BEA_AD_logo.png";

            //context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result3));
            //return;

            //文件上传的目录
            string floder = context.Request["floder"];
            HttpPostedFile file;
            if (String.IsNullOrEmpty( context.Request["file"]))
            {
               file = context.Request.Files["contentFile"];
            }
            else file = context.Request.Files[context.Request["file"]];

            var json = String.Empty;
            if (file != null && file.ContentLength > 0)
            {
                if (!String.IsNullOrEmpty(context.Request["maxLength"]))
                {
                    int maxLength = 0;
                    int.TryParse(context.Request["maxLength"], out maxLength);

                    if (maxLength < file.ContentLength)
                    {
                        var result = new NySoftland.Core.Result();
                        result.IsSucess = false;
                        result.Message = String.Format("只允许上传小于{0}的图片！", Fxm.Utility.FileHelper.FileSizeToString(maxLength));

                        json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                }

                string fileName = Guid.NewGuid().ToString() + "_" + System.IO.Path.GetFileName(file.FileName);
                var filePath = floder + @"/" + fileName;

                var datas = new byte[file.ContentLength];
                var stream = file.InputStream;
                stream.Read(datas, 0, file.ContentLength);

                var result1 = FileHelper.UploadDocument(datas, filePath, DZAFCPortal.Config.AppSettings.SharePointSiteUrl, DZAFCPortal.Config.Base.ImageExtensions);

                json = Newtonsoft.Json.JsonConvert.SerializeObject(result1);
                context.Response.Write(json);
            }
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