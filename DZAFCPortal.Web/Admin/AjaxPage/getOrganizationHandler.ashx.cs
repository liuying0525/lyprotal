
using DZAFCPortal.ViewModel.OrgTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYPortal.Admin.AjaxPage
{
    /// <summary>
    /// 获取组织结构树Json
    /// </summary>
    public class getOrganizationHandler : IHttpHandler
    {
        DZAFCPortal.Authorization.DAL.OrganizationService oService = new DZAFCPortal.Authorization.DAL.OrganizationService();

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
                var orgs = oService.GenericService.GetAll(p => Orgs.Contains(p.ID)).ToList();
                var orgModels = from o in orgs
                                select CreateOrgModel(o, Orgs);

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(orgModels.ToList());

                context.Response.Write(json);
            }
            else
            {
                context.Response.Write("[]");
            }
        }

        private OrganizationModel CreateOrgModel(DZAFCPortal.Authorization.Entity.Organization o, string[] checkedOrg)
        {
            var model = new OrganizationModel();

            model.ID = o.ID;
            model.Name = o.Name;
            model.ParentID = o.ParentID == null ? "" : o.ParentID;
            model.IsChecked = checkedOrg.Contains(model.ID);
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