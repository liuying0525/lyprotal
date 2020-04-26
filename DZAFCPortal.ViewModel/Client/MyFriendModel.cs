using DZAFCPortal.Entity;
using DZAFCPortal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.ViewModel.Client
{
    public class MyFriendModel : IGenerateModel<MyFriends>
    {
        public string ID { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadPhoto { get; set; }

        /// <summary>
        /// 用户显示名
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 表示是否审
        /// </summary>
        public bool IsEnable { get; set; }

        public MyFriends ToEntity()
        {
            throw new NotImplementedException();
        }

        public void Instance(MyFriends entity)
        {
            this.ID = entity.ID;
            this.UserAccount = entity.MyFriendAccount;

            var user = new DZAFCPortal.Authorization.BLL.UserAuthorizationBLL().GetUserByAccount(this.UserAccount);
            if (user == null)
            {
                this.HeadPhoto = "/TempPic/bea_person05.png";
                this.UserDisplayName = this.UserAccount + "(不存在)";
            }
            else
            {
                this.HeadPhoto = "/TempPic/bea_person05.png";
                this.UserDisplayName = user.DisplayName;
            }
        }

        public void Instance1(MyFriends entity)
        {
            this.ID = entity.ID;
            this.UserAccount = entity.UserAccount;

            var user = new DZAFCPortal.Authorization.BLL.UserAuthorizationBLL().GetUserByAccount(entity.UserAccount);
            if (user == null)
            {
                this.HeadPhoto = "/TempPic/bea_person05.png";
                this.UserDisplayName = this.UserAccount + "(不存在)";
            }
            else
            {
                this.HeadPhoto = "/TempPic/bea_person05.png";
                this.UserDisplayName = user.DisplayName;
            }
        }
    }
}
