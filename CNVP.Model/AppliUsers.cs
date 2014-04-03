using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class AppliUsers
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string AppCardID { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string AppPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string AppAddress { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string AppEmail { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime PostTime { get; set; }
    }
}
