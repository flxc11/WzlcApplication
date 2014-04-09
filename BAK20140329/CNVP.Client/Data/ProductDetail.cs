using System;
using System.Collections.Generic;
using System.Web;

namespace CNVP.Client.Data
{
    [Serializable]
    public class ProductDetail
    {
        /// <summary>
        /// 商品序号
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public string ProductPrice { get; set; }
        /// <summary>
        /// 购买件数
        /// </summary>
        public int ProductAmount { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImages { get; set; }
        /// <summary>
        /// 商品单位
        /// </summary>
        public string ProductUnit { get; set; }
        /// <summary>
        /// 商品总价
        /// </summary>
        public decimal ProductTotal { get; set; }
        /// <summary>
        /// 是否套餐(套餐：True，精品：False)
        /// </summary>
        public bool IsGroup { get; set; }
        /// <summary>
        /// 增加时间
        /// </summary>
        public string PostTime { get; set; }
    }
}