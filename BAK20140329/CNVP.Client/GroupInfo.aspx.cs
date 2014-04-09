using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Framework.Utils;
using CNVP.Framework.Helper;

namespace CNVP.Client
{
    public partial class GroupInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetGroupInfo":
                    GetGroupInfo();
                    break;
                case "GetGroupList":
                    GetGroupList();
                    break;
                case "GetAllProduct":
                    GetAllProduct();
                    break;
            }
        }
        #region "获取套餐列表"
        /// <summary>
        /// 获取套餐列表
        /// </summary>
        private void GetGroupList()
        {
            string GroupID = Request.Params["GroupID"];

            StringBuilder Str = new StringBuilder();
            Str.Append("<dt class=\"Title\">&nbsp;</dt>");

            Data.Group bll = new Data.Group();
            List<Model.Group> model = bll.GetGroupList(Convert.ToInt32(UIConfig.ClientID));
            foreach (Model.Group m in model)
            {
                string CssClass = string.Empty;
                if (m.GroupID == GroupID)
                {
                    CssClass = " class=\"current\"";
                }
                string ImagesUrl = m.GroupImages;
                if (string.IsNullOrEmpty(ImagesUrl))
                {
                    ImagesUrl = "Images/NoImages.jpg";
                }

                Str.Append("<dd onclick=\"GetGroupInfo('" + m.GroupID + "');\"" + CssClass + ">");
                Str.Append("<table style=\"width:200px;margin:0px auto\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                Str.Append("<tr>");
                Str.Append("<td style=\"height:50px\"><a href=\"javascript:void();\" onclick=\"GetGroupInfo('" + m.GroupID + "');\"><img src=\"" + ImagesUrl + "\" height=\"50\" style=\"border:solid 1px #ccc\" /></a></td>");
                Str.Append("<td style=\"height:30px;line-height:150%;font-size:12px;padding:0px 5px;\" valign=\"top\"><a href=\"javascript:void();\" onclick=\"GetGroupInfo('" + m.GroupID + "');\" style=\"color:#00F\">" + m.GroupName + "</a><br/>套餐价：" + m.AllPrice + "元<br/>截至时间：" + Public.GetDate(m.ExpireTime) + "</td>");
                Str.Append("</tr>");
                Str.Append("</table>");
                Str.Append("</dd>");
                Str.Append("<dt style=\"height:2px;\"><img src=\"Images/TypeBottom.png\" style=\"width:100%;height:2px\"/></dt>");
            }

            Response.Write(Str.ToString());
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
                string GroupImages = model.GroupImages;
                if (string.IsNullOrEmpty(GroupImages))
                {
                    GroupImages = "Images/NoImages.jpg";
                }
                Response.Write("{\"msgCode\":\"0\",\"GroupName\":\"" + model.GroupName + "\",\"GroupImages\":\"" + GroupImages + "\",\"GroupRemarks\":\"" + model.GroupRemarks + "\",\"GroupPrice\":\"" + model.GroupPrice + "\",\"AllPrice\":\"" + model.AllPrice + "\",\"CarName\":\"" + model.CarName + "\"}");
            }
            Response.End();
        }
        #endregion
        #region "获取更多精品"
        /// <summary>
        /// 获取更多精品
        /// </summary>
        private void GetAllProduct()
        {
            StringBuilder Str = new StringBuilder();

            string GroupID = Request.Params["GroupID"];
            int RecordCount,PageCount;
            string PageNo = Request.Params["PageNo"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            string PageSize = Request.Params["PageSize"];
            if (string.IsNullOrEmpty(PageSize) || (!Public.IsNumber(PageSize)))
            {
                PageSize = "10";
            }

            DataTable Dt = new Data.Product().GetGroupProduct(GroupID, Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);
            foreach(DataRow Row in Dt.Rows)
            {
                string ImagesUrl = Row["ImagesUrl"].ToString();
                if (string.IsNullOrEmpty(ImagesUrl))
                {
                    ImagesUrl = "Images/NoImages.jpg";
                }
                Str.Append("{\"TypeID\":\"" + Row["TypeID"] + "\",\"FullName\":\"" + Row["FullName"] + "\",\"ImagesUrl\":\"" + ImagesUrl + "\"},");
            }
            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"RecordCount\":\"" + RecordCount + "\",\"PageCount\":\"" + PageCount + "\"}");
            Response.End();
        }
        #endregion
    }
}