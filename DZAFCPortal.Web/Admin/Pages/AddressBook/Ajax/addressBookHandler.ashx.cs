using DZAFCPortal.ViewModel.OrgTree;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Admin.Pages.AddressBook.Ajax
{
    /// <summary>
    /// addressBookHandler 的摘要说明
    /// </summary>
    public class addressBookHandler : IHttpHandler
    {
        DZAFCPortal.Authorization.DAL.OrganizationService oService = new DZAFCPortal.Authorization.DAL.OrganizationService();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var op = context.Request["Op"];

            if (String.IsNullOrEmpty(op) || op == "Tree")
            {
                getOrgTreeJson(context);
            }
            else if (op == "Checked")
            {
                getCheckedOrgJson(context);
            }
            else if (op == "Save")
            {
                AddOrUpdate(context);
            }
            else if (op == "Synchronization")
            {
                OrUpdateParent(context);
            }
            else
            {
                context.Response.Write("");
            }
        }

        /// <summary>
        /// 获取组织树节点的 json
        /// </summary>
        /// <param name="context"></param>
        public void getOrgTreeJson(HttpContext context)
        {
            var orgs = oService.GenericService.GetAll().OrderBy(p => p.SortNum).ThenBy(p => p.Name);
            //已选中的组织
            var checkedOrg = context.Request["checkedOrgs"];

            string[] Orgs = new string[0];
            if (!String.IsNullOrEmpty(checkedOrg))
                Orgs = checkedOrg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var orgModels = from o in orgs.AsEnumerable()
                            select CreateOrgModel(o, Orgs);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(orgModels.ToList());

            context.Response.Write(json);
        }

        /// <summary>
        /// 获取选中的组织Json
        /// </summary>
        /// <param name="context"></param>
        private void getCheckedOrgJson(HttpContext context)
        {
            //已选中的组织
            var checkedOrg = context.Request["checkedOrgs"];
            if (!String.IsNullOrEmpty(checkedOrg))
            {
                string[] Orgs = checkedOrg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var orgs = oService.GenericService.GetAll(p => Orgs.Contains(p.ID));
                var orgModels = from o in orgs.AsEnumerable()
                                select CreateOrgModel(o, Orgs);

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(orgModels.ToList());

                context.Response.Write(json);
            }
            else
            {
                context.Response.Write("[]");
            }
        }

        private OrganizationModel CreateOrgModel(Organization o, string[] checkedOrg)
        {
            var model = new OrganizationModel();

            model.ID = o.ID;
            model.Name = o.Name;
            model.ParentID = o.ParentID == null ? "" : o.ParentID;
            model.IsChecked = checkedOrg.Contains(model.ID);
            model.OrderNum = o.SortNum;
            model.IsShow = o.IsShow;
            model.Description = o.Description;
           
            return model;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private void AddOrUpdate(HttpContext context)
        {
            var result = new NySoftland.Core.Result();
            try
            {
                var organizationItem = context.Request["organizationItem"];
                var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Organization>(organizationItem);
                var updateItem = oService.GenericService.GetModel(item.ID);
                updateItem.SortNum = item.SortNum;
                updateItem.IsShow = item.IsShow;
                updateItem.Description = item.Description;
              
                oService.GenericService.Update(updateItem);
                oService.GenericService.Save();

                //子类同时修改状态
                var updateparentItem = oService.GenericService.GetAll().Where(p => p.ParentID == item.ID).ToList();
                if (updateparentItem != null && updateparentItem.Count > 0)
                {
                    foreach (Organization og in updateparentItem)
                    {
                        og.IsShow = item.IsShow;
                        oService.GenericService.Update(og);
                        var userItem = userService.GenericService.GetAll().Where(p => p.OrganizationID == og.ID).ToList();
                        foreach (User user in userItem)
                        {
                            user.IsShow = item.IsShow;
                            userService.GenericService.Update(user);
                            userService.GenericService.Update(user);
                        }

                    }
                    oService.GenericService.Save();
                }


                result.IsSucess = true;
                result.Message = "保存成功";
            }
            catch (Exception ex)
            {
                result.IsSucess = false;

                result.Message = "保存失败！失败原因：" + ex.Message;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            context.Response.Write(json);
        }


        private void OrUpdateParent(HttpContext context)
        {
            
            var result = new NySoftland.Core.Result();
            try
            {
                var organizationItem = context.Request["organizationItem"];

                var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Organization>(organizationItem);
                var updateItem = oService.GenericService.GetModel(item.ID);
                updateItem.IsShow = item.IsShow;
                oService.GenericService.Update(updateItem);


                updateParent(updateItem);

                oService.GenericService.Save();
                userService.GenericService.Save();

                result.IsSucess = true;
                result.Message = "同步成功";
            }
            catch (Exception ex)
            {
                result.IsSucess = false;

                result.Message = "保存失败！失败原因：" + ex.Message;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            context.Response.Write(json);
        }


        private void updateParent(Organization ogtion)
        {
            //更新当前节点的用户
            updateuserItems(ogtion);

            var updateparentItems = oService.GenericService.GetAll().Where(p => p.ParentID == ogtion.ID).ToList();
            if (updateparentItems != null && updateparentItems.Count > 0)
            {
                foreach (var og in updateparentItems)
                {
                    og.IsShow = ogtion.IsShow;
                    oService.GenericService.Update(og);

                    //
                    updateuserItems(og);
                    updateParent(og);
                }
            }
        }

        private void updateuserItems(Organization ogtion)
        {
            var userItem = userService.GenericService.GetAll().Where(p => p.OrganizationID == ogtion.ID).ToList();
            foreach (var user in userItem)
            {
                user.IsShow = ogtion.IsShow;
                userService.GenericService.Update(user);
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