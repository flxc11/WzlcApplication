using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Framework.Utils
{
    public class MessageUtils
    {
        /// <summary>
        /// 一般输出
        /// </summary>
        /// <param name="str"></param>
        public static void Write(string str)
        {
            System.Web.HttpContext.Current.Response.Write(str);
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 显示一段自定义的输出代码
        /// </summary>
        /// <param name="MyPage"></param>
        /// <param name="strCode"></param>
        public static void ShowJS(System.Web.UI.Page MyPage, string strCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append(strCode.Trim());
            sb.Append("</script>");
            MyPage.Response.Write(sb.ToString());
        }
        /// <summary>
        /// 页面重载
        /// </summary>
        public static void Location()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("window.location.href=window.location.href;");
            sb.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 显示一个弹出窗口
        /// </summary>
        /// <param name="str"></param>
        public static void Show(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 显示一个弹出窗口，并关闭当前页
        /// </summary>
        /// <param name="str"></param>
        public static void ShowClose(string str)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=\"javascript\">\n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("window.close();\n");
            sb.Append("</script>\n");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 显示一个弹出窗口，并转向当前页(刷新)
        /// </summary>
        /// <param name="str"></param>
        public static void ShowLocation(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("window.location.href=window.location.href;\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 显示一个弹出窗口，并转向目标页(导航)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="url"></param>
        public static void ShowRedirect(string str, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("window.location.href=\"" + url.Trim() + "\";\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 显示一个弹出窗口，并转向上一页
        /// </summary>
        /// <param name="str"></param>
        public static void ShowPre(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + str.Trim() + "\");\n");
            sb.Append("history.back();\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="url"></param>
        public static void RedirectPage(string url)
        {
            string path = "http://" + System.Web.HttpContext.Current.Request.Url.Host + url;
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append(string.Format("window.location.href='{0}';", @path));
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
