using CNVP.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin
{
    public partial class appli1_1 : System.Web.UI.Page
    {
        public string _Content = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = DbHelper.ExecuteTable("select * from HX_Notice Where ID=1");
                if (dt != null && dt.Rows.Count > 0)
                {
                    _Content = dt.Rows[0]["NoticeContent"].ToString();
                }
            }
        }
    }
}