using DZAFCPortal.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DZAFCPortal.Web.Admin.AjaxPage.Authorization
{
    /// <summary>
    /// URLHandler 的摘要说明
    /// </summary>
    public class URLHandler : IHttpHandler
    {
        private AuthorizationFacade facade = new AuthorizationFacade();
        public void ProcessRequest(HttpContext context)
        {
            var operation = context.Request.Params["Op"];
            if (operation == null)
                return;

            switch (operation)
            {
                case "LoadURL":
                    var operationID = context.Request.Params["OperationID"];
                    var resultLoad = facade.GenerateResourceReadOnlyForURL(operationID);
                    var jsonLoad = new JavaScriptSerializer().Serialize(resultLoad);
                    context.Response.Write(jsonLoad);
                    context.Response.End();
                    break;
                case "SaveURL":
                    var sourceURL = context.Request.Params["Source"];
                    var opID = context.Request.Params["OpID"];
                    var resultURL = facade.SaveUrl(sourceURL, opID);
                    var jsonUrl = new JavaScriptSerializer().Serialize(resultURL);
                    context.Response.Write(jsonUrl);
                    context.Response.End();
                    break;


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