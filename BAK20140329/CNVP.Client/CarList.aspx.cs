using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Framework.Utils;
using System.Collections;
using Jayrock.Json.Conversion;

namespace CNVP.Client
{
    public partial class CarList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetAllCarType":
                    GetAllCarType();
                    break;
                case "GetProductType":
                    GetProductType();
                    break;
                case "GetProductList":
                    GetProductList();
                    break;
            }
        }
        #region "获取所有车型列表"
        /// <summary>
        /// 获取所有车型列表
        /// </summary>
        private void GetAllCarType()
        {
            StringBuilder Str = new StringBuilder();

            Data.CarType bll = new Data.CarType();
            DataTable Dt = bll.GetAllCarType(Convert.ToInt32(UIConfig.ClientID));
            foreach (DataRow Row in Dt.Rows)
            {
                string CarImages = Row["CarImages"].ToString();
                if (string.IsNullOrEmpty(CarImages))
                {
                    CarImages = "Images/NoImages.jpg";
                }

                Str.Append("{\"CarID\":\"" + Row["CarID"] + "\",\"CarName\":\"" + Row["CarName"] + "\",\"CarImages\":\"" + CarImages + "\"},");
            }
            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"RecordCount\":\"" + Dt.Rows.Count + "\"}");
            Response.End();
        }
        #endregion
        #region "获取车型精品类别"
        /// <summary>
        /// 获取车型精品类别
        /// </summary>
        private void GetProductType()
        {
            StringBuilder Str = new StringBuilder();
            string CarID = Request.Params["CarID"];
            if (string.IsNullOrEmpty(CarID) || (!Public.IsNumber(CarID)))
            {
                CarID = "0";
            }

            Data.Type bll = new Data.Type();
            List<Model.Type> model = bll.GetCarProductType(Convert.ToInt32(UIConfig.ClientID), Convert.ToInt32(CarID));
            foreach (Model.Type m in model)
            {
                Str.Append("{\"TypeID\":\"" + m.TypeID + "\",\"FullName\":\"" + m.FullName + "\",\"ParID\":\"" + m.ParID + "\"},");
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
        #region "获取车型精品列表"
        /// <summary>
        /// 获取车型精品列表
        /// </summary>
        private void GetProductList()
        {
            StringBuilder Str = new StringBuilder();
            string CarID = Request.Params["CarID"];
            string TypeID = Request.Params["TypeID"];
            string PageNo = Request.Params["PageNo"];
            if (string.IsNullOrEmpty(CarID) || (!Public.IsNumber(CarID)))
            {
                CarID = "0";
            }
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            string PageSize = Request.Params["PageSize"];
            if (string.IsNullOrEmpty(PageSize) || (!Public.IsNumber(PageSize)))
            {
                PageSize = "10";
            }
            int RecordCount, PageCount;

            Data.Product bll = new Data.Product();
            DataTable Dt = bll.GetCarProduct(Convert.ToInt32(UIConfig.ClientID), Convert.ToInt32(CarID), TypeID, Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);
            foreach (DataRow Row in Dt.Rows)
            {
                string ImagesUrl = Row["ImagesUrl"].ToString();
                if (string.IsNullOrEmpty(ImagesUrl))
                {
                    ImagesUrl = "Images/NoImages.jpg";
                }
                Str.Append("{\"CarID\":\"" + Row["CarID"] + "\",\"TypeID\":\"" + Row["TypeID"] + "\",\"FullName\":\"" + Row["FullName"] + "\",\"UserCode\":\"" + Row["UserCode"] + "\",\"EntryCode\":\"" + Row["EntryCode"] + "\",\"PyCode\":\"" + Row["PyCode"] + "\",\"BrandName\":\"" + Row["BrandName"] + "\",\"ImagesUrl\":\"" + ImagesUrl + "\"},");
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