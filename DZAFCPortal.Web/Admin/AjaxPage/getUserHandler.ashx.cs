using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using DZAFCPortal.ViewModel.OrgTree;

namespace NYPortal.Admin.AjaxPage
{
    /// <summary>
    /// getUserHandler 的摘要说明
    /// </summary>
    public class getUserHandler : IHttpHandler
    {
        DZAFCPortal.Authorization.DAL.OrganizationService oService = new DZAFCPortal.Authorization.DAL.OrganizationService();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();

        public void ProcessRequest(HttpContext context)
        {
            System.Linq.Expressions.Expression<Func<DZAFCPortal.Authorization.Entity.User, bool>> predicate = null;
            var op = context.Request["Op"];

            if (op == "choose")
            {
                predicate = GetUnCheckeUserPredicate(context);
                GetListByPager(context, predicate);
            }
            else if (op == "Checked")
            {
                string checkedUsers = context.Request["checkedUsers"];

                if (!String.IsNullOrEmpty(checkedUsers))
                {
                    string[] checkedUserArray = checkedUsers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    predicate = p => checkedUserArray.Contains(p.ID);
                    var users = userService.GenericService.GetAll(predicate).OrderBy(p => p.DisplayName).ToList();
                    var userModels = from u in users
                                     select CreateUserModel(u);

                    string modelJson = Newtonsoft.Json.JsonConvert.SerializeObject(userModels.ToList());
                    context.Response.Write(modelJson);
                }
                else
                {
                    context.Response.Write("[]");
                }
            }
            else if (op == "all")
            {
       
                    var users = userService.GenericService.GetAll().OrderBy(p => p.DisplayName).ToList();
                    var userModels = from u in users
                                     select CreateUserModel(u);
                    string modelJson = Newtonsoft.Json.JsonConvert.SerializeObject(userModels.ToList());
                    context.Response.Write(modelJson);
            }
            else
            {
                var users = userService.GenericService.GetAll(p => p.Status == 1).OrderBy(p => p.DisplayName).ToList();
                var userModels = from u in users
                                 select CreateUserModel(u);

                string modelJson = Newtonsoft.Json.JsonConvert.SerializeObject(userModels.ToList());
                context.Response.Write(modelJson);
            }
        }


        /// <summary>
        /// 获取选择用户的查看条件
        /// 该条件查询数据时会排除已经选中的项
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Expression<Func<DZAFCPortal.Authorization.Entity.User, bool>> GetUnCheckeUserPredicate(HttpContext context)
        {
            System.Linq.Expressions.Expression<Func<DZAFCPortal.Authorization.Entity.User, bool>> predicate = p => p.Status == 1;

            string key = context.Request["searchKey"];
            string orgId = context.Request["orgId"];
            string checkedUsers = context.Request["checkedUsers"];
            string[] checkedUserArray = new string[0];
            if (!String.IsNullOrEmpty(checkedUsers))
            {
                checkedUserArray = checkedUsers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!String.IsNullOrEmpty(key))
            {
                predicate = p => p.Status == 1 && (p.DisplayName.Contains(key) || p.Account.Contains(key)) && !checkedUserArray.Contains(p.ID);
            }
            else
            {
                if (String.IsNullOrEmpty(orgId)) return null;

                predicate = p => p.Status == 1 && p.OrganizationID == orgId && !checkedUserArray.Contains(p.ID);
            }


            return predicate;
        }


        private void GetListByPager(HttpContext context, System.Linq.Expressions.Expression<Func<DZAFCPortal.Authorization.Entity.User, bool>> predicate)
        {
            context.Response.ContentType = "text/plain";

            int currentIndex = 1;
            if (!String.IsNullOrEmpty(context.Request["currentIndex"]))
                currentIndex = int.Parse(context.Request["currentIndex"]);
            int pageSize = 10;
            if (!String.IsNullOrEmpty(context.Request["pagesize"]))
                pageSize = int.Parse(context.Request["pagesize"]);

            if (predicate == null) return;

            var users = userService.GenericService.GetAll(predicate).OrderBy(p => p.Account).Skip((currentIndex - 1) * pageSize).Take(pageSize).ToList();
            var userModels = from u in users
                             select CreateUserModel(u);

            string modelJson = Newtonsoft.Json.JsonConvert.SerializeObject(userModels.ToList());
            string json = "{ ";
            json += "\"recordCount\": " + userService.GenericService.GetAll(predicate).Count() + " ,";
            json += " \"datas\": " + modelJson;
            json += " }";


            context.Response.Write(json);
        }

        private UserModel CreateUserModel(DZAFCPortal.Authorization.Entity.User user)
        {
            var model = new UserModel();
            model.ID = user.ID;
            model.Account = user.Account;
            model.DisplayName = user.DisplayName;
            model.MobilePhone = user.MobilePhone;
            model.OfficePhone = user.OfficePhone;
            model.Email = user.Email;
            model.OrgId = user.OrganizationID == null ? "" : user.OrganizationID;
            model.LastName = user.LastName;
            model.FirstName = user.FirstName;
            model.PostName = "";
            model.Department = user.OrganizationName;
            model.OfficeName = user.OfficeName;
            model.Address = user.Address;

            return model;
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