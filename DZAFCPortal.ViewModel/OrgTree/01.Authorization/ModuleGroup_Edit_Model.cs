using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应ModuleGroup实体
    /// 适用范围:模块管理_编辑
    /// </summary>
    public class ModuleGroup_Edit_Model : IGenerateModel<ModuleGroup>
    {
        #region Properties
        /// <summary>
        /// GUID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// ModuleGroup Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 判断Module or Module Group
        /// </summary>
        public bool IsModuleGroupType { get; set; }

        /// <summary>
        /// 当前ModuleGroup关联的Module List
        /// </summary>
        public List<Module_Edit_Model> ModulelLst { get; set; }

        #endregion

        #region IGenerateModel<ModuleGroup> 成员

        public ModuleGroup ToEntity()
        {
            return new ModuleGroup
            {
                ID = this.ID,
                Name = this.Name,
                Icon = string.IsNullOrEmpty(this.Icon) ? "ico" : this.Icon,
                OrderNum = this.OrderNum,
                Summary = this.Summary
            };
        }

        public void Instance(ModuleGroup entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.Icon = entity.Icon;
            this.OrderNum = entity.OrderNum;
            this.Summary = entity.Summary;
            this.IsModuleGroupType = true;
        }

        #endregion
    }
}
