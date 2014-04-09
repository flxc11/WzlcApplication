using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.Config;

namespace CNVP.Admin
{
    public partial class Users : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Users");

            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "IsExist":
                    IsExist();
                    break;
                case "GetUsersList":
                    GetUsersList();
                    break;
                case "GetUserSex":
                    GetUserSex();
                    break;
                case "IsAdmin":
                    IsAdmin();
                    break;
                case "RoleList":
                    RoleList();
                    break;
                case "GetUserInfo":
                    GetUserInfo();
                    break;
                case "AddUserInfo":
                    AddUserInfo();
                    break;
                case "EditUserInfo":
                    EditUserInfo();
                    break;
                case "DelUserInfo":
                    DelUserInfo();
                    break;
                case "IsLock":
                    IsLock();
                    break;
                case "IsReset":
                    IsReset();
                    break;
            }
        }
        #region "是否存在帐号"
        /// <summary>
        /// 是否存在帐号
        /// </summary>
        private void IsExist()
        {
            bool flg = true;
            string LoginName = Public.FilterSql(Request.Params["LoginName"]);
            Data.Users bll = new Data.Users();
            if (bll.IsExist(LoginName))
            {
                flg = false;
            }
            Response.Write(flg.ToString().ToLower());
            Response.End();
        }
        #endregion
        
        #region "获取帐号列表"
        /// <summary>
        /// 获取帐号列表
        /// </summary>
        private void GetUsersList()
        {
            string PageNo = Request.Params["Page"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            string PageSize = Request.Params["PageSize"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageSize = "20";
            }
            string SortName = Request.Params["SortName"];
            if (string.IsNullOrEmpty(SortName))
            {
                SortName = "A.UserID";
            }
            string SortOrder = Request.Params["SortOrder"];
            if (string.IsNullOrEmpty(SortOrder))
            {
                SortOrder = "Desc";
            }
            string AppID = Request.Params["AppID"];

            int RecordCount, PageCount;
            string StrWhere = "" + DbConfig.Prefix + "AppList AS B INNER JOIN " + DbConfig.Prefix + "AppUsers AS C ON B.AppID = C.AppID RIGHT OUTER JOIN " + DbConfig.Prefix + "Users AS A ON C.UserID = A.UserID Where 1=1";

            if (!string.IsNullOrEmpty(AppID) && (Public.IsNumber(AppID)))
            {
                StrWhere += string.Format(" And C.AppID={0}", AppID);
            }

            StringBuilder Str = new StringBuilder();
            DataTable Dt = DbHelper.ExecutePage("A.*,B.AppName,C.AppID", StrWhere, SortName, "Order By A.UserID " + SortOrder + "", Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);

            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"UserID\":" + Row["UserID"].ToString() + ",\"UserName\":\"" + Public.GetStrUpper(Row["UserName"].ToString()) + "\",\"UserTrueName\":\"" + Row["UserTrueName"] + "\",\"UserEmail\":\"" + Public.GetStrUpper(Row["UserEmail"].ToString()) + "\",\"UserMobile\":\"" + Row["UserMobile"].ToString() + "\",\"UserTelPhone\":\"" + Row["UserTelPhone"].ToString() + "\",\"LoginNum\":\"" + Row["LoginNum"].ToString() + "\",\"LoginTime\":\"" + Public.GetDateTime(Row["LoginTime"].ToString()) + "\",\"LoginIP\":\"" + Row["LoginIP"] + "\",\"IsLock\":\"" + Row["IsLock"] + "\",\"PostTime\":\"" + Public.GetDateTime(Row["PostTime"].ToString()) + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + RecordCount + "\"}");
            Response.End();
        }
        #endregion
        #region "获取性别信息"
        /// <summary>
        /// 获取性别信息
        /// </summary>
        private void GetUserSex()
        {
            Response.Write("[{\"id\":\"2\",\"text\":\"保密\"},{\"id\":\"1\",\"text\":\"男性\"},{\"id\":\"0\",\"text\":\"女性\"}]");
            Response.End();
        }
        #endregion
        #region "是否超级用户"
        /// <summary>
        /// 是否超级用户
        /// </summary>
        private void IsAdmin()
        {
            //options: { valueFieldID: "IsAdmin", data: [{ text: '否(只可管理自己门店的信息)', id: '0' }, { text: '是(允许管理所有门店的信息)', id: '1'}]}
            Response.Write("[{\"id\":\"0\",\"text\":\"否(只可管理自己门店的信息)\"},{\"id\":\"1\",\"text\":\"是(允许管理所有门店的信息)\"}]");
            Response.End();
        }
        #endregion
        #region "用户角色列表"
        /// <summary>
        /// 用户角色列表
        /// </summary>
        private void RoleList()
        {
            StringBuilder Str = new StringBuilder();
            Data.Roles bll = new Data.Roles();
            List<Model.Roles> model = bll.GetAllRoles();
            foreach (Model.Roles m in model)
            {
                Str.Append("{\"id\":\"" + m.RoleID + "\",\"text\":\"" + m.RoleName + "\"},");
            }
            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("[" + ReturnStr + "]");
            Response.End();
        }
        #endregion
        #region "获取用户信息"
        /// <summary>
        /// 获取用户信息
        /// </summary>
        private void GetUserInfo()
        {
            string UserID = Request.Params["UserID"];
            if (string.IsNullOrEmpty(UserID) || (!Public.IsNumber(UserID)))
            {
                UserID = "0"; 
            }

            Data.Users bll = new Data.Users();
            DataTable Dt = bll.GetUserInfo(Convert.ToInt32(UserID));
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Response.Write("{\"IsError\":false,\"Message\":\"加载成功\",\"Data\":{\"UserID\":\"" + Dt.Rows[0]["UserID"] + "\",\"LoginName\":\"" + Public.GetStrUpper(Dt.Rows[0]["UserName"].ToString()) + "\",\"UserTrueName\":\"" + Dt.Rows[0]["UserTrueName"] + "\",\"UserEMail\":\"" + Dt.Rows[0]["UserEMail"] + "\",\"UserSex\":\"" + Dt.Rows[0]["UserSex"] + "\",\"UserMobile\":\"" + Dt.Rows[0]["UserMobile"] + "\",\"UserTelPhone\":\"" + Dt.Rows[0]["UserTelPhone"] + "\",\"UserFaxNumber\":\"" + Dt.Rows[0]["UserFaxNumber"] + "\",\"UserQQ\":\"" + Dt.Rows[0]["UserQQ"] + "\",\"UserNickName\":\"" + Dt.Rows[0]["UserNickName"] + "\",\"UserAddRess\":\"" + Dt.Rows[0]["UserAddRess"] + "\",\"UserRemark\":\"" + Dt.Rows[0]["UserRemark"] + "\",\"AppID\":\"" + Dt.Rows[0]["AppID"] + "\",\"RoleID\":\"" + Dt.Rows[0]["RoleID"] + "\",\"IsAdmin\":\"" + Dt.Rows[0]["IsAdmin"] + "\",\"PostTime\":\"" + Public.GetDateTime(Dt.Rows[0]["PostTime"].ToString()) + "\"}}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"找不到该记录\",\"Data\":{}}");
            }
            Response.End();
        }
        #endregion
        #region "增加管理帐号"
        /// <summary>
        /// 增加管理帐号
        /// </summary>
        private void AddUserInfo()
        {
            string AppID = "1";
            string RoleID = Request.Params["RoleID"];
            string IsAdmin = "1";
            string LoginName = Request.Params["LoginName"];
            string UserPass = Request.Params["UserPass"];
            string UserTrueName = Request.Params["UserTrueName"];
            string UserEMail = Request.Params["UserEMail"];
            string UserRemark = Request.Params["UserRemark"];
            string UserSex = Request.Params["UserSex"];
            string UserTelPhone = Request.Params["UserTelPhone"];
            string UserMobile = Request.Params["UserMobile"];
            string UserFaxNumber = Request.Params["UserFaxNumber"];
            string UserQQ = Request.Params["UserQQ"];
            string UserNickName = Request.Params["UserNickName"];
            string UserAddRess = Request.Params["UserAddRess"];

            DictHelper<string> Dict = new DictHelper<string>();
            Dict.AddItem("AppID", AppID);
            Dict.AddItem("RoleID", RoleID);
            Dict.AddItem("IsAdmin", IsAdmin);
            Dict.AddItem("UserName", LoginName);
            Dict.AddItem("UserPass", UserPass);
            Dict.AddItem("UserTrueName", UserTrueName);
            Dict.AddItem("UserEMail", UserEMail);
            Dict.AddItem("UserRemark", UserRemark);
            Dict.AddItem("UserSex", UserSex);
            Dict.AddItem("UserTelPhone", UserTelPhone);
            Dict.AddItem("UserMobile", UserMobile);
            Dict.AddItem("UserFaxNumber", UserFaxNumber);
            Dict.AddItem("UserQQ", UserQQ);
            Dict.AddItem("UserNickName", UserNickName);
            Dict.AddItem("UserAddRess", UserAddRess);

            Data.Users bll = new Data.Users();
            bll.AddUserInfo(Dict);

            Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            Response.End();
        }
        #endregion
        #region "编辑管理帐号"
        /// <summary>
        /// 编辑管理帐号
        /// </summary>
        private void EditUserInfo()
        {
            string UserID = Request.Params["UserID"];
            string AppID = "1";
            string RoleID = Request.Params["RoleID"];
            string IsAdmin = "1";
            string LoginName = Request.Params["LoginName"];
            string UserPass = Request.Params["UserPass"];
            string UserTrueName = Request.Params["UserTrueName"];
            string UserEMail = Request.Params["UserEMail"];
            string UserRemark = Request.Params["UserRemark"];
            string UserSex = Request.Params["UserSex"];
            string UserTelPhone = Request.Params["UserTelPhone"];
            string UserMobile = Request.Params["UserMobile"];
            string UserFaxNumber = Request.Params["UserFaxNumber"];
            string UserQQ = Request.Params["UserQQ"];
            string UserNickName = Request.Params["UserNickName"];
            string UserAddRess = Request.Params["UserAddRess"];

            DictHelper<string> Dict = new DictHelper<string>();
            Dict.AddItem("UserID", UserID);
            Dict.AddItem("AppID", AppID);
            Dict.AddItem("RoleID", RoleID);
            Dict.AddItem("IsAdmin", IsAdmin);
            Dict.AddItem("UserName", LoginName);
            Dict.AddItem("UserPass", UserPass);
            Dict.AddItem("UserTrueName", UserTrueName);
            Dict.AddItem("UserEMail", UserEMail);
            Dict.AddItem("UserRemark", UserRemark);
            Dict.AddItem("UserSex", UserSex);
            Dict.AddItem("UserTelPhone", UserTelPhone);
            Dict.AddItem("UserMobile", UserMobile);
            Dict.AddItem("UserFaxNumber", UserFaxNumber);
            Dict.AddItem("UserQQ", UserQQ);
            Dict.AddItem("UserNickName", UserNickName);
            Dict.AddItem("UserAddRess", UserAddRess);

            Data.Users bll = new Data.Users();
            bll.EditUserInfo(Dict);

            Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            Response.End();
        }
        #endregion 
        #region "删除管理帐号"
        /// <summary>
        /// 删除管理帐号
        /// </summary>
        private void DelUserInfo()
        {
            string UserID = Request.Params["UserID"];
            foreach (string a in UserID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.Users bll = new Data.Users();
                    bll.DelUserInfo(Convert.ToInt32(a));
                }
            }
            Response.End();
        }
        #endregion
        #region "启用禁用帐号"
        /// <summary>
        /// 启用禁用帐号
        /// </summary>
        private void IsLock()
        {
            string UserID = Request.Params["UserID"];
            string IsLock = Request.Params["IsLock"];
            if (string.IsNullOrEmpty(IsLock) || (!Public.IsNumber(IsLock)))
            {
                IsLock = "0"; 
            }
            foreach (string a in UserID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.Users bll = new Data.Users();
                    bll.IsLock(Convert.ToInt32(a), Convert.ToInt32(IsLock));
                }
            }
            Response.End();
        }
        #endregion
        #region "重置登录密码"
        /// <summary>
        /// 重置登录密码
        /// </summary>
        private void IsReset()
        {
            string UserID = Request.Params["UserID"];
            if (!string.IsNullOrEmpty(UserID) && (Public.IsNumber(UserID)))
            {
                Data.Users bll = new Data.Users();
                bll.IsReset(Convert.ToInt32(UserID));
            }
            Response.Write("{\"IsError\":false,\"Message\":\"密码重置操作成功，默认密码为[" + UIConfig.ResetPwd + "]。\"}");
            Response.End();
        }
        #endregion
    }
}