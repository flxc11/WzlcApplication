using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using CNVP.Framework.Helper;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.UI
{
    public class AdminPage : BasePage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AdminPage()
        {
            if (!CheckLogin())
            {
                MessageUtils.Write("<script>top.location.href=\"/Login.aspx\"</script>");
            }
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        /// <returns></returns>
        public static bool CheckLogin()
        {
            bool flg = false;
            if (LoginInfo()["UserID"] != "0")
            {
                flg = true;
            }
            return flg;
        }
        /// <summary>
        /// 用户序号
        /// </summary>
        public int UserID
        {
            get
            {
                return Convert.ToInt32(LoginInfo("UserID"));
            }
        }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string UserName
        {
            get
            {
                return LoginInfo("UserName");
            }
        }
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID
        {
            get
            {
                return Convert.ToInt32(LoginInfo("AppID"));
            }
        }
        /// <summary>
        /// 角色序号
        /// </summary>
        public int RoleID
        {
            get
            {
                return Convert.ToInt32(LoginInfo("RoleID"));
            }
        }
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin
        {
            get
            {
                bool flg = false;
                if (LoginInfo("IsAdmin") != "0")
                {
                    flg = true;
                }
                return flg;
            }
        }
        /// <summary>
        /// 用户登录信息
        /// </summary>
        /// <param name="Param">登录参数(UserID,UserName,AppID,RoleID,IsAdmin)</param>
        /// <returns></returns>
        public static string LoginInfo(string Param)
        {
            return LoginInfo()[Param];
        }
        /// <summary>
        /// 用户登录信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> LoginInfo()
        {
            DictHelper<string> Dict = new DictHelper<string>();

            //设置初始值
            Dict.AddItem("UserID", "0");
            Dict.AddItem("UserName", "");
            Dict.AddItem("AppID", "0");
            Dict.AddItem("RoleID", "0");
            Dict.AddItem("IsAdmin", "0");

            HttpCookie Cookie = HttpContext.Current.Request.Cookies[UIConfig.CookiesName];
            if (Cookie != null)
            {
                try
                {
                    string Str = Cookie.Value;
                    //string StrDecrypTo = EncryptUtils.DecodeCookies(Str);
                    //string[] StrInfo = StrDecrypTo.Split('|');
                    string[] StrInfo = Str.Split('|');

                    //清空集合
                    Dict.Clear();
                    Dict.AddItem("UserID", StrInfo[0]);
                    Dict.AddItem("UserName", StrInfo[1]);
                    Dict.AddItem("AppID", StrInfo[2]);
                    Dict.AddItem("RoleID", StrInfo[3]);
                    Dict.AddItem("IsAdmin", StrInfo[4]);
                }
                catch
                {
                    LogHelper.Write("用户登录状态解密失败", "当前用户的Cookies值[" + Cookie.Value + "]");
                }
            }
            return Dict.List;
        }
        /// <summary>
        /// 判断操作权限
        /// </summary>
        /// <param name="OperateCode">权限值</param>
        public void CheckAuthority(string OperateCode)
        {
            if (!CheckPopDom(OperateCode))
            {
                MessageUtils.Write("<script language=\"javascript\" type=\"text/javascript\" src=\"../lib/jquery/jquery-1.5.2.min.js\"></script><script language=\"javascript\" type=\"text/javascript\" src=\"../lib/ligerUI/js/ligerui.min.js\"></script><script language=\"javascript\" type=\"text/javascript\" src=\"../lib/js/common.js\"></script><script language=\"javascript\" type=\"text/javascript\" src=\"../lib/js/LG.js\"></script><script language=\"javascript\" type=\"text/javascript\">alert('警告：您没有该模块的操作权限！');var win = parent || window;win.LG.closeAndReloadParent(null, '');</script>");
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 判断操作权限
        /// </summary>
        /// <param name="OperateCode">权限值</param>
        /// <returns></returns>
        public bool CheckPopDom(string OperateCode)
        {
            if ((IsAdmin) || (new Data.Permissions().CheckPopDom(RoleID, OperateCode)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        public void LoginOut()
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[UIConfig.CookiesName];
            if (Cookie != null)
            {
                Cookie.Expires = DateTime.Now.AddDays(-1);
                Cookie.Value = null;
                Response.Cookies.Add(Cookie);
            }
            Context.Session.Clear();
            Context.Session.Abandon();

            MessageUtils.Write("<script>top.location.href=\"/Login.aspx\"</script>");
        }
    }
}