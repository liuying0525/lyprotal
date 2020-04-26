using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DZAFCPortal.ViewModel
{
    /// <summary>
    /// ViewModel与Model之间的转换 added by zhanxl
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IGenerateModel<T> where T : NySoftland.Core.BaseEntity
    {
        /// <summary>
        /// 转换成对应的Entity
        /// </summary>
        /// <returns></returns>
        T ToEntity();

        /// <summary>
        /// 初始化View model
        /// </summary>
        /// <param name="entity"></param>
        void Instance(T entity);

    }
}
