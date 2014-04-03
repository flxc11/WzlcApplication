using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;
using CNVP.Framework.Utils;

namespace CNVP.Admin
{
    public partial class StoresEdit : AdminPage
    {
        public string CurrentAppID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Stores");

            if (!IsPostBack)
            {
                CurrentAppID = Request.Params["StoreID"];
                if (string.IsNullOrEmpty(CurrentAppID) || (!Public.IsNumber(CurrentAppID)))
                {
                    CurrentAppID = "0"; 
                }
            }
        }
    }
}