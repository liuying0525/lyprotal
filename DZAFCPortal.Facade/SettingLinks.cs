using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Web;
using DZAFCPortal.Config;
using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DZAFCPortal.Facade
{
    public class SettingLinks
    {
        CommonLink homelink = new CommonLink();
        CommonLinkService homelinkService = new CommonLinkService();
        PageLinkUserConfigService pageLinkUserService = new PageLinkUserConfigService();
        DBHelper dbhelp = new DBHelper(AppSettings.NYPortalConn);
        private int syncIntervalMillisecond = 0;
        //用户账号
        private string userAccount;

        public SettingLinks(string userAccount)
        {
            this.userAccount = userAccount;
        }


        /// <summary>
        /// 同步链接
        /// 
        /// 存储的链接为用户有权限访问的且通过用户自定义的后的数据(包括排序，可见性等信息)
        /// </summary>
        /// <param name="isCommonLinks">
        ///    是否为常用链接的图标，常用链接会显示在首页
        ///    如果为 true，则获取常用链接中用户配置的图标信息
        /// </param>
        public void SyncLinks(int syncIntervalMillisecond)
        {
            this.syncIntervalMillisecond = syncIntervalMillisecond;

            //同步所有链接
            SyncAllLinks();


            //同步当前链接
            SyncCommonLinks();
        }


        public void UpdateLinks(string[] selectedIds)
        {

        }

        private void SyncAllLinks()
        {
            var keyName = SettingsKeyEnum.pagelink;

            if (IsAllowUpdate(keyName, syncIntervalMillisecond))
            {

                //从所有有权限访问的链接中，查找可设置为常用链接的图标
                var links = GetAllLinks().ToList();

                var linksJson = Newtonsoft.Json.JsonConvert.SerializeObject(links);

                AddOrUpdate(keyName, linksJson, links.Count());
            }
        }


        public void SyncCommonLinks()
        {
            var keyName = SettingsKeyEnum.commonlink;

            if (!IsAllowUpdate(keyName, syncIntervalMillisecond))
            {
                return;
            }

            var allLinksInDb = homelinkService.GenericService.GetAll().ToList();

            var links = new List<CommonLink>();
            //判断用户是否存在，如果不在提取空数据
            // 存在则判断勾选的用户|组织是否为当前用户(所属组织)
            if (currentUser != null)
            {
                //默认默认显示为常用链接的数据
                int count = 0;
                var pagelinksSettings = GetSettingsValue(SettingsKeyEnum.pagelink, out count);
                if (String.IsNullOrEmpty(pagelinksSettings)) pagelinksSettings = "[]";
                var pageLinks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommonLink>>(pagelinksSettings);
                var defaultCommonLinks = pageLinks.FindAll(p => p.IsCommonLink);

                var commonSettings = GetSettingsValue(keyName, out count);
                //从默认后台配置的显示在常用链接的数据项配置
                if (String.IsNullOrEmpty(commonSettings))
                {
                    links.Clear();

                    //将获取的数据添加到配置
                    //先移除配置
                    // Type=99 表示是首页显示的混合类别图标
                    pageLinkUserService.GenericService.Delete(p => p.UserID == currentUser.ID && p.Type == 99);

                    //将默认的数据项添加到用户配置中
                    var tempItems = defaultCommonLinks.OrderBy(p => p.OrderNum).ToList();
                    for (var i = 0; i < tempItems.Count(); i++)
                    {
                        var id = tempItems[i].ID;
                        pageLinkUserService.GenericService.Add(new PageLinkUserConfig()
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

                    pageLinkUserService.GenericService.Save();

                    //设置返回的数据源为默认显示在公共链接的数据
                    links = defaultCommonLinks;
                }
                //通过配置来取
                else
                {
                    links.Clear();

                    //获取用户配置的列表
                    var configItems = pageLinkUserService.GenericService.GetAll(p => p.UserID == currentUser.ID && p.Type == 99).ToList();
                    foreach (var config in configItems)
                    {
                        var link = allLinksInDb.FirstOrDefault(p => p.ID == config.LinkID);
                        if (link != null && link.IsEnable)
                        {
                            link.OrderNum = config.OrderNum;
                            link.IsEnable = config.IsEnable;
                            link.IsCommonLink = config.IsShowIndex;

                            links.Add(link);
                        }
                    }

                    var linkIds = links.Select(p => p.ID).ToList();

                    //将用户配置的数据与默认为常用链接的数据对比
                    //如果不存在于配置中，则添加到配置
                    foreach (var item in defaultCommonLinks)
                    {
                        if (!linkIds.Contains(item.ID))
                        {
                            links.Add(item);
                        }
                    }
                }
            }

            foreach (var item in links)
            {
                item.Url = CommonLinkService.PCFormatUrl(item.Url);
                item.PadUrl = CommonLinkService.PadFormatUrl(item.PadUrl);
                item.PhoneUrl = CommonLinkService.PhoneFormatUrl(item.PhoneUrl);
            }

            var items = links.FindAll(p => p.IsCommonLink).OrderBy(p => p.OrderNum).ToList();
            var linksJson = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            AddOrUpdate(keyName, linksJson, items.Count());
        }


        /// 获取用户有权限访问的所有链接
        /// </summary>
        /// <returns></returns>
        public List<CommonLink> GetAllLinks()
        {
            var links = new List<CommonLink>();
            //判断用户是否存在，如果不在直接取AppType=0(全部)的数据
            // 存在则判断勾选的用户|组织是否为当前用户(所属组织)
            if (currentUser != null)
            {
                var userOrg = currentUser.OrganizationID == null ? Guid.Empty.ToString() : currentUser.OrganizationID;

                //条件1：应用范围为所有人|指定觉角色的
                Expression<Func<CommonLink, bool>> predice = p => p.IsEnable && (p.AppalyType == 0 || p.AppalyType == 2);
                links = homelinkService.GenericService.GetAll(predice).ToList();

                //用户所拥有的角色ID数组
                var userRoles = (from p in new RoleUserService().GenericService.GetAll(p => p.UserID == currentUser.ID)
                                 select p.RoleID.ToString()).ToArray();

                for (int i = links.Count - 1; i >= 0; i--)
                {
                    var item = links[i];

                    if (item.AppalyType == 2)
                    {
                        //表示当前用户的角色无法访问该链接，移除链接项
                        if (!IsLinkInRole(userRoles, item.AppalyRoles))
                        {
                            links.RemoveAt(i);
                            continue;
                        }
                    }

                    item.Url = CommonLinkService.PCFormatUrl(item.Url);
                    item.PadUrl = CommonLinkService.PadFormatUrl(item.PadUrl);
                    item.PhoneUrl = CommonLinkService.PhoneFormatUrl(item.PhoneUrl);
                }
            }
            else
            {
                links = homelinkService.GenericService.GetAll(p => p.IsEnable && p.AppalyType == 0).ToList();

                links.ForEach(p =>
                {
                    p.Url = CommonLinkService.PCFormatUrl(p.Url);
                    p.PadUrl = CommonLinkService.PadFormatUrl(p.PadUrl);
                    p.PhoneUrl = CommonLinkService.PhoneFormatUrl(p.PhoneUrl);
                });
            }

            var items = links.OrderBy(p => p.OrderNum).ToList();

            return items;
        }

        /// <summary>
        /// 通过更新间隔来判断当前数据是否需要更新
        /// 只有大于间隔时间才会更新
        /// </summary>
        /// <param name="settingsKey"></param>
        /// <param name="syncIntervalMillisecond"></param>
        /// <returns></returns>
        public bool IsAllowUpdate(SettingsKeyEnum settingsKey, int syncIntervalMillisecond)
        {
            if (syncIntervalMillisecond == 0)
            {
                //虽然是立即更新，但是为了防止快速刷新更新重复，设置为间隔2秒更新一次
                syncIntervalMillisecond = 2 * 1000;
            }

            var rs = true;

            var updateTime = GetUpdateTime(settingsKey);
            if (updateTime.HasValue)
            {
                if ((DateTime.Now - updateTime.Value).TotalMilliseconds < syncIntervalMillisecond)
                {
                    rs = false;
                }
            }

            return rs;
        }

        /// <summary>
        /// 判断当前链接是否为用户所拥有的角色范围内
        /// </summary>
        /// <param name="roleIds">用户所拥有的角色</param>
        /// <param name="applayRoles">当前Link可访问的角色</param>
        /// <returns></returns>
        private bool IsLinkInRole(string[] roleIds, string applayRoles)
        {
            string[] applayRoleIds = String.IsNullOrEmpty(applayRoles) ? new string[0] : applayRoles.Split(',');

            var count = (from p in applayRoleIds where roleIds.Contains(p) select p).Count();
            return count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingsKey"></param>
        /// <param name="valueJson"></param>
        /// <param name="count"></param>
        /// <param name="IntervalUpdateMilliseconds">更新的间隔时间(毫秒)</param>
        public void AddOrUpdate(SettingsKeyEnum settingsKey, string valueJson, int count)
        {
            string querySql = " SELECT * FROM  NY_Settings WHERE UserAccount=@UserAccount AND [Key]=@Key ";
            var parmers = new SqlParameter[]{
                    new SqlParameter("UserAccount",userAccount),
                    new SqlParameter("Key",settingsKey.ToString())
                };

            var isExsists = false;
            using (var reader = dbhelp.ExecuteReader(CommandType.Text, querySql, parmers))
            {
                if (reader.Read())
                {
                    isExsists = true;
                }
            }

            //表示存在，则更新数据
            if (isExsists)
            {
                var updateSql = String.Format(" UPDATE NY_Settings SET [Value]='{0}',Count={1},UpdateTime='{2}'  WHERE UserAccount=@UserAccount AND [Key]=@Key", valueJson, count, DateTime.Now.ToString());
                dbhelp.ExecuteNonQuery(CommandType.Text, updateSql, parmers);
            }
            else
            {
                var insertSql = String.Format(" INSERT INTO NY_Settings VALUES(@UserAccount,@Key,'{0}',{1},'{2}' ) ", valueJson, count, DateTime.Now.ToString());
                dbhelp.ExecuteNonQuery(CommandType.Text, insertSql, parmers);
            }
        }


        private DateTime? GetUpdateTime(SettingsKeyEnum settingsKey)
        {
            string querySql = " SELECT * FROM  NY_Settings WHERE UserAccount=@UserAccount AND [Key]=@Key ";
            var parmers = new SqlParameter[]{
                    new SqlParameter("UserAccount",userAccount),
                    new SqlParameter("Key",settingsKey.ToString())
                };

            var updateTime = String.Empty;
            using (var reader = dbhelp.ExecuteReader(CommandType.Text, querySql, parmers))
            {
                if (reader.Read())
                {
                    updateTime = reader["UpdateTime"].ToString();
                }
            }

            if (String.IsNullOrEmpty(updateTime))
            {
                return null;
            }
            else
                return DateTime.Parse(updateTime);
        }


        public string GetSettingsValue(SettingsKeyEnum settingsKey, out int count)
        {
            string value = String.Empty;
            count = 0;

            string querySql = " SELECT * FROM  NY_Settings WHERE UserAccount=@UserAccount AND [Key]=@Key ";
            var parmers = new SqlParameter[]{
                    new SqlParameter("UserAccount",userAccount),
                    new SqlParameter("Key",settingsKey.ToString())
                };

            var reader = dbhelp.ExecuteReader(CommandType.Text, querySql, parmers);
            if (reader.Read())
            {
                count = int.Parse(reader["Count"].ToString());
                value = reader["Value"].ToString();
            }

            return value;
        }

        public User currentUser
        {
            get
            {
                var user = new UserAuthorizationBLL().GetUserByAccount(UserInfo.Account);


                return user;
            }
        }
        public enum SettingsKeyEnum
        {
            /// <summary>
            /// 当前用户所有有权限查看的图标
            /// </summary>
            pagelink,
            /// <summary>
            /// 常用链接的图标
            /// </summary>
            commonlink,
            /// <summary>
            /// 可以参与配置到常用链接的图标
            /// </summary>
            commonconfiglink,
        }
    }
}
