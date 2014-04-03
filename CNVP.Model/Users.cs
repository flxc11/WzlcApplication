using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Users
    {
        /// <summary>
        /// 管理员序号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPass { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserTrueName { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 用户性别(0-女，1-男)
        /// </summary>
        public int UserSex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string UserTelPhone { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string UserFaxNumber { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string UserQQ { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string UserAddRess { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string UserRemark { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginNum { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public string LoginTime { get; set; }
        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 是否有效(0-无效，1-有效)
        /// </summary>
        public int IsLock { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string PostTime { get; set; }
    }
}