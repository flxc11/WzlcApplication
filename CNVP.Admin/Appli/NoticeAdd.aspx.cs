using CNVP.UI;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.Appli
{
    public partial class NoticeAdd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["Action"] == "Save")
            {
                string NoticeTitle = Request.Params["NoticeTitle"];
                string NoticeContent = Request.Params["editorValue"];

                Data.Notice bll = new Data.Notice();
                Model.Notice model = new Model.Notice();
                model.NoticeTitle = NoticeTitle;
                model.NoticeContent = NoticeContent;
                model.PostTime = DateTime.Now;

                bll.AppNotice(model);
                Response.Write("<script>var win = parent || window;win.LG.closeAndReloadParent(null, 'NoticeList');</script>");
                Response.End();
            }
        }
    }
}