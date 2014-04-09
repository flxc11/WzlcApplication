using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.Appli
{
    public partial class List : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Users");

            if (!IsPostBack)
            {
                //string softwareSerialNo = "3SDK-EMY-0130-JCXMM";
                //string key = "cnvp";
                //string serialpass = "326410";

                //int rslt = Ajax.CreateService().registEx(softwareSerialNo, key, serialpass);
                //if (rslt == 0)
                //{
                //    Response.Write("<script>alert('注册成功！');</script>");
                //}
                //else
                //{
                //    Response.Write("<script>alert('注册失败，请联系管理员！')</script>");
                //    Response.End();
                //}

                string Action = Request.Params["Action"];
                switch (Action)
                {
                    case "AppList":
                        AppList();
                        break;
                    case "Del":
                        Del();
                        break;
                    case "IsLock":
                        IsLock();
                        break;
                    case "GetAppInfo":
                        GetAppInfo();
                        break;
                }
            }
        }

        private void AppList()
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
            int RecordCount, PageCount;
            string StrWhere = "" + DbConfig.Prefix + "Application As B INNER JOIN " + DbConfig.Prefix + "AppliUsers As A ON B.AppUserID=A.ID Where 1=1 And A.ID=" + UserID;
            DataTable dt = DbHelper.ExecutePage("B.ID as BID,A.ID,A.AppName,B.AppType,B.PostTime,B.IsAudit,B.AppContent,B.AppReply, B.PostTime", StrWhere, "B.ID", "Order By B.PostTime Desc", Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);
            StringBuilder SB = new StringBuilder();
            foreach (DataRow Row in dt.Rows)
            {
                SB.Append("{\"ID\":\"" + Row["BID"] + "\",\"AppName\":\"" + Row["AppName"] + "\",\"AppType\":\"" + Row["AppType"] + "\",\"AppContent\":\"" + Row["AppContent"] + "\",\"PostTime\":\"" + Convert.ToDateTime(Row["PostTime"]).ToString("yyyy-MM-dd") + "\",\"IsAudit\":\"" + Row["IsAudit"] + "\",\"AppReply\":\"" + Row["AppReply"] + "\"},");
            }
            string ReturnStr = SB.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }
            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + RecordCount + "\"}");
            Response.End();
        }

        #region "删除申请记录"
        /// <summary>
        /// 删除申请记录
        /// </summary>
        private void Del()
        {
            string ID = Request.Params["ID"];
            foreach (string a in ID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.Application bll = new Data.Application();
                    bll.Del(Convert.ToInt32(a));
                }
            }
            Response.End();
        }
        #endregion

        #region "删除事项"
        /// <summary>
        /// 删除事项
        /// </summary>
        private void TypeDel()
        {
            string ID = Request.Params["ID"];
            foreach (string a in ID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.Application bll = new Data.Application();
                    bll.Del(Convert.ToInt32(a));
                }
            }
            Response.End();
        }
        #endregion

        #region
        /// <summary>
        /// 修改申请状态
        /// </summary>
        private void IsLock()
        {
            string ID = Request.Params["ID"];
            string IsLock = Request.Params["IsLock"];
            if (string.IsNullOrEmpty(IsLock) || (!Public.IsNumber(IsLock)))
            {
                IsLock = "0";
            }
            foreach (string a in ID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.Application bll = new Data.Application();
                    bll.IsLock(Convert.ToInt32(a), IsLock);
                }
            }
            Response.End();
        }
        #endregion

        #region
        /// <summary>
        /// 申请详细信息
        /// </summary>
        public void GetAppInfo()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID) || (!Public.IsNumber(ID)))
            {
                ID = "0";
            }
            DataTable Dt = DbHelper.ExecuteTable("select A.AppName, A.AppCardID, A.AppPhone, A.AppAddress,A.AppEmail, B.AppType, B.AppResult, B.AppPic, B.AppContent, B.AppReply,B.PostTime from HX_Application as B inner join HX_AppliUsers as A on B.AppUserID=A.ID Where B.ID=" + ID);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Response.Write("{\"IsError\":false,\"Message\":\"加载成功\",\"Data\":{\"AppName\":\"" + Dt.Rows[0]["AppName"] + "\",\"AppCardID\":\"" + Dt.Rows[0]["AppCardID"] + "\",\"AppPhone\":\"" + Dt.Rows[0]["AppPhone"] + "\",\"AppAddress\":\"" + Dt.Rows[0]["AppAddress"] + "\",\"AppEmail\":\"" + Dt.Rows[0]["AppEmail"] + "\",\"AppType\":\"" + GetType(Dt.Rows[0]["AppType"].ToString()) + "\",\"AppResult\":\"" + GetResult(Dt.Rows[0]["AppResult"].ToString()) + "\",\"AppPic\":\"" + Dt.Rows[0]["AppPic"] + "\",\"AppContent\":\"" + Dt.Rows[0]["AppContent"] + "\",\"PostTime\":\"" + Convert.ToDateTime(Dt.Rows[0]["PostTime"].ToString()).ToString("yyyy-MM-dd") + "\"}}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"找不到该记录\",\"Data\":{}}");
            }
            Response.End();
        }
        #endregion

        #region 申请类型
        public string GetType(string _Type)
        {
            string str = "个人";
            if (_Type == "1")
            {
                str = "企业";
            }
            return str;
        }
        #endregion

        #region 结果需求
        public string GetResult(string _Type)
        {
            string str = "邮件";
            if (_Type == "1")
            {
                str = "快递（到付）";
            }
            return str;
        }
        #endregion
    }
}