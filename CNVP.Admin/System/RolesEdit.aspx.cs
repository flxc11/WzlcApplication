using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;
using CNVP.Framework.Utils;

namespace CNVP.Admin
{
    public partial class RolesEdit : AdminPage
    {
        public string CurrentRoleID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Users");

            if (!IsPostBack)
            {
                CurrentRoleID = Request.Params["RoleID"];
                if (string.IsNullOrEmpty(CurrentRoleID) || (!Public.IsNumber(CurrentRoleID)))
                {
                    CurrentRoleID = "0";
                }
            }
        }
    }
}