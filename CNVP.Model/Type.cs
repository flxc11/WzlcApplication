using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Type
    {
        /// <summary>
        /// 事项ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 申请事项名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 申请事项说明
        /// </summary>
        public string TypeContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime PostTime { get; set; }
    }
}