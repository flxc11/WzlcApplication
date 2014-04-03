using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class TypeProduct
    {
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 类别序号
        /// </summary>
        public string ParID { get; set; }
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