using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Permissions
    {
        /// <summary>
        /// 角色序号
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public string OperateCode { get; set; }
    }
}