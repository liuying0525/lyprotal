using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应Module实体
    /// 适用范围:操作管理列表页查看
    /// </summary>
    public class Module_Readonly_Model : IGenerateModel<Module>
    {
        #region Properties
        /// <summary>
        /// 当前Module ID
        /// </summary>
        public string ID { get; set; }


        /// <summary>
        /// 当前Module Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 当前Module 关联的Operation List
        /// </summary>
        public List<Operation_Readonly_Model> OperationLst { get; set; }

        ///// <summary>
        ///// 基本Operation List(增删查改)
        ///// </summary>
        //public List<Operation_Readonly_Model> BaseOperationLst { get; set; }
        #endregion

        #region IGenerateModel<Module> 成员

        public Module ToEntity()
        {
            return new Module
            {
                ID = this.ID,
                Name = this.Name,
                OrderNum = this.OrderNum,
                Code = this.Code
            };
        }

        public void Instance(Module entity)
        {
            this.ID = entity.ID.ToString();
            this.Name = entity.Name;
            this.OrderNum = entity.OrderNum;
            this.Code = entity.Code;

        }

        #endregion
    }
}
