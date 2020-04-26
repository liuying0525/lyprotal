
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Authorization.Web
{
    public class ControlHelper
    {
        OperationUrlSerivce opUlrService = new OperationUrlSerivce();

        private List<Operation> lstUserOperation;
        private string url;
        //  private List<string> lstOperationID;

        public ControlHelper(string account)
        {
            this.url = System.Web.HttpContext.Current.Request.Url.PathAndQuery.ToLower();
            lstUserOperation = UserOperation.GetUserOperation(account);

            if (lstUserOperation == null)
            {
                //lstOperationID = lstUserOperation.Select(p => p.ControlID).ToList();
                lstUserOperation = new List<Operation>();
            }
        }



        public void ControlAuthorization(ControlCollection controls)
        {
            if (lstUserOperation.Count <= 0)
            {
                throw new Exception("您没权限操作!");
            }

            foreach (Control control in controls)
            {
                ControlAuthorization(control);

                if (control.HasControls())
                {
                    ControlAuthorization(control.Controls);
                }
            }
        }

        public void ControlAuthorization(Control control)
        {
            string tag = "";
            if (control is HtmlControl)
            {
                tag = (control as HtmlControl).Attributes["Tag"];
            }
            else if (control is WebControl)
            {
                tag = (control as WebControl).Attributes["Tag"];
            }

            if (!String.IsNullOrEmpty(tag))
            {
                if (!isControlAuth(tag)) control.Visible = false;
            }
        }
        /// <summary>
        /// 判断控件是否有可见权限
        /// </summary>
        /// <returns></returns>
        private bool isControlAuth(string tag)
        {
            var rs = false;

            foreach (var op in lstUserOperation)
            {
                if (op.ControlID == tag)
                {
                    var urls = opUlrService.GenericService.GetAll(p => p.OperationID == op.ID);

                    foreach (var u in urls)
                    {
                        if (url.ToLower().Contains(u.URL.ToLower()))
                        {
                            rs = true;
                        }
                    }
                }
            }

            return rs;
        }
    }
}
