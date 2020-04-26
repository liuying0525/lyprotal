using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using NySoftland.Core;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.ViewModel.OrgTree;

namespace DZAFCPortal.ClientWeb.AjaxPage
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
            Expression<Func<User, bool>> predicate = null;
            var op = context.Request["Op"];

            if (op == "choose")
            {
                predicate = GetUnCheckeUserPredicate(context);
                GetListByPager(context, predicate);
            }
            else if (op == "addressbook")
            {
                predicate = GetAddressBookUserPredicate(context);
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
        }


        /// <summary>
        ///  获取组织通讯录查询条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private System.Linq.Expressions.Expression<Func<User, bool>> GetAddressBookUserPredicate(HttpContext context)
        {
            System.Linq.Expressions.Expression<Func<User, bool>> predicate = p => p.Status == 1;

            bool isSearchKey = false;
            string orgId = context.Request["orgId"];

            predicate = p => (p.Status == 1);

            #region------获取查询条件----------
            var account = context.Request["Account"];
            if (!String.IsNullOrEmpty(account))
            {
                predicate = predicate.And(p => (p.DisplayName.Contains(account) || p.Account.Contains(account)));

                isSearchKey = true;
            }
            var postName = context.Request["PostName"];
            if (!String.IsNullOrEmpty(postName))
            {
                //predicate = predicate.And(p => (p.PostName.Contains(postName)));

                //isSearchKey = true;
            }
            var department = context.Request["Department"];
            if (!String.IsNullOrEmpty(department))
            {
                predicate = predicate.And(p => (p.OrganizationName.Contains(department)));

                isSearchKey = true;
            }
            var officeName = context.Request["OfficeName"];
            if (!String.IsNullOrEmpty(officeName))
            {
                predicate = predicate.And(p => (p.OfficeName.Contains(officeName)));

                isSearchKey = true;
            }
            var address = context.Request["Address"];
            if (!String.IsNullOrEmpty(address))
            {
                predicate = predicate.And(p => (p.Address.Contains(address)));

                isSearchKey = true;
            }
            var officePhone = context.Request["OfficePhone"];
            if (!String.IsNullOrEmpty(officePhone))
            {
                predicate = predicate.And(p => (p.OfficePhone.Contains(officePhone)));

                isSearchKey = true;
            }
            #endregion


            if (!isSearchKey)
            {
                if (string.IsNullOrEmpty(orgId)) return null;


                predicate = predicate.And(p => (p.OrganizationID == orgId));
            }

            return predicate;
        }


        /// <summary>
        /// 获取选择用户的查看条件
        /// 该条件查询数据时会排除已经选中的项
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private System.Linq.Expressions.Expression<Func<User, bool>> GetUnCheckeUserPredicate(HttpContext context)
        {
            System.Linq.Expressions.Expression<Func<User, bool>> predicate = p => p.Status == 1;

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


        private void GetListByPager(HttpContext context, System.Linq.Expressions.Expression<Func<User, bool>> predicate)
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

        private UserModel CreateUserModel(User user)
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