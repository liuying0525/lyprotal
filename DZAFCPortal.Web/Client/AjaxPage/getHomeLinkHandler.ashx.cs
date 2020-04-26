using DZAFCPortal.Facade;
using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DZAFCPortal.Service;
using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Entity;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web.Client.AjaxPage
{
    /// <summary>
    /// getHomeLinkHandler 的摘要说明
    /// </summary>
    public class getHomeLinkHandler : IHttpHandler
    {
        HttpContext context;
        SettingLinks settinglink = new SettingLinks(UserInfo.Account);
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            this.context = context;
            var op = this.context.Request["op"];
           

            switch (op)
            {
                case "save":
                    {
                        SaveSort();
                    } break;
                case "commonLinks":
                    {
                        ResponseCommonLinks();
                    }; break;
                case "linkConfig":
                    {
                        ResponseConfigLinks();
                    }; break;

                case "SyncLinks":
                    {
                        SyncLinks();
                    } break;
                case "InfoAccessAdd":
                    {
                        InfoAccessAdd();
                    };break;
            }
        }
        InforAccessService infoAccessService = new InforAccessService();
        UserService userService = new UserService();
        NewsService dynamicService = new NewsService();
        public void SaveSort()
        {
            PageLinkUserConfigService configService = new PageLinkUserConfigService();

            var account = UserInfo.Account;
            var currentUser = new UserAuthorizationBLL().GetUserByAccount(account);
            if (currentUser != null)
            {
                //先移除配置
                // Type=99 表示是首页显示的混合类别图标
                configService.GenericService.Delete(p => p.UserID == currentUser.ID && p.Type == 99);

                var selectedIds = context.Request["selectedIds"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < selectedIds.Length; i++)
                {
                    var id = selectedIds[i];

                    configService.GenericService.Add(new PageLinkUserConfig()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LinkID = id,
                        UserID = currentUser.ID,
                        OrderNum = i + 1,
                        Type = 99,
                        IsEnable = true,
                        IsShowIndex = true
                    });
                }

                //默认默认显示为常用链接的数据
                int count = 0;
                var pagelinksSettings = settinglink.GetSettingsValue(SettingLinks.SettingsKeyEnum.pagelink, out count);
                if (String.IsNullOrEmpty(pagelinksSettings)) pagelinksSettings = "[]";
                var pageLinks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonLink>>(pagelinksSettings);

                var defaultCommonLinks = pageLinks.FindAll(p => p.IsCommonLink);
                for (int i = defaultCommonLinks.Count - 1; i >= 0; i--)
                {
                    var item = defaultCommonLinks[i];

                    //不包含，表示被排除的项
                    if (!selectedIds.Contains(item.ID))
                    {
                        configService.GenericService.Add(new PageLinkUserConfig()
                        {
                            ID = Guid.NewGuid().ToString(),
                            LinkID = item.ID,
                            UserID = currentUser.ID,
                            OrderNum = 99,
                            Type = 99,
                            IsEnable = true,
                            IsShowIndex = false //设置为非常用链接
                        });
                    }
                }

                configService.GenericService.Save();

                //同步当前用户配置数据
                SyncLinks();
            }
        }
        /// <summary>
        /// 存储同步常用链接
        /// </summary>
        private void SyncLinks()
        {
            var syncIntervalMillisecond = getSyncIntervalMillisecond();
            settinglink.SyncLinks(syncIntervalMillisecond);
        }


        private void ResponseCommonLinks()
        {
            int count = 0;
            string LinksValue = settinglink.GetSettingsValue(SettingLinks.SettingsKeyEnum.commonlink, out count);
            context.Response.Write(LinksValue);
        }

        private int getSyncIntervalMillisecond()
        {
            var syncIntervalMillisecondStr = context.Request["syncIntervalMillisecond"];
            var syncIntervalMillisecond = 0;
            if (!String.IsNullOrEmpty(syncIntervalMillisecondStr))
            {
                syncIntervalMillisecond = int.Parse(syncIntervalMillisecondStr);
            }

            return syncIntervalMillisecond;
        }

        private List<CommonLink> getCommonLinks()
        {
            var count = 0;
            var linksJson = settinglink.GetSettingsValue(SettingLinks.SettingsKeyEnum.commonlink, out count);
            var links = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonLink>>(linksJson);

            return links;
        }


        private List<CommonLink> getCommonConfigLinks()
        {
            var count = 0;
            var linksJson = settinglink.GetSettingsValue(SettingLinks.SettingsKeyEnum.pagelink, out count);
            var links = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonLink>>(linksJson);

            return links;
        }

        private void ResponseConfigLinks()
        {
            var commonConfigLinks = getCommonConfigLinks();
            var commonLinks = getCommonLinks();

            var item = new { CommonConfigLinks = commonConfigLinks, CommonLinks = commonLinks, RecommandLinks = "" };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            context.Response.Write(json);
        }
        private void InfoAccessAdd()
        {
            string ContentId = context.Request["ContentId"] == null ? string.Empty : context.Request["ContentId"];
            string AttachName = context.Request["attachName"] == null ? string.Empty : context.Request["attachName"];
            var model = dynamicService.GenericService.GetModel(ContentId);
            InforAccess info = new InforAccess();
            info.Title = model.Title;
            info.AccessName = Utils.CurrentUser.DisplayName;
            string account = UserInfo.Account;
            info.AccessDepartment = userService.GenericService.GetAll(p => p.Account == account).FirstOrDefault().OrganizationName;
            info.CategoryID = model.CategoryID;
            info.AttachName = "";
            info.Type = 2;
            info.AttachName = AttachName;
            infoAccessService.GenericService.Add(info);
            infoAccessService.GenericService.Save();
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