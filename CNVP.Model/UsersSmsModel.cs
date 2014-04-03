using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class UsersSmsModel
    {
        /// <summary>
        /// 站内消息序号
        /// </summary>
        public int SmsID { get; set; }
        /// <summary>
        /// 用户序号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 站内消息标题
        /// </summary>
        public string SmsTitle { get; set; }
        /// <summary>
        /// 站内消息内容
        /// </summary>
        public string SmsContent { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string PostTime { get; set; }

        public string UserPhone { get; set; }
    }
}
