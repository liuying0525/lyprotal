using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace DZAFCPortal.Web.Admin.Authorization
{
    /// <summary>
    /// ModuleManageHandler 的摘要说明
    /// </summary>
    public class ModuleManageHandler : IHttpHandler
    {
        private DZAFCPortal.Facade.AuthorizationFacade facade = new DZAFCPortal.Facade.AuthorizationFacade();

        public void ProcessRequest(HttpContext context)
        {
            var result = new NySoftland.Core.Result();
            var json = string.Empty;
            var saveType = string.Empty;
            var jsonSource = string.Empty;

            var op = context.Request.Params["Op"];
            if (op == null)
                return;

            switch (op)
            {
                case "GetEdit":
                    var flag = Convert.ToBoolean(context.Request.Params["GetApp"]);
                    result = facade.GenerateResourceForModuleManagement(flag);
                    json = new JavaScriptSerializer().Serialize(result);
                    break;
                case "SaveModuleGroup":
                    saveType = context.Request.Params["SaveType"];
                    jsonSource = context.Request.Params["Source"];

                    result = facade.SavgModuleGroup(saveType, jsonSource);
                    json = new JavaScriptSerializer().Serialize(result);
                    break;
                case "SaveModule":
                    saveType = context.Request.Params["SaveType"];
                    jsonSource = context.Request.Params["Source"];

                    result = facade.SaveModule(saveType, jsonSource);
                    json = new JavaScriptSerializer().Serialize(result);
                    break;
                case "Remove":
                    var objectId = context.Request.Params["ID"];
                    var isModuleGroup = Convert.ToBoolean(context.Request.Params["IsModuleGroupType"]);

                    result = facade.Remove(objectId, isModuleGroup);
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