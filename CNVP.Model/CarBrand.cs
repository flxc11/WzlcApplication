using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class CarBrand
    {
        /// <summary>
        /// 品牌序号
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string BrandRemarks { get; set; }
        /// <summary>
        /// 品牌排序
        /// </summary>
        public int OrderID { get; set; }
    }
}