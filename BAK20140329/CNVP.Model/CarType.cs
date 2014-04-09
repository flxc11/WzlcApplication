using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class CarType
    {
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 车型序号
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// 品牌序号
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// 车型名称
        /// </summary>
        public string CarName { get; set; }
        /// <summary>
        /// 车型说明
        /// </summary>
        public string CarRemarks { get; set; }
        /// <summary>
        /// 车型排序
        /// </summary>
        public int OrderID { get; set; }
    }
}