using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.Client
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Action = Request.Params["Action"];
                switch (Action)
                {
                    case "GetGroupList":
                        GetGroupList();
                        break;
                    case "GetAllGroupList":
                        GetAllGroupList();
                        break;
                    case "GetGroupInfo":
                        GetGroupInfo();
                        break;
                    case "GetGroupProduct":
                        GetGroupProduct();
                        break;
                }
            }
        }
        #region "获取套餐组合"
        /// <summary>
        /// 获取套餐组合
        /// </summary>
        private void GetGroupList()
        {
            StringBuilder Str = new StringBuilder();
            Str.Append("<li class=\"title\">套餐组合</li>");

            //获取套餐列表
            Data.Group bll = new Data.Group();
            List<Model.Group> model = bll.GetGroupList(Convert.ToInt32(UIConfig.ClientID));
            foreach (Model.Group m in model)
            {
                string GroupImages = m.GroupImages;
                if (string.IsNullOrEmpty(GroupImages))
                {
                    GroupImages = "/themes/images/noimages.jpg";
                }
                Str.Append("<li class=\"content\">");
                Str.AppendFormat("<a href=\"GroupInfo.aspx?GroupID={1}\" target=\"_blank\"><img src=\"{0}\"/></a>", GroupImages, m.GroupID);
                Str.AppendFormat("<h1>套餐名称1：<a href=\"GroupInfo.aspx?GroupID={1}\" target=\"_blank\">{0}</a></h1>", m.GroupName, m.GroupID);
                Str.AppendFormat("<h2>套餐原价：{0}元</h2>", m.AllPrice);
                Str.AppendFormat("<h2>活动价格：{0}元</h2>", m.GroupPrice);
                Str.AppendFormat("<h2>适用车型：{0}元</h2>", m.CarName);
                Str.AppendFormat("<h2>结束时间：{0}元</h2>", Public.GetDate(m.ExpireTime));
                Str.Append("</li>");
            }
            //Response.Write(Str.ToString());
            Response.End();
        }
        #endregion
        #region "获取套餐列表"
        /// <summary>
        /// 获取套餐列表
        /// </summary>
        private void GetAllGroupList()
        {
            string GroupID = Request.Params["GroupID"];

            Response.Write("<dl>");
            Response.Write("<dt></dt>");

            Data.Group bll = new Data.Group();
            List<Model.Group> model = bll.GetGroupList(Convert.ToInt32(UIConfig.ClientID));
            foreach (Model.Group m in model)
            {
                string CssClass = string.Empty;
                if (m.GroupID == GroupID)
                {
                    CssClass = " class=\"current\"";
                }

                Response.Write("<dd onclick=\"GetGroupInfo('" + m.GroupID + "');\"" + CssClass + ">");
                Response.Write("<table style=\"width:200px;margin:0px auto\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                Response.Write("<tr>");
                Response.Write("<td style=\"height:50px\"><a href=\"javascript:void();\" onclick=\"GetGroupInfo('" + m.GroupID + "');\"><img src=\"Themes/images/noimages.jpg\" height=\"50\" style=\"border:solid 1px #ccc\" /></a></td>");
                Response.Write("<td style=\"height:30px;font-size:14px;padding:0px 5px;\" valign=\"top\"><b><a href=\"javascript:void();\" onclick=\"GetGroupInfo('" + m.GroupID + "');\">" + m.GroupName + "</a></b><br/><span style=\"color:#00F\">截至时间：" + Public.GetDate(m.ExpireTime) + "</span></td>");
                Response.Write("</tr>");
                Response.Write("</table>");
                Response.Write("</dd>");
                Response.Write("<dt style=\"height:2px;\"><img src=\"Themes/images/typebottom.png\" style=\"width:100%;height:2px\"/></dt>");
            }
            Response.Write("</dl>");
            Response.End();
        }
        #endregion
        #region "获取套餐信息"
        /// <summary>
        /// 获取套餐信息
        /// </summary>
        private void GetGroupInfo()
        {
            string GroupID = Request.Params["GroupID"];

            Data.Group bll = new Data.Group();
            Model.Group model = bll.GetGroupInfo(GroupID);
            if (model != null)
            {
                Response.Write("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:100%;padding:20px\">");
                Response.Write("<tr><td colspan=\"2\" style=\"color:#555555;font-size:20px;font-weight:bolder;\">套餐名称：" + model.GroupName + "</td></tr>");
                Response.Write("<tr><td colspan=\"2\" style=\"height:1px;\"><img src=\"themes/images/productline.png\" style=\"width:100%;height:1px\"/></td></tr>");
                Response.Write("<tr>");
                Response.Write("<td style=\"width:60%\">");

                //读取套餐信息
                string GroupImages = model.GroupImages;
                if (string.IsNullOrEmpty(GroupImages))
                {
                    GroupImages = "/themes/images/noimages.jpg";
                }
                Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                Response.Write("<tr>");
                Response.Write("<td colspan=\"4\" style=\"padding:5px 0px\"><img src=\"" + GroupImages + "\" style=\"width:400px\"/></td>");
                Response.Write("</tr>");
                Response.Write("<tr>");
                Response.Write("<td style=\"width:15%;height:25px;font-size:14px;\">套餐原价：</td>");
                Response.Write("<td style=\"width:35%;text-align:left;font-size:14px;\">￥" + model.AllPrice + "元</td>");
                Response.Write("<td style=\"width:15%;font-size:14px;\">活动价格：</td>");
                Response.Write("<td style=\"width:35%;font-size:14px;\">￥" + model.GroupPrice + "元</td>");
                Response.Write("</tr>");
                Response.Write("<tr>");
                Response.Write("<td style=\"height:25px;font-size:14px;\">适用车型：</td>");
                Response.Write("<td style=\"font-size:14px;\" colspan=\"3\">" + model.CarName + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr><td colspan=\"4\" style=\"color:#555555;font-size:20px;font-weight:bolder;\">套餐描述</td></tr>");
                Response.Write("<tr><td colspan=\"4\" style=\"height:1px;\"><img src=\"themes/images/productline.png\" style=\"width:100%;height:1px\"/></td></tr>");
                Response.Write("<tr>");
                Response.Write("<td colspan=\"4\" style=\"padding:10px\">" + model.GroupRemarks + "</td>");
                Response.Write("</tr>");
                Response.Write("</table>");



                Response.Write("</td>");
                Response.Write("<td style=\"width:40%\">");
                //读取套餐精品

                Response.Write("</td>");
                Response.Write("</tr>");
                Response.Write("</table>");
            }
            Response.End();
            //Response.Write("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:100%;padding:20px\">");
            //Response.Write("<tr>");
            //Response.Write("<td style=\"width:20%;background:#FF0000;padding-bottom:2px;\" rowspan=\"2\" valign=\"top\">");
            //Response.Write("</td>");
            //Response.Write("<td colspan=\"2\" valign=\"top\" style=\"width:85%\"><div class=\"header\"><h1>" +  + "</h1></div></td>");
            //Response.Write("</tr>");
            //Response.Write("<tr>");
            //Response.Write("<td style=\"width:60%;background:#FF9900;padding-bottom:2px;\" valign=\"top\">");
            //Response.Write("<div><img src=\"/themes/images/noimages.jpg\" style=\"width:500px\"/></div>");
            //Response.Write("</td>");
            ////读取套餐精品
            //Response.Write("<td style=\"width:20%;background:#990000;text-align:right\" valign=\"top\">");
            //Data.Product bll = new Data.Product();
            //DataTable Dt = bll.GetGroupProduct(GroupID);
            //foreach (DataRow Row in Dt.Rows)
            //{
            //    string ImagesUrl = Row["ImagesUrl"].ToString();
            //    if (string.IsNullOrEmpty(ImagesUrl))
            //    {
            //        ImagesUrl = "/themes/images/noimages.jpg";
            //    }
            //    Response.Write("<img src=\"" + ImagesUrl + "\">");
            //}
            //Response.Write("</td>");
            //Response.Write("</tr>");
            //Response.Write("</table>");
        }
        #endregion
        #region "获取套餐商品"
        /// <summary>
        /// 获取套餐列表
        /// </summary>
        private void GetGroupProduct()
        {
            /*string GroupID = Request.Params["GroupID"];

            string GroupName = new Data.Group().GetGroupName(GroupID);
            Response.Write("<div class=\"header\"><h1>套餐名称：" + GroupName + "</h1></div>");

            Data.Product bll = new Data.Product();
            DataTable Dt = bll.GetGroupProduct(GroupID);
            foreach (DataRow Row in Dt.Rows)
            {
                Response.Write("<div class=\"list\">");

                string ImagesUrl = Row["ImagesUrl"].ToString();
                if (string.IsNullOrEmpty(ImagesUrl))
                {
                    ImagesUrl = "/themes/images/noimages.jpg";
                }

                Response.Write("<h4><a href=\"javascript:void();\" onclick=\"GetProductInfo('"+ Row["TypeID"] +"');\"><img src=\"" + ImagesUrl + "\"></a></h4>");
                Response.Write("<h2>精品名称：<a href=\"javascript:void();\" onclick=\"GetProductInfo('" + Row["TypeID"] + "');\">" + Row["FullName"] + "</a></h2>");
                Response.Write("<h2>精品编码：" + Row["UserCode"] + "元</h2>");
                Response.Write("<h2>品牌名称：" + Row["BrandName"] + "</h2>");
                Response.Write("</div>");
            }
            Response.Write("<div class=\"bottom\">&nbsp;</div>");
            Response.End();*/
        }
        #endregion
    }
}