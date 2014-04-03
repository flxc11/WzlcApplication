using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.Client
{
    public partial class GroupList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetGroupList":
                    GetGroupList();
                    break;
            }
        }
        #region "获取套餐列表"
        /// <summary>
        /// 获取套餐列表
        /// </summary>
        private void GetGroupList()
        {
            StringBuilder Str = new StringBuilder();
            Str.Append("<li class=\"Title\">套餐组合</li>");

            //获取套餐列表
            Data.Group bll = new Data.Group();
            List<Model.Group> model = bll.GetGroupList(Convert.ToInt32(UIConfig.ClientID));
            foreach (Model.Group m in model)
            {
                string GroupImages = m.GroupImages;
                if (string.IsNullOrEmpty(GroupImages))
                {
                    GroupImages = "Images/NoImages.jpg";
                }
                Str.Append("<li class=\"List\">");
                Str.AppendFormat("<a href=\"GroupInfo.aspx?GroupID={1}\"><img src=\"{0}\"/></a>", GroupImages, m.GroupID);
                Str.AppendFormat("<h1>套餐名称：<a href=\"GroupInfo.aspx?GroupID={1}\">{0}</a></h1>", m.GroupName, m.GroupID);
                Str.AppendFormat("<h2>套餐原价：{0}元</h2>", m.AllPrice);
                Str.AppendFormat("<h2>活动价格：{0}元</h2>", m.GroupPrice);
                Str.AppendFormat("<h2>适用车型：{0}</h2>", m.CarName);
                Str.AppendFormat("<h2>结束时间：{0}</h2>", Public.GetDate(m.ExpireTime));
                Str.Append("</li>");
            }

            Response.Write(Str.ToString());
            Response.End();
        }
        #endregion
    }
}