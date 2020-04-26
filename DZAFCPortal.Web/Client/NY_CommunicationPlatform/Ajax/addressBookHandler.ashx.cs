using DZAFCPortal.Utility;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DZAFCPortal.ViewModel.OrgTree;
using DZAFCPortal.Authorization.Web;

namespace DZAFCPortal.Web.Client.NY_CommunicationPlatform.Ajax
{
    /// <summary>
    /// addressBookHandler 的摘要说明
    /// </summary>
    public class addressBookHandler : IHttpHandler
    {
        DZAFCPortal.Authorization.DAL.OrganizationService orgService = new DZAFCPortal.Authorization.DAL.OrganizationService();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();

        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            var op = context.Request["Op"];

            switch (op)
            {
                case "bookAddress":
                    {
                        System.Linq.Expressions.Expression<Func<User, bool>> predicate = GetBookAddressUserPredicate();

                        ResponseUsersByPager(predicate);
                    } break;
                case "orgTree":
                    {
                        ResponseOrgTreeJson();
                    } break;
                case "departmentBookAddress": //部门通讯录
                    {
                        ResponseDepartmentUserJson();
                    } break;
            }
        }

        #region-------------主方法------------
        /// <summary>
        /// 输出组织树节点的 json
        /// </summary>
        public void ResponseOrgTreeJson()
        {
            //待序列化的视图模型
            var orgModels = new List<OrganizationModel>();

            //数据库中的所有部门
       
            var orgsQuery = orgService.GenericService.GetAll(p => p.IsShow && p.IsEnable && p.ParentID != ConstValue.EMPTY_GUID_STR).OrderBy(p => p.SortNum).ThenBy(p => p.Name);
            var orgs = orgsQuery.ToList();

            orgModels = (from o in orgs
                         select CreateOrgModel(o)).ToList();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(orgModels);

            context.Response.Write(json);
        }

        /// <summary>
        /// 响应部门通讯录
        /// </summary>
        /// <param name="context"></param>
        private void ResponseDepartmentUserJson()
        {
            System.Linq.Expressions.Expression<Func<User, bool>> predicate = p => true;
            string key = context.Request["searchKey"];
            string selecttype = context.Request["selectType"];
            var currentAccount = UserInfo.Account;
            //var currentAccount = "王立弘";
            var user = userService.GenericService.GetAll(p => p.Account == currentAccount).FirstOrDefault();
            if (user != null)
            {
                predicate = p => p.OrganizationID == user.OrganizationID;
            }
            else predicate = p => p.OrganizationID == ConstValue.EMPTY_GUID_STR;

            if (!String.IsNullOrEmpty(key))
            {
                if (String.IsNullOrEmpty(selecttype))
                {
                    predicate = predicate.And(p => p.DisplayName.Contains(key) || p.Account.Contains(key));
                }
                else
                {
                    predicate = predicate.And(SelectType());
                }
            }

            predicate = predicate.And(p => p.IsShow);

            ResponseUsersByPager(predicate);
        }

        /// <summary>
        /// 分页输出用户
        /// </summary>
        private void ResponseUsersByPager(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            int currentIndex = 1;
            if (!String.IsNullOrEmpty(context.Request["currentIndex"]))
                currentIndex = int.Parse(context.Request["currentIndex"]);
            int pageSize = 10;
            if (!String.IsNullOrEmpty(context.Request["pagesize"]))
                pageSize = int.Parse(context.Request["pagesize"]);

            if (predicate == null) return;

            var usersQuery = userService.GenericService.GetAll(predicate).OrderBy(p => p.SortNum).ThenBy(p => p.DisplayName).Skip((currentIndex - 1) * pageSize).Take(pageSize);
            var users = usersQuery.ToList();

            //users = temp(users);

            var userModels = from u in users
                             select CreateUserModel(u);

            var jsonItem = new
            {
                recordCount = userService.GenericService.GetAll(predicate).Count(),
                datas = userModels.ToList()
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonItem);
            context.Response.Write(json);
        }

        #endregion

        #region-----------辅助方法-----------

