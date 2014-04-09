using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class AppList
    {
        /// <summary>
        /// 应用序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 门店域名
        /// </summary>
        public string AppUrl { get; set; }
        /// <summary>
        /// 门店地址
        /// </summary>
        public string AppAddRess { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string AppTelPhone { get; set; }
        /// <summary>
        /// 售后电话
        /// </summary>
        public string AppServiceTelPhone { get; set; }
        /// <summary>
        /// 救援电话
        /// </summary>
        public string AppSOSTelPhone { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string AppFaxNumber { get; set; }
        /// <summary>
        /// 通讯公钥
        /// </summary>
        public string AppPubKey { get; set; }
        /// <summary>
        /// 通讯私钥
        /// </summary>
        public string AppPriKey { get; set; }
        /// <summary>
        /// 是否有效(0-锁定，1-有效)
        /// </summary>
        public int IsLock { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string PostTime { get; set; }

    }
}