using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.UI;
using System.Text;
using CNVP.Framework.Utils;

namespace CNVP.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetAppList":
                    GetAppList();
                    break;
                case "UserLogin":
                    UserLogin();
                    break;
            }
        }
        #region "获取门店列表"
        /// <summary>
        /// 获取门店列表
        /// </summary>
        private void GetAppList()
        {
            StringBuilder Str = new StringBuilder();
            Data.AppList bll = new Data.AppList();
            List<Model.AppList> model = bll.GetAppList();

            foreach (Model.AppList m in model)
            {
                Str.Append("{text:'" + m.AppName + "',id:" + m.AppID + "},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }
            
            Response.Write(string.Format("[{0}]", ReturnStr));
            Response.End();
        }
        #endregion
        #region "用户登录判断"
        /// <summary>
        /// 用户登录判断
        /// </summary>
        private void UserLogin()
        {
            string Str = "{msgCode:'0',msgStr:'您的登录密码不正确，请重新登录。'}";
            string LoginInfo = string.Empty;

            string UserName = Public.FilterSql(Request.Params["UserName"]);
            string UserPass = Public.FilterSql(Request.Params["UserPass"]);
            string AppID = Public.FilterSql(Request.Params["AppID"]);
            if (string.IsNullOrEmpty(AppID) || (!Public.IsNumber(AppID)))
            {
                AppID = "1";
            }

            Data.Users bll = new Data.Users();
            bool flg = bll.CheckLogin(Convert.ToInt32(AppID), UserName, EncryptUtils.MD5(UserPass), out LoginInfo);
            if (flg)
            {
                Str = "{msgCode:'1',msgStr:''}";

                //创建登录授权
                string UserID = LoginInfo.Split('|')[0];
                HttpCookie _Cookie = new HttpCookie(UIConfig.CookiesName);
                //string StrEncrypTo = EncryptUtils.EncodeCookies(LoginInfo);
                //_Cookie.Value = StrEncrypTo;
                _Cookie.Value = LoginInfo;
                HttpContext.Current.Response.Cookies.Add(_Cookie);

                //创建登录日志
                Data.SysLogs bll_logs = new Data.SysLogs();
                Model.SysLogs model_logs = new Model.SysLogs();
                model_logs.AppID = Convert.ToInt32(AppID);
                model_logs.UserID = Convert.ToInt32(UserID);
                model_logs.LogType = "登录日志";
                model_logs.LogTitle = string.Format("帐号[{0}]成功登录系统", Public.GetStrUpper(UserName));
                model_logs.LogContent = "";
                bll_logs.AddLogs(model_logs);
            }

            Response.Write(Str);
            Response.End(); 
        }
        #endregion
    }
}