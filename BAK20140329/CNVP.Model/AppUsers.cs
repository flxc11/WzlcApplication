using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class AppUsers
    {
        /// <summary>
        /// 客户端序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 用户序号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 是否管理员(0-非，1是)
        /// </summary>
        public int IsAdmin { get; set; }
    }
}