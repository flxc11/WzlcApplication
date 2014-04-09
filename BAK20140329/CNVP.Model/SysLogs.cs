using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class SysLogs
    {
        /// <summary>
        /// 日志序号
        /// </summary>
        public int LogsID { get; set; }
        /// <summary>
        /// 应用序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 用户序号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 日志类别
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }
        /// <summary>
        /// 操作IP地址
        /// </summary>
        public string LogIP { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public string LogTime { get; set; }
    }
}