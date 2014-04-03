using System;
using System.Collections.Generic;
using System.Text;
using CNVP.Framework.Utils;

namespace CNVP.UI
{
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        ///  页面访问前调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Int64 ExpireTime = Convert.ToInt64(Convert.ToDateTime("2024-05-15 00:00:00").ToString("yyyyMMddHHmmss"));
            Int64 TodayTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));

            if (ExpireTime <= TodayTime)
            {
                MessageUtils.Show("软件试用已经到期，请与我们联系：400-7111-263，谢谢！");
                Response.End();
            }
        }
    }
}