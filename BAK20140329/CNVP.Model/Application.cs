using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Application
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 申请类型
        /// </summary>
        public string AppType { get; set; }
        /// <summary>
        /// 上传材料
        /// </summary>
        public string AppPic { get; set; }
        /// <summary>
        /// 结果需要
        /// </summary>
        public string AppResult { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime PostTime { get; set; }
        /// <summary>
        /// 是否审核通过，0表示未通过，1表示已通过
        /// </summary>
        public string IsAudit { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string AppContent { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string AppUserID { get; set; }
        /// <summary>
        /// 管理员回复
        /// </summary>
        public string AppReply { get; set; }
    }
}
