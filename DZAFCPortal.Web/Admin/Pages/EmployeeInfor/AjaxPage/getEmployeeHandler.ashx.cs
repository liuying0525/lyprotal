using Newtonsoft.Json;
using DZAFCPortal.Service;
using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Admin.Pages.EmployeeInfor.AjaxPage
{
    /// <summary>
    /// getEmployeeHandler 的摘要说明
    /// </summary>
    public class getEmployeeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var operation = context.Request["Op"];
            var result = new Result();
            if (operation == null)
                return;
            switch (operation)
            {
                case "validateUser":
                    var Account = context.Request.Params["Account"];
                    result = validateUser(Account);
                    break;
            }

            var json = JsonConvert.SerializeObject(result);

            context.Response.Write(json);
            context.Response.End();
        }

        private Result validateUser(string Account)
        {
            EmployeeInforService employeeInforService = new EmployeeInforService();
            Result result = new Result();
            try
            {
                int count = employeeInforService.GenericService.GetAll(p => p.NameAccount == Account).Count();
                if (count > 0)
                {
                    result.IsSucess = false;
                    result.Message = "用户已存在";
                }
                else
                {
                    result.IsSucess = true;
                    result.Message = "";
                }
            }
            catch(Exception ex)
            {
                result.Message = "验证失败,ex:" + ex.Message;
                result.IsSucess = false;
            }
            return result;
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