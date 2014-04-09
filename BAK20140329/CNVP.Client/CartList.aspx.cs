using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.Client
{
    public partial class CartList : System.Web.UI.Page
    {
        Data.CartItem Cart = new Data.CartItem("ShopCart");
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "IsEmpty":
                    IsEmpty();
                    break;
                case "ShowCart":
                    ShowCart();
                    break;
                case "AddGroup":
                    AddGroup();
                    break;
                case "AddProduct":
                    AddProduct();
                    break;
                case "Update":
                    Update();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "DeleteAll":
                    DeleteAll();
                    break;
            }
        }
        #region "是否为空"
        /// <summary>
        /// 是否为空
        /// </summary>
        private void IsEmpty()
        {
            bool flg = false;
            ICollection ProductList = Cart.CartItems;
            if (ProductList.Count > 0)
            {
                flg = true;
            }
            Response.Write("{\"msgCode\":\"" + flg + "\"}");
            Response.End();
        }
        #endregion
        #region "购物清单"
        /// <summary>
        /// 购物清单
        /// </summary>
        private void ShowCart()
        {
            string JSON = string.Empty;
            ICollection ProductList = Cart.CartItems;
            if (ProductList.Count > 0)
            {
                JSON = SerializeHelper.ToJson(ProductList);
            }
            Response.Write(JSON);
            Response.End();
        }
        #endregion
        #region "套餐加入"
        /// <summary>
        /// 套餐加入
        /// </summary>
        private void AddGroup()
        {
            string GroupID = Request.Params["GroupID"];

            Data.Group bll = new Data.Group();
            Model.Group model = bll.GetGroupInfo(GroupID);
            if (model != null)
            {
                Data.ProductDetail ShopItem = new Data.ProductDetail();
                ShopItem.ProductID = GroupID;
                ShopItem.ProductName = model.GroupName;
                ShopItem.ProductImages = model.GroupImages;
                ShopItem.ProductPrice = model.GroupPrice.ToString();
                ShopItem.ProductAmount = 1;
                ShopItem.ProductUnit = "套";
                ShopItem.IsGroup = true;
                ShopItem.PostTime = DateTime.Now.ToString();

                Cart.AddItem(ShopItem, true);
            }

            Response.Write("{\"msgCode\":\"1\",\"msgStr\":\"成功添加到购物车。\"}");
            Response.End();
        }
        #endregion
        #region "商品加入"
        /// <summary>
        /// 商品加入
        /// </summary>
        private void AddProduct()
        {
            string ProductID = Request.Params["ProductID"];
            
            Data.Product bll = new Data.Product();
            Model.Type model = bll.GetProductInfo(ProductID);
            if (model != null)
            {
                Data.ProductDetail ShopItem = new Data.ProductDetail();
                ShopItem.ProductID = ProductID;
                ShopItem.ProductName = model.FullName;
                ShopItem.ProductPrice = model.GuidePrice.ToString();
                ShopItem.ProductAmount = 1;
                ShopItem.ProductUnit = "件";
                ShopItem.IsGroup = false;
                ShopItem.PostTime = DateTime.Now.ToString();
                Cart.AddItem(ShopItem, true);
            }

            Response.Write("{\"msgCode\":\"1\",\"msgStr\":\"成功添加到购物车。\"}");
            Response.End();
        }
        #endregion
        #region "更新数量"
        /// <summary>
        /// 更新购物车数量
        /// </summary>
        private void Update()
        {
            string ProductID = Request.Params["ProductID"];
            string ProductNum=Request.Params["ProductNum"];
            if (!string.IsNullOrEmpty(ProductID))
            {
                if (string.IsNullOrEmpty(ProductNum) || (!Public.IsNumber(ProductNum)))
                {
                    ProductNum = "1";
                }
                Cart.UpdateItem(ProductID, Convert.ToInt32(ProductNum));
            }
            Response.Write("{\"msgCode\":\"1\",\"msgStr\":\"精品数量更新成功。\"}");
            Response.End();
        }
        #endregion
        #region "移除商品"
        #endregion
        #region "删除精品"
        /// <summary>
        /// 删除精品
        /// </summary>
        private void Delete()
        {
            string ProductID = Request.Params["ProductID"];
            if (!string.IsNullOrEmpty(ProductID))
            {
                Cart.RemoveItem(ProductID, true);
            }

            Response.Write("{\"msgCode\":\"1\",\"msgStr\":\"精品删除操作成功。\"}");
            Response.End();
        }
        #endregion
        #region "清空精品"
        /// <summary>
        /// 清空精品
        /// </summary>
        private void DeleteAll()
        {
 
        }
        #endregion
    }
}