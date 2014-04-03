using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;

namespace CNVP.Admin
{
    public partial class FilesSearch : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断用户权限
                base.CheckAuthority("Files");
            }
        }
    }
}