using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class GroupProduct
    {
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 套餐序号
        /// </summary>
        public string GroupID { get; set; }
        /// <summary>
        /// 商品序号
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 精品排序
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 指导价格
        /// </summary>
        public decimal GuidePrice { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string PostTime { get; set; }
    }
}