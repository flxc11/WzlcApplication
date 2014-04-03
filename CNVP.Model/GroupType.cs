using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class GroupType
    {
        /// <summary>
        /// 当前类别
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 所属父类
        /// </summary>
        public string ParID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string EntryCode { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string PyCode { get; set; }
        /// <summary>
        /// 子类别数量
        /// </summary>
        public int SonNum { get; set; }
        /// <summary>
        /// 子类别总数
        /// </summary>
        public int SonCount { get; set; }
    }
}