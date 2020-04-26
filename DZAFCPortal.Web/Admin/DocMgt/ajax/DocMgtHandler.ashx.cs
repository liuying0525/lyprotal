using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DZAFCPortal.Facade;
using DZAFCPortal.Utility;
using Newtonsoft.Json;
using NySoftland.Core;

namespace DZAFCPortal.Web.Admin.DocMgt.ajax
{
    /// <summary>
    /// NavHandler 的摘要说明
    /// </summary>
    public class DocMgtHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var result = new Result();
            var op = context.Request["op"];

            switch (op)
            {
                case "loadTree":
                    var json_objs = new FTPHelper("172.16.7.33", "dzafc\\zhanxl", "!!!123abc", 7000).GetFtpObjectsRecursively("/").OrderBy(o => o.DisplayName);
                    var json = JsonConvert.SerializeObject(json_objs);
                    context.Response.Write(json);
                    context.Response.End();
                    break;
                case "AsyncLoad":
                    var path = context.Request["AbsolutePath"];
                    path = string.IsNullOrEmpty(path) ? "/" : path;
                    var json_objs_async = new FTPHelper("172.16.7.33", "dzafc\\zhanxl", "!!!123abc", 7000).GetFtpObjectsRecursively(path).OrderBy(o => o.DisplayName);
                    var json_async = JsonConvert.SerializeObject(json_objs_async);
                    context.Response.Write(json_async);
                    context.Response.End();
                    break;
            }

            //var json = JsonConvert.SerializeObject(result);
            //context.Response.Write(json);
            //context.Response.End();
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