using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin
{
    public partial class lgout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie Cookie = Request.Cookies["CNVP_HD_Users"];
                if (Cookie != null)
                {
                    Cookie.Expires = DateTime.Now.AddDays(-5);
                    Cookie.Value = null;
                    Response.Cookies.Add(Cookie);
                }
                Context.Session.Clear();
                Context.Session.Abandon();
                Response.Redirect("Appsearch.aspx");
                Response.End();
            }
        }
    }
}