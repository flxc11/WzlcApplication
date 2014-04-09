using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class OrderDetail
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 商品序号
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string PriductNumber { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal ProductPrice { get; set; }
        /// <summary>
        /// 商品单位
        /// </summary>
        public string ProductUnit { get; set; }
        /// <summary>
        /// 购买件数
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 小计金额
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}