using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DZAFCPortal.Facade;
using DZAFCPortal.Utility;
using NySoftland.Core;

namespace DZAFCPortal.Web.Admin.NavMgt.ajax
{
    /// <summary>
    /// NavHandler 的摘要说明
    /// </summary>
    public class NavHandler : IHttpHandler
    {

        NavigateFacade navFacade = new NavigateFacade();
        JavaScriptSerializer jss = new JavaScriptSerializer();


        public void ProcessRequest(HttpContext context)
        {
            var result = new Result();
            var op = context.Request["op"];

            switch (op)
            {
                case "loadTree":
                    result = navFacade.LoadAllNavigator();
                    break;
                case "save":
                    var savetype = context.Request.Params["savetype"];
                    var sourceJson = context.Request.Params["model"];
                    result = navFacade.SaveNavigator(savetype, sourceJson);
                    break;
                case "remove":
                    var id = context.Request.Params["removeid"];
                    result = navFacade.Remove(id);
                    break;
                case "iconUpload":
                    try
                    {
                        HttpPostedFile img = context.Request.Files["uploadFile"];
                        var fileName = Guid.NewGuid() + "_" + img.FileName.Replace('/', '|').Replace('\\', '|').Split('|').LastOrDefault();
                        var path = "/Uploads/NavIcon/";
                        var filePath = path + fileName;

                        var length = img.ContentLength;
                        var datas = new byte[length];
                        var stream = img.InputStream;
                        stream.Read(datas, 0, img.ContentLength);
                        var results = Fxm.Utility.FileHelper.UploadToWeb(new string[] { ".jpg", ".png" }, context.Request.Files["uploadFile"], path, fileName);
                        //var results = FileHelper.UploadDocument(datas,
                        //                                        filePath,
                        //                                        DZAFCPortal.Config.AppSettings.SharePointSiteUrl,
                        //                                        DZAFCPortal.Config.Base.ImageExtensions);
                        result.IsSucess = true;
                        result.Datas = filePath;



                    }
                    catch (Exception ex)
                    {
                        result.IsSucess = false;
                        result.Message = ex.Message;
                    }

                    break;
            }

            var json = jss.Serialize(result);
            context.Response.Write(json);
            context.Response.End();
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