        /// <summary>
        /// 获取通讯录的查询条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private System.Linq.Expressions.Expression<Func<User, bool>> GetBookAddressUserPredicate()
        {
            System.Linq.Expressions.Expression<Func<User, bool>> predicate = p => true;

            string key = context.Request["searchKey"];
            string orgIdString = context.Request["orgId"];
            string selecttype = context.Request["selectType"];
            var currentAccount = UserInfo.Account;

            //限制查询条件
            if (!String.IsNullOrEmpty(key))
            {
                predicate = predicate.And(SelectType());
            }

            //限制组织
            if (!String.IsNullOrEmpty(orgIdString))
            {
                var isContainersChildren = context.Request["isContainsChildren"];
                if (isContainersChildren.Equals("1"))
                {
                    if (String.IsNullOrEmpty(key))
                    {
                        predicate = predicate.And(p => (p.OrganizationID != null && p.OrgPath.Contains(orgIdString)));
                    }
                }
                else
                {
                    predicate = predicate.And(p => p.OrganizationID == orgIdString);
                }
            }

            predicate = predicate.And(p => p.IsShow);
            //设置员工状态为 1（在职） 0表示已离职，不在通讯录中显示
            predicate = predicate.And(p => p.Status == 1);

            return predicate;
        }

        /// <summary>
        /// 根据类型查询条件
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private System.Linq.Expressions.Expression<Func<User, bool>> SelectType()
        {
            System.Linq.Expressions.Expression<Func<User, bool>> predicate = p => true;

            string key = context.Request["searchKey"];
            string selecttype = context.Request["selectType"];
            switch (selecttype)
            {
                case "userName": predicate = predicate.And(p => p.DisplayName.Contains(key) || p.Account.Contains(key));
                    break;
                case "email": predicate = predicate.And(p => p.Email.Contains(key));
                    break;
                //case "homePhone": predicate = predicate.And(p => p.HomePhone.Contains(key));
                //    break;
                case "ipPhone": predicate = predicate.And(p => p.IpPhone.Contains(key));
                    break;
                case "mobilePhone": predicate = predicate.And(p => p.MobilePhone.Contains(key));
                    break;
            }

            return predicate;
        }

        private UserModel CreateUserModel(User user)
        {
            var model = new UserModel();
            model.ID = user.ID;
            model.Account = user.Account;
            model.DisplayName = user.DisplayName;
            model.MobilePhone = user.MobilePhone;
            model.OfficePhone = user.OfficePhone;
            model.IpPhone = user.IpPhone;
            model.Email = user.Email;
            //model.LyncSip = user.LyncSip;
            if (user.OrganizationID == null)
            {
                model.Organization = String.Empty;
                model.OrgId = String.Empty;
                model.OrgPathName = String.Empty;
            }
            else
            {
                var id = user.OrganizationID == null ? Guid.Empty.ToString() : user.OrganizationID;
                var organization = orgService.GenericService.GetModel(id);
                model.Organization = organization == null ? String.Empty : organization.Name;
                model.OrgId = user.OrganizationID;
            }
            model.OrgPathName = user.OrgPathName;

            model.PhotoUrl = Convertphoto(user.PhotoUrl);
            model.Department = user.OrganizationName;
            model.LevelName = user.Title;
         
            return model;
        }

        private OrganizationModel CreateOrgModel(Organization o)
        {
            var model = new OrganizationModel();

            model.ID = o.ID;
            model.Name = o.Name;
            model.Path = o.Path;
            model.PathName = o.PathName;
            model.ParentID = o.ParentID == null ? "" : o.ParentID;
            model.IsChecked = false;

            return model;
        }

        /// <summary>
        /// 转换头像数据
        /// </summary>
        /// <param name="photoBytes"></param>
        /// <returns></returns>
        private string Convertphoto(byte[] photoBytes)
        {
            string img = "";
            if (photoBytes != null)
            {
                img = "data:image/gif;base64," + Convert.ToBase64String(photoBytes);

            }
            else
            {
                img = "/Scripts/Client/images/ny_header_person_pic.png";
            }
            return img;
        }

        #endregion

        /// <summary>
        /// 临时使用的，通过 Ldap同步通讯录信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        //public List<User> temp(List<User> items)
        //{
        //    var accounts = items.Select(p => p.Account);

        //    var tempItems = new SMGPortal.BLL.TranUserService().GetAll(p => accounts.Contains(p.Account)).ToList();
        //    foreach (var item in items)
        //    {
        //        var t = tempItems.FirstOrDefault(p => p.Account == item.Account);
        //        if (t != null)
        //        {
        //            item.MobilePhone = t.MobilePhone;
        //            item.OfficePhone = t.OfficePhone;
        //            item.IpPhone = t.IpPhone;
        //        }
        //    }

        //    return items;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}