using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.ViewModel._02.BLL
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class SMSMessage_ReadOnly_Model
    {
        public string Message
        {
            get
            {
                var str = "";

                for (int i = 1; i <= SingleContent.Count; i++)
                {
                    str = i.ToString() + "," + SingleContent[i - 1] + ";";
                }
                return str;
            }
        }

        public List<string> SingleContent { get; set; }

        public string Phone { get; set; }


    }
}
