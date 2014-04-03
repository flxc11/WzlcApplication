using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Client
{
    public partial class ProductImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ID = Request.Params["ID"];
                string Type = Request.Params["Type"];

                switch (Type)
                {
                    case "GroupInfo": 
                        GroupImages();
                        break;
                }
            }
        }
        #region "套餐精品图片"
        /// <summary>
        /// 套餐精品图片
        /// </summary>
        private void GroupImages()
        {
            string ID = Request.Params["ID"];
            if (!string.IsNullOrEmpty(ID))
            {
                //获取标题名称
                Model.Group model=new Data.Group().GetGroupInfo(ID);
                string GroupName = model.GroupName;
                LitFullName.Text = string.Format("套餐名称：{0}", GroupName);

                StringBuilder Str = new StringBuilder();
                Data.Product bll = new Data.Product();
                DataTable Dt = bll.GetGroupProduct(ID);

                foreach (DataRow Row in Dt.Rows)
                {
                    string ImagesUrl = Row["ImagesUrl"].ToString();
                    string FullName = Row["FullName"].ToString();
                    if (string.IsNullOrEmpty(ImagesUrl))
                    {
                        ImagesUrl = "Images/NoImages.jpg";
                    }
                    Str.AppendFormat("<li><a href=\"#\"><img src=\"{0}\" data-large=\"{0}\" data-description=\"{1}\" /></a></li>\r\n", ImagesUrl, FullName);
                }
                LitImagesUrl.Text = Str.ToString();
            }
        }
        #endregion
    }
}