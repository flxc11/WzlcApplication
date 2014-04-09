using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Framework.Utils;
using CNVP.UI;

namespace CNVP.Admin
{
    public partial class StoresDown : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Stores");

            if (!IsPostBack)
            {
                string AppID = Request.Params["AppID"];
                if (string.IsNullOrEmpty(AppID) || (!Public.IsNumber(AppID)))
                {
                    AppID = "0";
                }

                //读取站点信息
                Data.AppList bll = new Data.AppList();
                Model.AppList model = bll.GetAppInfo(Convert.ToInt32(AppID));
                if (model != null)
                {
                    Response.ContentType = "application/ms-word";
                    Response.AddHeader("Content-Disposition", "inline;filename=Config.ini");
                    StringBuilder Str = new StringBuilder();
                    Str.Append("[同步工具]\r\n");
                    Str.Append("TimeSpan=1\r\n");
                    Str.AppendFormat("AppUrl=http://{0}\r\n", Public.GetHost());
                    Str.AppendFormat("AppID={0}\r\n", model.AppID);
                    Str.AppendFormat("AppPriKey={0}\r\n", model.AppPriKey);
                    Str.Append("WebRoot=");
                    Response.Write(Str.ToString());
                    Response.End();
                }
            }
        }
    }
}