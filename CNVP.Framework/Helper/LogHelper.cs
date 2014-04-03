using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;
using CNVP.Framework.Utils;

namespace CNVP.Framework.Helper
{
    public class LogHelper
    {
        /// <summary>
        /// 系统操作日志
        /// </summary>
        /// <param name="Title">日志标题</param>
        /// <param name="Message">日志内容</param>
        public static void Write(string Title, string Message)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                FileName = HttpContext.Current.Server.MapPath("~/App_Logs/") + FileName + ".txt";
                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/App_Logs")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion
                sw.WriteLine("IP地址：" + Public.GetUserIP() + "\r");
                sw.WriteLine("标  题：" + Title + "\r");
                sw.WriteLine("内  容：" + Message);
                sw.WriteLine("时  间：" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="Str"></param>
        public static void WriteLog(string Str)
        {
            string LogFilePath = Application.StartupPath + "\\LogFiles\\";
            //判断文件目录是否存在，不存在则自动创建文件目录
            if (!Directory.Exists(Path.GetDirectoryName(LogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
            }
            //判断是否存在日志，如果存在日志文件，则自动添加日志
            if (File.Exists(LogFilePath + DateTime.Today.ToString("yyyyMMdd") + ".log"))
            {
                StreamWriter sw = new StreamWriter(LogFilePath + DateTime.Today.ToString("yyyyMMdd") + ".log", true, Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + Str);
                sw.Close();
                return;
            }
            //如果文件不存在，则创建文件后向文件添加日志
            StreamWriter sw2 = new StreamWriter(LogFilePath + DateTime.Today.ToString("yyyyMMdd") + ".log", true, Encoding.Default);
            sw2.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + Str);
            sw2.Close();
        }
    }
}