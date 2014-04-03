using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Software
    {
        /// <summary>
        /// 软件序号
        /// </summary>
        public int SoftID { get; set; }
        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftName { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftVersion { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string SoftPubTime { get; set; }
        /// <summary>
        /// 软件包地址
        /// </summary>
        public string SoftDownUrl { get; set; }
        /// <summary>
        /// 补丁包地址
        /// </summary>
        public string SoftPatchUrl { get; set; }
        /// <summary>
        /// 更新说明
        /// </summary>
        public string SoftContent { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string PostTime { get; set; }
    }
}