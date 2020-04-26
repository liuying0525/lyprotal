
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    public enum EngineTypeEnum
    {
        //短信消息
        SMS,
        //邮箱
        Email,
        //Lync消息
        Lync,
        //微信消息
        WeChat,
        //微信部门消息
        WeChatParty,
    }

    public enum MessageStateEnum
    {
        /// <summary>
        /// 等待发送中，初始状态
        /// </summary>
        Waiting,
        /// <summary>
        /// 预发送状态，表示从数据库读取出来，准备发送
        /// </summary>
        PreSending,
        /// <summary>
        /// 正在发送中
        /// </summary>
        Sending,
        /// <summary>
        /// 已经发送(成功)
        /// </summary>
        Sent,
        /// <summary>
        /// 错误|失败后等待重新发送
        /// </summary>
        ErrorWaiting
    }

    /// <summary>
    /// 企业号创建应用对应id
    /// </summary>
    public enum ApplicationEnum
    {
        工作流程 = 6,
        职工之家 = 3,
        公司动态 = 2
    }
}
