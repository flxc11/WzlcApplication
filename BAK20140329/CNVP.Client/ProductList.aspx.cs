using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Framework.Utils;

namespace CNVP.Client
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetProductType":
                    GetProductType();
                    break;
                case "GetRootPath":
                    GetRootPath();
                    break;
                case "GetTypeProduct":
                    GetTypeProduct();
                    break;
            }
        }
        #region "获取精品类别"
        /// <summary>
        /// 获取精品类别
        /// </summary>
        private void GetProductType()
        {
            StringBuilder Str = new StringBuilder();

            string TypeID = Request.Params["TypeID"];
            Data.Type bll = new Data.Type();
            List<Model.Type> model = bll.GetProductType(TypeID);

            foreach (Model.Type m in model)
            {
                Str.Append("{\"TypeID\":\"" + m.TypeID + "\",\"FullName\":\"" + m.FullName + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"RecordCount\":\"" + model.Count + "\"}");
            Response.End();
        }
        #endregion
        #region "获取当前位置"
        /// <summary>
        /// 获取当前位置
        /// </summary>
        private void GetRootPath()
        {
            StringBuilder Str = new StringBuilder();
            string TypeID = Request.Params["TypeID"];
            Data.Type bll = new Data.Type();

            Response.Write("<span>&nbsp;当前位置：</span><a href=\"ProductList.aspx\">精品列表</a>" + bll.GetRootPath(Str, TypeID) + "");
            Response.End();
        }
        #endregion
        #region "获取精品列表"
        /// <summary>
        /// 获取精品列表
        /// </summary>
        private void GetTypeProduct()
        {
            StringBuilder Str = new StringBuilder();
            string TypeID = Request.Params["TypeID"];
            string CarID = Request.Params["CarID"];
            string PageNo = Request.Params["PageNo"];
            string PageSize = Request.Params["PageSize"];
            if (string.IsNullOrEmpty(CarID))
            {
                CarID = "0";
            }
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            if (string.IsNullOrEmpty(PageSize) || (!Public.IsNumber(PageSize)))
            {
                PageSize = "20";
            }

            int RecordCount, PageCount;
            Data.Product bll = new Data.Product();
            DataTable Dt = bll.GetTypeProduct(TypeID, Convert.ToInt32(CarID), Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);

            foreach (DataRow Row in Dt.Rows)
            {
                string ImagesUrl = Row["ImagesUrl"].ToString();
                if (string.IsNullOrEmpty(ImagesUrl))
                {
                    ImagesUrl = "/Images/NoImages.jpg";
                }
                Str.Append("{\"TypeID\":\"" + Row["TypeID"] + "\",\"FullName\":\"" + Row["FullName"] + "\",\"ImagesUrl\":\"" + ImagesUrl + "\",\"BrandName\":\"" + Row["BrandName"] + "\",\"EntryCode\":\"" + Row["EntryCode"] + "\",\"PyCode\":\"" + Row["PyCode"] + "\"},");
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