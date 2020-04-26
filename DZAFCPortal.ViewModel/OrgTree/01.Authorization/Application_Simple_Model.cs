using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应Application实体,模型很简单,包含ID,Name
    /// 适用范围:模块管理_编辑
    /// </summary>
    public class Application_Simple_Model : IGenerateModel<Applications>
    {
        /// <summary>
        /// 当前Application ID
        /// </summary>
        public string ID { get; set; }


        /// <summary>
        /// 当前Application Name
        /// </summary>
        public string Name { get; set; }

        #region IGenerateModel<Applications> 成员

        public Applications ToEntity()
        {
            return new Applications
            {
                ID = this.ID,
                Name = this.Name
            };
        }

        public void Instance(Applications entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
        }

        #endregion
    }
}
