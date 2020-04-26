using DZAFCPortal.Entity;
using DZAFCPortal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.ViewModel.Client
{
    /// <summary>
    /// 我的同事圈列表页视图
    /// </summary>
    public class FriendNewsReplyModel : IGenerateModel<FriendNewsReply>
    {

        #region 成员属性
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
        /// 发布的内容
        /// </summary>
        public string ReplyContent { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string CreateTime { get; set; }
        #endregion

        public FriendNewsReply ToEntity()
        {
            throw new NotImplementedException();
        }

        public void Instance(FriendNewsReply entity)
        {
            //获取用户信息
            var user = new DZAFCPortal.Authorization.BLL.UserAuthorizationBLL().GetUserByAccount(entity.UserAccount);

            this.ID = entity.ID;
            this.UserAccount = entity.UserAccount;

            if (user == null)
            {
                this.HeadPhoto = "/TempPic/bea_person05.png";
                this.UserDisplayName = entity.UserAccount;
            }
            else
            {
                this.HeadPhoto = "/TempPic/bea_person05.png";
                this.UserDisplayName = user.DisplayName;
            }

            this.ReplyContent = entity.ReplyContent;
            this.CreateTime = entity.CreateTime.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
