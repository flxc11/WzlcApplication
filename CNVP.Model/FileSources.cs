using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class FileSources
    {
        /// <summary>
        /// 文件序号
        /// </summary>
        public int FilesID { get; set; }
        /// <summary>
        /// 门店序号
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 用户序号
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FilesType { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FilesName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilesUrl { get; set; }
        /// <summary>
        /// 是否审核(0-未审核，1-已审核)
        /// </summary>
        public int IsLock { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 关联商品的TypeID
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 图片排序
        /// </summary>
        public int OrderID { get; set; }
    }
}