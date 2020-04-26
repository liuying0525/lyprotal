using DZAFCPortal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class OnlineVoteOptionsService : BizGenericService<OnlineVoteOptions>
    {
        /// <summary>
        /// 投票选项数添加
        /// </summary>
        /// <param name="Num">投票数</param>
        /// <param name="option">选项</param>
        /// <param name="guid">选项ID</param>
        /// <returns></returns>
        public DZAFCPortal.Entity.OnlineVoteOptions CreateNewPrefer(int Num, string option, string guid)
        {
            DZAFCPortal.Entity.OnlineVoteOptions op = new DZAFCPortal.Entity.OnlineVoteOptions();
            op = GenericService.GetAll().Where(p => p.NY_OnlineVoteID == guid).Where(p => p.Option == option).FirstOrDefault();
            op.ReviewsNum = Num + 1;
            return op;
        }
    }
}
