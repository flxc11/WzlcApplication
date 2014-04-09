using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class CarProduct
    {
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 品牌序号
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// 车型序号
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// 精品序号
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 指导价格
        /// </summary>
        public decimal GuidePrice { get; set; }
        /// <summary>
        /// 精品排序
        /// </summary>
        public int OrderID { get; set; }
    }
}