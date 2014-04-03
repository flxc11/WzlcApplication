using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;
using CNVP.Framework.Utils;

namespace CNVP.Admin
{
    public partial class UsersEdit : AdminPage
    {
        public string CurrentUserID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Users");

            if (!IsPostBack)
            {
                CurrentUserID = Request.Params["UserID"];
                if (string.IsNullOrEmpty(CurrentUserID) || (!Public.IsNumber(CurrentUserID)))
                {
                    CurrentUserID = "0";
                }
            }
        }
    }
}