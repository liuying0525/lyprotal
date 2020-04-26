using DZAFCPortal.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service.CMS
{
    public class AttachService : BizGenericService<Attach>
    {
        /// <summary>
        /// 获取内容下的所有附件
        /// </summary>
        /// <param name="contentID">内容主体ID</param>
        /// <param name="type">类型 一般在添加时会为各分类的添加类别</param>
        /// <returns></returns>
        public List<Attach> GetContentAttachs(string contentID, int type)
        {
            return GenericService.GetAll(p => p.ContentID == contentID && p.Type == type.ToString()).ToList();
        }
    }
}
