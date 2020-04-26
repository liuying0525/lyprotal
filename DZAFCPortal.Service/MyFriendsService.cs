using DZAFCPortal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class MyFriendsService : BizGenericService<MyFriends>
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public List<MyFriends> GetMyFriends(string userAccount)
        {
            return GenericService.GetAll(p => p.UserAccount == userAccount&&p.ApplyState==99).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// 获取登录用户 待审批的好友
        /// </summary>
        /// <returns></returns>
        public List<MyFriends> GetFriendPendingReply(string userAccount)
        {
            // 用户待审批好友 

            return GenericService.GetAll(p => p.MyFriendAccount == userAccount && p.ApplyState == 0).ToList();
        }

        public void SureRendingReply(Guid itemId)
        {
            var item = GenericService.GetModel(itemId);
            if (item == null) throw new Exception("用户不存在，或已被删除！");

            item.ApplyState = 99;

            var newItem = new DZAFCPortal.Entity.MyFriends();
            newItem.ID = Guid.NewGuid().ToString();
            newItem.UserAccount = item.MyFriendAccount;
            newItem.MyFriendAccount = item.UserAccount;
            newItem.ApplyState = 99;

            //判断用户是否在另一用户列表中
            var _temp = GenericService.GetAll(p => p.UserAccount == newItem.UserAccount && p.MyFriendAccount == newItem.MyFriendAccount).FirstOrDefault();
            if (_temp != null)
            {
                _temp.ApplyState = 99;
                GenericService.Update(_temp);
            }
            else
            {
                GenericService.Add(newItem);
            }

            GenericService.Save();
        }

        public void IgnorRendingReply(Guid itemId)
        {
            var item = GenericService.GetModel(itemId);
            if (item == null) throw new Exception("用户不存在，或已被删除！");

            item.ApplyState = -99;

            GenericService.Update(item);
            GenericService.Save();
        }
    }
}
