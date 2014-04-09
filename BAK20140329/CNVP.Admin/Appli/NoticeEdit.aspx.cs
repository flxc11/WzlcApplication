using CNVP.Framework.Utils;
using CNVP.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.Appli
{
    public partial class NoticeEdit : AdminPage
    {
        public string CurrentID, NoticeContent = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentID = Request.Params["ID"];
                if (string.IsNullOrEmpty(CurrentID) || (!Public.IsNumber(CurrentID)))
                {
                    CurrentID = "0";
                }
                Data.Notice bll = new Data.Notice();
                DataTable dt = bll.GetNoticeInfo(Convert.ToInt32(CurrentID));
                if (dt != null && dt.Rows.Count > 0)
                {
                    NoticeTitle.Text = dt.Rows[0]["NoticeTitle"].ToString();
                    NoticeContent = dt.Rows[0]["NoticeContent"].ToString();
                }
            }

            if (Request.Params["Action"] == "Edit")
            {
                CurrentID = Request.Params["HidTypeID"];
                string NoticeTitle = Request.Params["NoticeTitle"];
                NoticeContent = Request.Params["editorValue"];
                Data.Notice bll1 = new Data.Notice();
                Model.Notice model1 = new Model.Notice();
                model1.ID = Convert.ToInt32(CurrentID);
                model1.NoticeTitle = NoticeTitle;
                model1.NoticeContent = NoticeContent;
                bll1.EditNoticeInfo(model1);
                Response.Write("<script>var win = parent || window;win.LG.closeAndReloadParent(null, 'NoticeList');</script>");
                Response.End();
            }
        }
    }
}