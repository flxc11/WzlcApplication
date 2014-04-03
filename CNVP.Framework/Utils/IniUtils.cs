using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace CNVP.Framework.Utils
{
    public class IniUtils
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
           string lpAppName,// 指向包含 Section 名称的字符串地址
           string lpKeyName,// 指向包含 Key 名称的字符串地址
           string lpDefault,// 如果 Key 值没有找到，则返回缺省的字符串的地址
           StringBuilder lpReturnedString,// 返回字符串的缓冲区地址
           int nSize,// 缓冲区的长度
           string lpFileName
           );

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpString,
        string lpFileName
        );

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <param name="Section">节点值</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public static string GetString(string FileName,string Section, string Key)
        {
            StringBuilder Temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, Key, "", Temp, 1024, FileName);
            return Temp.ToString();
        }
        /// <summary>
        /// 是否存在文件
        /// </summary>
        /// <returns></returns>
        public static bool IsExist(string FileName)
        {
            return File.Exists(FileName);
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        public static void CreateFile(string FileName)
        {
            StreamWriter sw = new StreamWriter(FileName, true, Encoding.Default);
            sw.Close();
        }
        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="FileName"></param>
        public static void SetConfig(string FileName)
        {
            WriteFile(FileName, "Collect", "TimeSpan", "10");
            WriteFile(FileName, "Collect", "AutoRun", "true");
        }
        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <param name="Section">节点值</param>
        /// <param name="Key">键名</param>
        /// <param name="Value">键值</param>
        public static void WriteFile(string FileName, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, FileName);
        }
        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        public static void DelKey(string FileName, string Section, string Key)
        {
            WritePrivateProfileString(Section, Key, null, FileName);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="section"></param>
        public static void DelSection(string FileName, string Section)
        {
            WritePrivateProfileString(Section, null, null, FileName);
        }
    }
}