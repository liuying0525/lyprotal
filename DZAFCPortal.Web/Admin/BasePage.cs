using DZAFCPortal.Service;
using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin
{
    public class BasePage : DZAFCPortal.Authorization.Web.PortalPageBase
    {
        public string ItemID
        {
            get
            {
                return Request["ID"];
            }
        }


        protected new void Page_Init(object sender, EventArgs e)
        {
            var account = DZAFCPortal.Utility.UserInfo.Account;
            var user = new DZAFCPortal.Authorization.BLL.UserAuthorizationBLL().GetUserByAccount(account);

            if (user == null)
            {
                //throw new Exception("当前登录账号未同步到当前系统，请联系管理员添加该账号到当前系统中！");
                Response.Write(String.Format("当前登录账号[{0}]未同步到当前系统，请联系管理员添加该账号到当前系统中！", account));
                Response.End();
            }

            base.Page_Init(sender, e);
        }

        /// <summary>
        /// 标志当前操作
        /// Add|Edit
        /// </summary>
        public DZAFCPortal.Config.Enums.OperateEnum Op
        {
            get
            {
                var op = Request["Op"];
                if (op == null)
                    throw new Exception("缺少Op参数，或参数不为指定的值");

                DZAFCPortal.Config.Enums.OperateEnum _tempOp;
                switch (op)
                {
                    case "Add": _tempOp = DZAFCPortal.Config.Enums.OperateEnum.Add; break;
                    case "Edit": _tempOp = DZAFCPortal.Config.Enums.OperateEnum.Edit; break;
                    default: throw new Exception("未指定对应的Op参数");
                }
                return _tempOp;
            }
        }

        BnDictTypeService typeService = new BnDictTypeService();
        BnDictService bnDictService = new BnDictService();
        public void dropDownBind(string Code, DropDownList droplist,bool isAddFirstItem = false,string key="",string value="")
        {
            var BnType = typeService.GenericService.GetAll(p => p.Code == Code).First();
            var BnDict = bnDictService.GenericService.GetAll(p => p.BnDictTypeID == BnType.ID && p.IsVisible).OrderBy(p => p.OrderNum).ToList();

            var source = new Dictionary<string, string>();
            if (isAddFirstItem)
            {
                source.Add(value,key);
            }
            BnDict.ForEach(n =>
            {
                source.Add(n.Code, n.DisplayName);
            });
         
            droplist.DataSource = source;
            droplist.DataTextField = "value";
            droplist.DataValueField = "key";

            droplist.DataBind();
        }

        //FD_JobInformationService JobInformationService = new FD_JobInformationService();
        //public void dropDownBindJob(string Name, DropDownList droplist)
        //{
        //    var job = JobInformationService.GenericService.GetAll(p => p.Department.Contains(Name) && p.Enable == 1).OrderBy(p => p.OrderNum).ToList();
        //    var source = new Dictionary<string, string>();

        //    job.ForEach(n =>
        //    {
        //        source.Add(n.ID, n.Position);
        //    });

        //    droplist.DataSource = source;
        //    droplist.DataTextField = "value";
        //    droplist.DataValueField = "key";

        //    droplist.DataBind();
        //}

        public string GetBasicDateName(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                string name = bnDictService.GenericService.GetAll(p => p.Code == code).FirstOrDefault().DisplayName;
                return name;
            }
            return "";
        }
        UserService userService = new UserService();
        public string GetDisplayNameByAccount(string account)
        {
            return userService.getUserName(account);
        }

        /// <summary>
        /// 格式化URL
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        //public string FormatUrl(object instanceId, string Type)
        //{
        //    var param = BDHelper.EncodeParam(int.Parse(instanceId.ToString()));
        //    string tp = Type;
        //    if(Type == "ReimbursementApply")
        //    {
        //        tp = "PositiveApply";
        //    }
        //    string url = string.Format("/workflow/Templates/{0}/{1}Display.aspx?InstanceID={2}&CheckCode={3}", tp, Type, instanceId.ToString(), param);
        //    return url;
        //}

        //public string FormatHtmlControl(object instanceId, object title,object type)
        //{
        //    if (instanceId == null || instanceId.ToString() == "0") return string.Format("<span>{0}</span>", title);
        //    else
        //        return string.Format("<a href='{0}' target='_blank'>{1}</a>",
        //            FormatUrl(instanceId, type.ToString()),
        //            title);
        //}
    }
}