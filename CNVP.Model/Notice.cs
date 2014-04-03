using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Notice
    {
        public int ID { get; set; }

        public string NoticeTitle { get; set; }
        public string NoticeContent { get; set; }
        public DateTime PostTime { get; set; }
    }
}
