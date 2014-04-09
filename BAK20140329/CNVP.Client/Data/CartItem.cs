using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Collections;

namespace CNVP.Client.Data
{
    [Serializable]
    public class CartItem
    {
        private Hashtable cartItems = new Hashtable();
        private Hashtable deletedItems = new Hashtable();
        private string cookieName;

        public ICollection DeletedItems
        {
            get { return deletedItems.Values; }
        }

        public CartItem(string CookieName)
        {
            this.cookieName = CookieName;
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                CartItem sc = this.Load();
                this.cartItems = sc.cartItems;
                this.deletedItems = sc.deletedItems;
            }
        }

        /// <summary>
        /// 返回购物车中的所有商品
        /// </summary>
        public ICollection CartItems
        {
            get
            {
                if (cartItems != null)
                {
                    return cartItems.Values;
                }
                return new Hashtable();
            }
        }


        public decimal TotalPrice
        {
            get
            {
                decimal sum = 0;
                foreach (ProductDetail item in cartItems.Values)
                {
                    sum += ((decimal.Parse(item.ProductPrice) * item.ProductAmount));
                }
                return sum;
            }
        }

        /// <summary>
        /// 返回购物车里所有商品的数量
        /// </summary>
        public double TotalNum
        {
            get
            {
                double sum = 0;
                foreach (ProductDetail item in cartItems.Values)
                {
                    sum += item.ProductAmount;
                }
                return sum;
            }
        }

        /// <summary>
        /// 向购物车里添加某商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <param name="Name">商品名称</param>
        /// <param name="Price">商品单价</param>
        /// <param name="AutoAddQuantity">如果购物车中已经存在该商品，该商品的数量是否加1，True数量加1，False数量不变</param>
        public void AddItem(ProductDetail ShopItem, bool AutoAddQuantity)
        {
            if (cartItems == null)
            {
                cartItems = new Hashtable();
            }
            if (!cartItems.Contains(ShopItem.ProductID))
            {
                cartItems.Add(ShopItem.ProductID, ShopItem);
            }
            else
            {
                ProductDetail item = (ProductDetail)cartItems[ShopItem.ProductID];
                if (item == null)
                {
                    cartItems.Add(ShopItem.ProductID, ShopItem);
                }
                else
                {
                    if (AutoAddQuantity)
                    {
                        item.ProductAmount++;
                    }
                    cartItems[item.ProductID] = item;
                }
            }
            //保存购物车
            Save();
        }


        /// <summary>
        /// 向购物车里添加某商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <param name="Name">商品名称</param>
        /// <param name="Price">商品单价</param>
        /// <param name="AutoAddQuantity">如果购物车中已经存在该商品，该商品的数量是否加1，True数量加1，False数量不变</param>
        public void AddItem(ProductDetail ShopItem, int num)
        {
            if (cartItems == null)
            {
                cartItems = new Hashtable();
            }
            if (!cartItems.Contains(ShopItem.ProductID))
            {
                cartItems.Add(ShopItem.ProductID, ShopItem);
            }
            else
            {
                ProductDetail item = (ProductDetail)cartItems[ShopItem.ProductID];
                item.ProductAmount += num;
                item.ProductTotal = decimal.Parse(item.ProductPrice) * item.ProductAmount;
                cartItems[item.ProductID] = item;
            }
            //保存购物车
            Save();
        }

        /// <summary>
        /// 从购物车里移除某商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <param name="FullDelete">如果商品数量大于１，是否彻底从购物车中删除该种商品，true彻底从购物车中删除该种商品，false则仅将该种商品数量减一</param>
        public void RemoveItem(string ItemId, bool FullDelete)
        {
            ProductDetail item = cartItems[ItemId] as ProductDetail;
            if (deletedItems == null)
            {
                deletedItems = new Hashtable();
            }
            if (!deletedItems.ContainsKey(item.ProductID))
            {
                deletedItems.Add(item.ProductID, item);
            }
            if (FullDelete)
            {
                cartItems.Remove(ItemId);
            }
            else
            {
                item.ProductAmount--;
                if (item.ProductAmount == 0)
                {
                    cartItems.Remove(ItemId);
                }
                else
                {
                    cartItems[ItemId] = item;
                }
            }
            //保存购物车
            Save();
        }
        /// <summary>
        /// 恢复删除的商品
        /// </summary>
        /// <param name="ID"></param>
        public void ResumeItem(string ItemId)
        {
            if (deletedItems != null && deletedItems.ContainsKey(ItemId))
            {
                ProductDetail item = deletedItems[ItemId] as ProductDetail;
                this.AddItem(item, true);
                deletedItems.Remove(ItemId);
            }
            //保存购物车
            Save();
        }
        /// <summary>
        /// 修改购物车里某商品的数量
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <param name="Quantity">商品数量</param>
        public void UpdateItem(string ItemId, int Quantity)
        {
            ProductDetail item = (ProductDetail)cartItems[ItemId];
            if (Quantity > 0) //商品数量必须大于0
            {
                item.ProductAmount = Quantity;
                item.ProductTotal = Quantity * decimal.Parse(item.ProductPrice);
            }
            cartItems[ItemId] = item;
            //保存购物车
            Save();
        }


        /// <summary>
        /// 移除购物车中所有商品
        /// </summary>
        public void RemoveAllItem()
        {
            cartItems.Clear();
            //保存购物车
            Save();
        }

        /// <summary>
        /// 保存购物车清单列表(序列化到Cookie中)
        /// </summary>
        private void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            string result = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);

                byte[] byt = new byte[stream.Length];
                byt = stream.ToArray();
                result = Convert.ToBase64String(byt);
                stream.Flush();
            }

            HttpCookie hc = new HttpCookie(cookieName);
            hc.Value = HttpContext.Current.Server.UrlEncode(result);
            hc.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.Cookies.Add(hc);
        }
        /// <summary>
        /// 载入购物车清单列表
        /// </summary>
        /// <returns></returns>
        public CartItem Load()
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                string StrCartNew = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[cookieName].Value.ToString());
                byte[] bt = Convert.FromBase64String(StrCartNew);
                Stream smNew = new MemoryStream(bt);
                IFormatter fmNew = new BinaryFormatter();
                return (CartItem)fmNew.Deserialize(smNew);
            }
            return new CartItem(cookieName);
        }
    }
}