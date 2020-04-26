using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class BnDictBLL
    {
        BnDictService dictService = new BnDictService();
        BnDictTypeService dictTypeService = new BnDictTypeService();

        /// <summary>
        /// 通过大类的 Code 获取词典列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<BnDict> GetBnDictsByCode(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException("Code 参数不能为空");

            var dictType = dictTypeService.GenericService.FirstOrDefault(p => p.Code == code);
            if (dictType != null)
            {
                return dictService.GenericService.GetAll(p => p.BnDictTypeID == dictType.ID).ToList();
            }

            return new List<BnDict>();
        }
    }
}
