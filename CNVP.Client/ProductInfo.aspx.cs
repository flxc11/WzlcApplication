using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.Client
{
    public partial class ProductInfo : System.Web.UI.Page
    {
        public string TypeID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TypeID = Request.Params["TypeID"];
                if (!string.IsNullOrEmpty(TypeID))
                {
                    //读取精品信息
                    Data.Product bll = new Data.Product();
                    Model.Type model = bll.GetProductInfo(TypeID);
                    if (model != null)
                    {
                        LitFullName.Text = model.FullName;
                        LitGuidePrice.Text = model.GuidePrice.ToString();
                        LitUserCode.Text = model.UserCode;
                        LitEntryCode.Text = model.EntryCode;
                        LitPyCode.Text = model.PyCode;
                        LitBrandName.Text = model.BrandName;

                        //读取车型信息
                        LitCarName.Text = bll.GetCarName(TypeID);
                        //读取精品分类
                        LitTypeName.Text = bll.GetTypeName(model.ParID);

                        //读取精品图片
                        GetImagesUrl(TypeID, model.ImagesUrl);
                        //读取颜色图片
                        GetColorsUrl(TypeID);
                        //读取款式图片
                        GetStyleUrl(TypeID);
                    }
                }
            }
        }
        #region "读取精品图片"
        /// <summary>
        /// 读取精品图片
        /// </summary>
        /// <param name="TypeID">精品类别</param>
        /// <param name="FocusPic">焦点图片</param>
        private void GetImagesUrl(string TypeID,string FocusPic)
        {
            StringBuilder Str = new StringBuilder();
            Data.Product bll = new Data.Product();
            List<Model.FileSources> model = new List<Model.FileSources>();
            model = bll.GetProductPic(TypeID, 1);

            foreach (Model.FileSources m in model)
            {
                string CssClass = string.Empty;
                if (m.FilesUrl == FocusPic)
                {
                    CssClass = "current";
                }
                Str.AppendFormat("<li id=\"{2}\" class=\"{1}\"><a href=\"javascript:void()\" onclick=\"ChangeImagesUrl('{2}');\"><img src=\"http://127.0.0.1/{0}\" align=\"absmiddle\"/></a></li>", m.FilesUrl, CssClass, "ImagesUrl_" + m.FilesID);
            }

            LitImagesUrl.Text = string.Format("<div class=\"ProductImagesUrl\"><button class=\"prev\" type=\"button\" id=\"LeftBtn\"></button><div class=\"Pic_List\" id=\"Movie_Box\"><ul id=\"Scroll_Container\">{0}</ul></div><button class=\"next over\" type=\"button\" id=\"RightBtn\"></button></div>", Str.ToString());
        }
        #endregion
        #region "读取颜色图片"
        /// <summary>
        /// 读取颜色图片
        /// </summary>
        /// <param name="TypeID">精品类别</param>
        private void GetColorsUrl(string TypeID)
        {
            StringBuilder Str = new StringBuilder();
            Data.Product bll = new Data.Product();
            List<Model.FileSources> model = new List<Model.FileSources>();
            model = bll.GetProductPic(TypeID, 2);

            foreach (Model.FileSources m in model)
            {
                Str.AppendFormat("<li id=\"{1}\"><a href=\"javascript:void()\" onclick=\"ChangeImagesUrl('{1}');\"><img src=\"http://127.0.0.1/{0}\" align=\"absmiddle\"/></a></li>", m.FilesUrl, "ColorsUrl_" + m.FilesID);
            }

            LitColorsUrl.Text = string.Format("<div class=\"ProductColorsUrl\"><div class=\"Colors_List\"><ul>{0}</ul></div></div>", Str.ToString());
        }
        #endregion
        #region "读取款式图片"
        /// <summary>
        /// 读取款式图片
        /// </summary>
        /// <param name="TypeID">精品类别</param>
        private void GetStyleUrl(string TypeID)
        {
            StringBuilder Str = new StringBuilder();
            Data.Product bll = new Data.Product();
            List<Model.FileSources> model = new List<Model.FileSources>();
            model = bll.GetProductPic(TypeID, 3);

            foreach (Model.FileSources m in model)
            {
                Str.AppendFormat("<li id=\"{1}\"><a href=\"javascript:void()\" onclick=\"ChangeImagesUrl('{1}');\"><img src=\"http://127.0.0.1/{0}\" align=\"absmiddle\"/></a></li>", m.FilesUrl, "StyleUrl_" + m.FilesID);
            }

            LitStyleUrl.Text = string.Format("<div class=\"ProductStyleUrl\"><div class=\"Style_List\"><ul>{0}</ul></div></div>", Str.ToString());
        }
        #endregion
    }
}