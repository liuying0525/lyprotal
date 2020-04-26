/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/9/23 15:29:44
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using DZAFCPortal.Config;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Console
{
    public class Sync2Portal
    {
        static NySoftland.Core.GenericService<User> user_srv = new UserService().GenericService;
        static NySoftland.Core.GenericService<Organization> org_srv = new OrganizationService().GenericService;

        static List<org_vm> all_orgs = new List<org_vm>();
        static string access_token;

        /// <summary>
        /// 初始化同步状态为false
        /// </summary>
        private static void InitSyncStatus()
        {
            user_srv.GetAll().ToList().ForEach(u =>
            {
                u.IsOnSynchronize = false;
            });

            org_srv.GetAll().ToList().ForEach(o =>
            {
                o.IsOnSynchronize = false;
            });

            user_srv.Save();
            org_srv.Save();
        }

        private static void RemoveUnSyncItems()
        {

            user_srv.GetAll(u => !u.IsOnSynchronize).ToList().ForEach(u =>
              {
                  user_srv.Delete(u);
              });

            org_srv.GetAll(o => !o.IsOnSynchronize).ToList().ForEach(o =>
              {
                  org_srv.Delete(o);
              });

            user_srv.Save();
            org_srv.Save();
        }

        public static void Process()
        {

            InitSyncStatus();

            access_token = GetApplicationAccessToken();
            var org_req_url = GenerateOrgRequestUrl(access_token, AppSettings.RootExternalID, true);
            var json_orgs = HttpRequestHelper.SendHttpRequest(org_req_url);

            var org_result = JsonConvert.DeserializeObject<org_full_vm>(json_orgs);
            if (org_result.errorNumber != 0)
            {

                System.Console.WriteLine("组织源获取异常，开始输出错误信息：");
                org_result.errors.ForEach(e =>
                {
                    System.Console.WriteLine(e);
                });

            }
            else
            {
                all_orgs = org_result.organizationUnitList;
                var root_org = all_orgs.FirstOrDefault(o => o.externalId == AppSettings.RootExternalID && o.parentDirectory.Trim() == "/" && string.IsNullOrEmpty(o.parentExternalId));

                ProcessOrgs(root_org, "", "", "");

            }

            RemoveUnSyncItems();
        }

        public static void ProcessOrgs(org_vm current_org,
                                       string parent_uuid,
                                       string parent_uuid_path,
                                       string parent_name_path)
        {
            System.Console.WriteLine($"当前同步组织:{current_org.name}");

            var current_org_uuid = current_org.uuid.Trim();
            var current_org_name = current_org.name.Trim();

            var uuid_path = $"{parent_uuid_path}/{current_org_uuid}";
            var name_path = $"{parent_name_path}/{current_org_name}";

            var exist_org = org_srv.FirstOrDefault(o => o.ID == current_org_uuid);
            if (exist_org == null)
            {
                var org_new = new Organization
                {
                    ID = current_org_uuid,
                    Name = current_org_name,
                    IsVirtual = false,
                    ParentID = parent_uuid,
                    IsEnable = true,
                    IsDelete = false,
                    CreateTime = current_org.createTime,
                    ModifyTime = current_org.lastModifyTime,
                    Path = uuid_path,
                    PathName = name_path,

                    Code = "ORG",

                    IsOnSynchronize = true,
                    IsShow = true,
                    SortNum = 0,
                    OrganizationType = current_org.type,
                    RegionID = current_org.regionId,
                    ExtAttributes = current_org.attributes.ToString(),
                    DeptManagerIDs = string.Join(";", current_org.managerUDAccountUuids),
                    ExternalID = current_org.externalId,
                    ParentExternalID = current_org.parentExternalId,
                    LevelNumber = current_org.levelNumber
                };
                org_srv.Add(org_new);
                org_srv.Save();
            }
            else
            {
                exist_org.Name = current_org_name;
                exist_org.ParentID = parent_uuid;
                exist_org.CreateTime = current_org.createTime;
                exist_org.ModifyTime = current_org.lastModifyTime;
                exist_org.Path = uuid_path;
                exist_org.PathName = name_path;
                exist_org.IsOnSynchronize = true;
                exist_org.OrganizationType = current_org.type;
                exist_org.RegionID = current_org.regionId;
                exist_org.ExtAttributes = current_org.attributes.ToString();
                exist_org.DeptManagerIDs = string.Join(";", current_org.managerUDAccountUuids);
                exist_org.ExternalID = current_org.externalId;
                exist_org.ParentExternalID = current_org.parentExternalId;
                exist_org.LevelNumber = current_org.levelNumber;

                org_srv.Update(exist_org);
                org_srv.Save();
            }

            #region get orgs

            var child_orgs = all_orgs.Where(o => o.parentExternalId == current_org.externalId).ToList();
            child_orgs.ForEach(o =>
            {
                ProcessOrgs(o, current_org_uuid, uuid_path, name_path);
            });

            #endregion

            #region get users
            var user_req_url = GenerateUserRequestUrl(access_token, current_org_uuid);
            var json_users = HttpRequestHelper.SendHttpRequest(user_req_url);
            var user_result = JsonConvert.DeserializeObject<user_full_vm>(json_users);

            if (user_result.errorNumber != 0)
            {

                System.Console.WriteLine($"用户源(parent ou:{current_org_name}|{current_org_uuid})获取异常，开始输出错误信息：");
                user_result.errors.ForEach(e =>
                {
                    System.Console.WriteLine(e);
                });

            }
            else
            {
                var all_users = new List<user_vm>(user_result.systemAccountLists);

                int left_count = user_result.total / user_result.limit;//剩余取数次数
                int last_page_size = user_result.total % user_result.limit;//最后一次取数的pagesize

                var current_page_index = user_result.start + user_result.limit;

                var page_size = user_result.limit;

                for (int i = 0; i < left_count; i++)
                {
                    if (i == left_count - 1)
                    {
                        page_size = last_page_size;
                    }

                    var req_next_page = GenerateUserRequestUrl(access_token, current_org_uuid, current_page_index, page_size);
                    var json_next_page = HttpRequestHelper.SendHttpRequest(req_next_page);
                    var result_next_page = JsonConvert.DeserializeObject<user_full_vm>(json_next_page);
                    if (result_next_page.errorNumber == 0)
                    {
                        all_users.AddRange(result_next_page.systemAccountLists);
                    }

                    current_page_index += result_next_page.limit;
                }



                all_users.ForEach(u =>
                {
                    System.Console.WriteLine($"当前同步用户:{u.username}");

                    var current_user_uuid = u.uuid.Trim();
                    var current_user_account = u.username.Trim();

                    var exist_user = user_srv.FirstOrDefault(o => o.ID == current_user_uuid);
                    if (exist_user == null)
                    {
                        var user_new = new User
                        {

                            Gender = 1,
                            SortNum = 0,
                            IsShow = true,
                            IsOnSynchronize = true,

                            ID = current_user_uuid,
                            Account = current_user_account,
                            DisplayName = u.displayName,
                            MobilePhone = u.phoneNumber,
                            Email = u.email,
                            Locked = u.locked,
                            ExternalID = u.externalId,
                            OrganizationExtID = u.ouId,
                            CreateTime = u.createTime,
                            OrgPath = uuid_path,
                            OrgPathName = name_path,

                            OfficePhone = u.attributes.ext_phone,
                            Title = u.attributes.ext_position

                            // OfficePhone

                        };
                        user_srv.Add(user_new);
                        user_srv.Save();
                    }
                    else
                    {
                        exist_user.IsOnSynchronize = true;
                        exist_user.Account = current_user_account;
                        exist_user.DisplayName = u.displayName;
                        exist_user.MobilePhone = u.phoneNumber;
                        exist_user.Email = u.email;
                        exist_user.Locked = u.locked;
                        exist_user.ExternalID = u.externalId;
                        exist_user.OrganizationExtID = u.ouId;
                        exist_user.CreateTime = u.createTime;
                        exist_user.OrgPath = uuid_path;
                        exist_user.OrgPathName = name_path;

                        exist_user.OfficePhone = u.attributes.ext_phone;
                        exist_user.Title = u.attributes.ext_position;

                        user_srv.Update(exist_user);
                        user_srv.Save();
                    }


                });

            }

            #endregion


        }

        #region Generate Request Url
        private static string GenerateOrgRequestUrl(string access_token, string external_id = "", bool is_include_child = false)
        {
            string base_url = $"{AppSettings.SSOHost }{ConstValue.SSO.RETRIEVE_ORGANIZATION_URL}?access_token={access_token}&isIncludeChild={is_include_child.ToString().ToLower()}";
            if (!string.IsNullOrEmpty(external_id))
            {
                base_url += $"&id={external_id}";
            }

            return base_url;

        }


        public static string GenerateUserRequestUrl(string access_token,
                                                    string ou_uuid = "",
                                                    int page_index = 0,
                                                    int page_size = 50,
                                                    string user_name = "",
                                                    string created_start_date = "",
                                                    string created_end_date = "")
        {
            string base_url = $"{AppSettings.SSOHost }{ConstValue.SSO.RETRIEVE_USER_URL}?access_token={access_token}&start={page_index}&limit={page_size}";

            if (!string.IsNullOrEmpty(ou_uuid))
            {
                base_url += $"&ouUuid={ou_uuid}";
            }

            if (!string.IsNullOrEmpty(user_name))
            {
                base_url += $"&username={user_name}";
            }

            if (!string.IsNullOrEmpty(created_start_date))
            {
                base_url += $"&createStateDate={created_start_date}";
            }

            if (!string.IsNullOrEmpty(created_end_date))
            {
                base_url += $"&createEndDate={created_end_date}";
            }

            return base_url;

        }

        private static string GenerateAccTokenRequestUrl()
        {
            return $"{ AppSettings.SSOHost}{ConstValue.SSO.RETRIEVE_APP_ACCESS_TOKEN_URL}?client_id={AppSettings.SSOClientID}&client_secret={AppSettings.SSOClientSecret}&scope=read&grant_type=client_credentials";
        }
        #endregion

        /// <summary>
        /// 获取access token
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationAccessToken()
        {
            var token_req_url = GenerateAccTokenRequestUrl();
            var json_token = HttpRequestHelper.SendHttpRequest(token_req_url, "post");
            return JsonConvert.DeserializeObject<access_token_vm>(json_token).access_token;
        }

    }

    #region View Model

    #region user

    public class user_full_vm
    {
        public int errorNumber { get; set; }
        public List<string> errors { get; set; }
        public int start { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public List<user_vm> systemAccountLists { get; set; }
    }
    public class user_vm
    {
        public string uuid { get; set; }
        public DateTime createTime { get; set; }
        public bool archived { get; set; }
        public string username { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool locked { get; set; }
        public string externalId { get; set; }
        public string ouId { get; set; }
        public string ouDirectory { get; set; }
        public string displayName { get; set; }
        public user_extattr_vm attributes { get; set; }
    }

    public class user_extattr_vm
    {
        public string ext_dealerlevel { get; set; }
        public string ext_phone { get; set; }
        public string ext_address { get; set; }
        public string ext_companyname { get; set; }
        public string ext_position { get; set; }
        public string ext_dealercode { get; set; }
    }

    #endregion

    #region org
    public class org_full_vm
    {
        public int errorNumber { get; set; }
        public List<string> errors { get; set; }

        public List<org_vm> organizationUnitList { get; set; }


    }

    public class org_vm
    {

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string externalId { get; set; }
        public string parentDirectory { get; set; }
        public string parentExternalId { get; set; }
        public string type { get; set; }
        public string regionId { get; set; }
        public object attributes { get; set; }
        public bool rootNode { get; set; }
        public DateTime lastModifyTime { get; set; }
        public DateTime createTime { get; set; }
        public List<string> managerUDAccountUuids { get; set; }

        public string uuid { get; set; }

        public int levelNumber { get; set; }

    }

    public class org_extattr_vm
    {

    }
    #endregion

    #region access token

    public class access_token_vm
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }

    }

    #endregion

    #endregion


}
