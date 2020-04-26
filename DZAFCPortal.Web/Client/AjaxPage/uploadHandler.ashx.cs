using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Client.AjaxPage
{
    /// <summary>
    /// uploadHandler 的摘要说明
    /// </summary>
    public class uploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //文件上传的目录
            string floder = context.Request["floder"];
            HttpPostedFile file = context.Request.Files["contentFile"];

            var json = String.Empty;
            if (file != null && file.ContentLength > 0)
            {
                if (!String.IsNullOrEmpty(context.Request["maxLength"]))
                {
                    int maxLength = 0;
                    int.TryParse(context.Request["maxLength"], out maxLength);

                    if (maxLength < file.ContentLength)
                    {
                        var result = new Result();
                        result.IsSucess = false;
                        result.Message = string.Format("只允许上传小于{0}的图片！", Fxm.Utility.FileHelper.FileSizeToString(maxLength));

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

                //var result1 = FileHelper.UploadDocument(datas, 
                //                                                    filePath,
                //                                                    DZAFCPortal.Config.AppSettings.SharePointSiteUrl,
                //                                                    DZAFCPortal.Config.Base.ImageExtensions);

                throw new Exception("需替换上传方法");
                var res = new Result();

                json = Newtonsoft.Json.JsonConvert.SerializeObject(res);
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