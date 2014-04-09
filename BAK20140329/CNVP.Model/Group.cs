using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Group
    {
        /// <summary>
        /// 套餐序号
        /// </summary>
        public string GroupID { get; set; }
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 商品总价
        /// </summary>
        public decimal AllPrice { get; set; }
        /// <summary>
        /// 活动价格
        /// </summary>
        public decimal GroupPrice { get; set; }
        /// <summary>
        /// 活动车型
        /// </summary>
        public string CarName { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string ExpireTime { get; set; }
        /// <summary>
        /// 套餐图片
        /// </summary>
        public string GroupImages { get; set; }
        /// <summary>
        /// 所有说明
        /// </summary>
        public string AllImagesName { get; set; }
        /// <summary>
        /// 所有图片
        /// </summary>
        public string AllImagesUrl { get; set; }
        /// <summary>
        /// 删除图片
        /// </summary>
        public string DelImagesUrl { get; set; }
        /// <summary>
        /// 套餐说明
        /// </summary>
        public string GroupRemarks { get; set; }
        /// <summary>
        /// 套餐排序
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 增加时间
        /// </summary>
        public string PostTime { get; set; }
    }
}