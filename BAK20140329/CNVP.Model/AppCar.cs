using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class AppCar
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
        /// 车型排序
        /// </summary>
        public int OrderID { get; set; }
    }
}