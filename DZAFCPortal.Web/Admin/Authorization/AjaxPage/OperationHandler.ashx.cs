using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DZAFCPortal.Facade;

namespace DZAFCPortal.Web.Admin.AjaxPage.Authorization
{
    /// <summary>
    /// OperationHandler 的摘要说明
    /// </summary>
    public class OperationHandler : IHttpHandler
    {
        private AuthorizationFacade facade = new AuthorizationFacade();

        public void ProcessRequest(HttpContext context)
        {
            var result = new NySoftland.Core.Result();
            var json = string.Empty;

            var operation = context.Request.Params["Op"];
            if (operation == null)
                return;

            switch (operation)
            {
                case "GetEdit":
                    var moduleid = context.Request.Params["ModuleID"];
                    result = facade.GenerateResourceEditForOperation(moduleid);
                    json = new JavaScriptSerializer().Serialize(result);

                    break;
                case "GetReadonly":
                    result = facade.GenerateResourceReadOnlyForOperation();
                    json = new JavaScriptSerializer().Serialize(result);

                    break;
                case "SaveOperation":
                    var sourceOpBaseSave = context.Request.Params["Source"];
                    var moduleID = context.Request.Params["ModuleID"];
                    result = facade.SaveOperation(sourceOpBaseSave, moduleID);
                    json = new JavaScriptSerializer().Serialize(result);

                    break;
                case "RemoveOperation":
                    var sourceOpCountSave = context.Request.Params["Source"];
                    result = facade.RemoveOperaion(sourceOpCountSave);
                    json = new JavaScriptSerializer().Serialize(result);

                    break;
            }
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