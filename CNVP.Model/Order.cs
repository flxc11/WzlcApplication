using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Order
    {
        /// <summary>
        /// 订单序号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 商户序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderTotalMoney { get; set; }
        /// <summary>
        /// 订单总件数
        /// </summary>
        public int OrderTotalNumber { get; set; }
        /// <summary>
        /// 购买者姓名
        /// </summary>
        public string BuyerName { get; set; }
        /// <summary>
        /// 购买车电话
        /// </summary>
        public string BuyTelPhone { get; set; }
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 下单人电话
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 订单状态(0-未审核，1-已完成，2-无效订单)
        /// </summary>
        public int OrderState { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string PostTime { get; set; }
    }
}