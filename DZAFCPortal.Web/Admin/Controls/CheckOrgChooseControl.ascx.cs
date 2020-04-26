using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class CheckOrgChooseControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region--------页面控件ID----------------
        private string containerID = "container_" + Guid.NewGuid().ToString().Replace('-', '_');
        /// <summary>
        /// 容器ID
        /// </summary>
        public string ContainerID
        {
            get
            {
                return containerID;
            }
        }

        private string ztreeID = "ztree_" + Guid.NewGuid().ToString().Replace('-', '_');
        /// <summary>
        /// 树控件ID
        /// </summary>
        protected string ZtreeID
        {
            get
            {
                return ztreeID;
            }
        }

        private string layerContainerID = "layer_" + Guid.NewGuid().ToString().Replace('-', '_');

        /// <summary>
        /// 
        /// </summary>
        protected string LayerContainerID
        {
            get
            {
                return layerContainerID;
            }
        }
        #endregion

        /// <summary>
        /// 组织树根节点，如果不指定，则取全部节点
        /// </summary>
        public string OrgRootID { get; set; }

        #region--------选中的用户取值、赋值--------

        /// <summary>
        /// 选中的组织机构ID，多项以 , 隔开
        /// </summary>
        public string CheckedOrgIds
        {
            get
            {
                return hfdCheckedOrgIds.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedOrgIds.Value = value;
            }
        }


        /// <summary>
        /// 选中的组织机构ID，多项以 , 隔开
        /// </summary>
        public string CheckedOrgNames
        {
            get
            {
                return hfdCheckedOrgNames.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedOrgNames.Value = value;
            }
        }


        /// <summary>
        /// 选中的组织机构路径，多项以,隔开
        /// 用于判断是否选中的整个组织以及子级 
        /// </summary>
        public string CheckedOrgPaths
        {
            get
            {
                return hfdCheckedOrgPaths.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedOrgPaths.Value = value;
            }
        }

        /// <summary>
        /// 选中的用户ID，多项以 , 隔开
        /// </summary>
        public string CheckedUserIds
        {
            get
            {
                return hfdCheckedUserIds.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedUserIds.Value = value;
            }
        }


        public string chosenDeptName
        {
            get
            {
                return hfdchosenDeptName.Value.TrimEnd(',');
            }
            set
            {
                hfdchosenDeptName.Value = value;
            }
        }

        #endregion

        #region----------对外公布存储值的控件在页面的ID-------------
        /// <summary>
        /// 存储选中的组织机构ID的隐藏控件
        /// </summary>
        public string hfdCheckedOrgIdsClientID
        {
            get
            {
                return hfdCheckedOrgIds.ClientID;
            }
        }

        /// <summary>
        /// 存储选中组织Path的隐藏控件
        /// </summary>
        public string hfdCheckedOrgPathsClientID
        {
            get
            {
                return hfdCheckedOrgPaths.ClientID;
            }
        }

        /// <summary>
        /// 存储选中的用户ID的隐藏控件
        /// </summary>
        public string hfdCheckedUserIdsClientID
        {
            get
            {
                return hfdCheckedUserIds.ClientID;
            }
        }

        #endregion
    }
}