using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Authorization.BLL
{
    public class OperationUrlBLL
    {
        private OperationUrlSerivce service = new OperationUrlSerivce();

        /// <summary>
        /// 根据筛选条件获取所有的URL
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<OperationUrl> GetAllUrl(System.Linq.Expressions.Expression<Func<OperationUrl, bool>> predicate)
        {
            var urls = service.GenericService.GetAll(predicate);
            if (urls == null || urls.Count() == 0)
                return new List<OperationUrl>();

            return urls.ToList();
        }


        /// <summary>
        /// 删除所有url
        /// </summary>
        public void RemoveAll(string OpID)
        {
            var all = GetAllUrl(u => u.OperationID == OpID);
            all.ForEach(u =>
            {
                service.GenericService.Delete(u);
                service.GenericService.Save();
            });
        }

        /// <summary>
        /// 添加url
        /// </summary>
        /// <param name="model"></param>
        public void Add(OperationUrl model)
        {
            service.GenericService.Add(model);
            service.GenericService.Save();

        }

        

    }
}
