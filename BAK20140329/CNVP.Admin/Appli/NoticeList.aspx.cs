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
    public partial class NoticeList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Action = Request.Params["Action"];
                switch (Action)
                {
                    case "List":
                        GetList();
                        break;
                    case "AddInfo":
                        AddInfo();
                        break;
                    case "Del":
                        Del();
                        break;
                    case "GetTypeInfo":
                        GetTypeInfo();
                        break;
                    case "EditInfo":
                        EditInfo();
                        break;
                }
            }
        }

        private void GetList()
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
            string StrWhere = "" + DbConfig.Prefix + "Notice ";
            DataTable dt = DbHelper.ExecutePage("*", StrWhere, "ID", "Order By ID Desc", Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);
            StringBuilder SB = new StringBuilder();
            foreach (DataRow Row in dt.Rows)
            {
                SB.Append("{\"ID\":\"" + Row["ID"] + "\",\"NoticeTitle\":\"" + Row["NoticeTitle"] + "\",\"PostTime\":\"" + Convert.ToDateTime(Row["PostTime"].ToString()).ToString("yyyy-MM-dd") + "\"},");
            }
            string ReturnStr = SB.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }
            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + RecordCount + "\"}");
            Response.End();
        }

        #region 新增事项
        private void AddInfo()
        {
            string TypeName = Request.Params["TypeName"];
            string TypeContent = Request.Params["TypeContent"];

            Data.Type bll = new Data.Type();
            Model.Type model = new Model.Type();
            model.TypeName = TypeName;
            model.TypeContent = TypeContent;
            model.PostTime = DateTime.Now;
            bll.AppType(model);
            Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            Response.End();
        }
        #endregion

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
                    Data.Type bll = new Data.Type();
                    bll.Del(Convert.ToInt32(a));
                }
            }
            Response.End();
        }
        #endregion

        #region 获取类型信息
        /// <summary>
        /// 获取类型信息
        /// </summary>
        public void GetTypeInfo()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID) || !Public.IsNumber(ID))
            {
                ID = "0";
            }
            Data.Type bll = new Data.Type();
            DataTable Dt = bll.GetTypeInfo(Convert.ToInt32(ID));
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Response.Write("{\"IsError\":false,\"Message\":\"加载成功\",\"Data\":{\"ID\":\"" + Dt.Rows[0]["ID"] + "\",\"TypeName\":\"" + Dt.Rows[0]["TypeName"] + "\",\"TypeContent\":\"" + Dt.Rows[0]["TypeContent"] + "\"}}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"找不到该记录\",\"Data\":{}}");
            }
            Response.End();
        }
        #endregion

        #region
        public void EditInfo()
        {
            string ID = Request.Params["ID"];
            string TypeName = Request.Params["TypeName"];
            string TypeContent = Request.Params["TypeContent"];

            Data.Type bll = new Data.Type();
            Model.Type model = new Model.Type();

            model.ID = Convert.ToInt32(ID);
            model.TypeName = TypeName;
            model.TypeContent = TypeContent;
            bll.EditTypeInfo(model);

            Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            Response.End();
        }
        #endregion
    }
}