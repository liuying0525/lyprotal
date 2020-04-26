using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应Module实体
    /// 适用范围:模块管理_编辑
    /// </summary>
    public class Module_Edit_Model : IGenerateModel<Module>
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
        public string OrderNum { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 是否启用，默认值true
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除,默认值false
        /// </summary>
        public bool IsDelete { get; set; }


        /// <summary>
        /// Module Group ID
        /// </summary>
        public string ModuleGroup_ID { get; set; }


        /// <summary>
        /// Application ID
        /// </summary>
        public string ApplicationID { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        ///// <summary>
        ///// Application Name
        ///// </summary>
        //public string ApplicationName { get; set; }


        /// <summary>
        /// 判断Module or Module Group
        /// </summary>
        public bool IsModuleGroupType { get; set; }
        #endregion

        #region IGenerateModel<Module> 成员

        public Module ToEntity()
        {
            return new Module
            {
                ID = this.ID,
                Name = this.Name,
                Url = this.Url,
                OrderNum = Int32.Parse(this.OrderNum),
                Code = this.Code,
                IsEnable = this.IsEnable,
                IsDelete = this.IsDelete,
                ModuleGroup_ID =this.ModuleGroup_ID,
                ApplicationID = this.ApplicationID,
            };
        }

        public void Instance(Module entity)
        {
            this.ID = entity.ID.ToString();
            this.Name = entity.Name;
            this.Url = entity.Url;
            this.OrderNum = entity.OrderNum.ToString();
            this.Code = entity.Code;
            this.IsEnable = entity.IsEnable;
            this.IsDelete = entity.IsDelete;
            this.ModuleGroup_ID = entity.ModuleGroup_ID.ToString();
            this.ApplicationID = entity.ApplicationID.ToString();
            //this.ApplicationName = entity.Application.Name;

            this.IsModuleGroupType = false;
        }

        #endregion
    }
}
