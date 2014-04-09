using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Tactics
    {
        /// <summary>
        /// 策略序号
        /// </summary>
        public int TacticsID { get; set; }
        /// <summary>
        /// 商户序号
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 操作方法(UpdateSql|UpdateFile|UpdateSoft)
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        public string SqlInfo { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 优先级别(数字越大，优先执行)
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 更新状态(0-未执行，1-已执行)
        /// </summary>
        public int IsUpdate { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string UpdateIP { get; set; }
        /// <summary>
        /// 更新完成时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string PostTime { get; set; }
    }
}