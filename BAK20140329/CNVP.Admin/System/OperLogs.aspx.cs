using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Framework.Utils;
using CNVP.UI;
using CNVP.Config;
using CNVP.Framework.Helper;

namespace CNVP.Admin
{
    public partial class OperLogs : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetLogsType":
                    GetLogsType();
                    break;
                case "GetAllLogs":
                    GetAllLogs();
                    break;
                case "DelLogs":
                    DelLogs();
                    break;
                case "ClearLogs":
                    ClearLogs();
                    break;
            }
        }
        #region "获取日志内容"
        /// <summary>
        /// 获取日志内容
        /// </summary>
        private void GetAllLogs()
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
            string SortName=Request.Params["SortName"];
            if (string.IsNullOrEmpty(SortName))
            {
                SortName="LogsID";
            }
            string SortOrder=Request.Params["SortOrder"];
            if(string.IsNullOrEmpty(SortOrder))
            {
                SortOrder = "Desc";
            }
            string LogType = Request.Params["LogType"];
            LogType = Public.FilterSql(LogType);

            int RecordCount, PageCount;
            string StrWhere = "" + DbConfig.Prefix + "Users AS C RIGHT OUTER JOIN "+ DbConfig.Prefix +"SysLogs AS A ON C.UserID = A.UserID LEFT OUTER JOIN "+ DbConfig.Prefix +"AppList AS B ON A.AppID = B.AppID Where 1=1";

            if (!string.IsNullOrEmpty(LogType))
            {
                StrWhere += string.Format(" And A.LogType='{0}'", LogType);
            }

            StringBuilder Str = new StringBuilder();
            DataTable Dt = DbHelper.ExecutePage("A.*,B.AppName,C.UserName", StrWhere, SortName, "Order By A.LogsID " + SortOrder + "", Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);

            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"LogsID\":" + Row["LogsID"] + ",\"LogType\":\"" + Row["LogType"] + "\",\"LogTitle\":\"" + Row["LogTitle"] + "\",\"LogIP\":\"" + Row["LogIP"] + "\",\"LogTime\":\"" + Public.GetDateTime(Row["LogTime"].ToString()) + "\",\"AppName\":\"" + Row["AppName"] + "\",\"UserName\":\"" + Public.GetStrUpper(Row["UserName"].ToString()) + "\"},");
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
        #region "获取日志类型"
        /// <summary>
        /// 获取日志类型
        /// </summary>
        private void GetLogsType()
        {
            StringBuilder Str = new StringBuilder();
            Data.SysLogs bll = new Data.SysLogs();
            DataTable Dt = bll.GetLogsType();
            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"Name\":\"" + Row["Name"].ToString() + "\",\"Value\":\"" + Row["Value"].ToString() + "\"},");
            }
            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("[{\"Name\":\"日志类型\",\"children\":[" + ReturnStr + "]}]");
            Response.End();
        }
        #endregion
        #region "删除日志内容"
        /// <summary>
        /// 删除日志内容
        /// </summary>
        private void DelLogs()
        {
            string ID = Request.Params["ID"];
            foreach (string a in ID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.SysLogs bll = new Data.SysLogs();
                    bll.DelLogs(Convert.ToInt32(a));
                }
            }
            Response.End();
        }
        #endregion
        #region "清空日志内容"
        /// <summary>
        /// 清空日志内容
        /// </summary>
        private void ClearLogs()
        {
            Data.SysLogs bll = new Data.SysLogs();
            bll.DelAllLogs();

            Response.End();
        }
        #endregion
    }
}