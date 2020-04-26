using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.BLL;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应Operation实体
    /// 适用范围:列表页查看
    /// </summary>
    public class Operation_Readonly_Model : IGenerateModel<Operation>
    {
        #region Properties
        /// <summary>
        /// 当前Operation ID
        /// </summary>
        public string ID { get; set; }


        /// <summary>
        /// 当前Operation Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 所属Module ID
        /// </summary>
        public string ModuleID { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否允许被删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否基本操作
        /// </summary>
        //public bool IsBased { get; set; }


        /**********************************前端操作属性****************************************/
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 是否编辑状态
        /// </summary>
        //public bool IsEdit { get; set; }
        #endregion

        #region IGenerateModel<Operation> 成员


        public Operation ToEntity()
        {
            return new Operation
            {
                ID = this.ID,
                ModuleID = this.ModuleID,
                Name = this.Name,
                IsEnable = this.IsEnable,
                IsDelete = this.IsDelete,
                Code = this.Code,
                ControlID = this.Code,
                OrderNum = this.OrderNum//默认赋值1
            };
        }

        public Operation ToEntity(int ordnum)
        {
            return new Operation
            {
                ID = this.ID,
                ModuleID = this.ModuleID,
                Name = this.Name,
                IsEnable = true,
                IsDelete = false,
                Code = Fxm.Utility.PinYin.ConvertCh(this.Name).ToLower(),
                ControlID = Fxm.Utility.PinYin.ConvertCh(this.Name).ToLower(),
                OrderNum = ordnum//默认赋值1
            };
        }

        public void Instance(Operation entity)
        {
            this.ID = entity.ID.ToString();
            this.Name = entity.Name;
            this.ModuleID = entity.ModuleID.ToString();
            this.Code = entity.Code;
            this.IsEnable = entity.IsEnable;
            this.IsDelete = entity.IsDelete;
            this.OrderNum = entity.OrderNum;
        }

        #endregion

        public Operation_Readonly_Model()
        {
            IsChecked = false;
            //IsEdit = false;
        }

    }
}
