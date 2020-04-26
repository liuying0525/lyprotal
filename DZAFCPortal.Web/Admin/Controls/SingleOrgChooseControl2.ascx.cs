﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class SingleOrgChooseControl2 : System.Web.UI.UserControl
    {
        private string chosenDeptName;

        public string ChosenDeptName
        {
            get { return chosenDeptName; }
            set { chosenDeptName = value; }
        }

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

        private string spanID = "span_" + Guid.NewGuid().ToString().Replace('-', '_');

        public string SpanID
        {
            get
            {
                return spanID;
            }
        }
        #endregion

        /// <summary>
        /// 组织树根节点，如果不指定，则取全部节点
        /// </summary>
        public string OrgRootID { get; set; }

        public string chosenDeptname
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

        public string chosenDeptnameID
        {
            get
            {
                return hfdchosenDeptNameIds.Value.TrimEnd(',');
            }
            set
            {
                hfdchosenDeptNameIds.Value = value;
            }
        }
    }
}