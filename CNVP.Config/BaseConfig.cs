using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

namespace CNVP.Config
{
    public class BaseConfig
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string FilePath = GetMapPath("CNVP.CMS");
        /// <summary>
        /// 得到配置文件
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static string GetConfigParamvalue(string Item)
        {
            return string.Empty;
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public static string GetConfigValue(string Target)
        {
            return GetConfigValue(Target, FilePath);
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="ConfigPathName"></param>
        /// <returns></returns>
        public static string GetConfigValue(string Target, string XmlPath)
        {
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(XmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(Target);
            try
            {
                return elemList[0].InnerText;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="strTarget"></param>
        /// <param name="strValue"></param>
        public static void SaveXmlConfig(string strTarget, string strValue)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(FilePath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName(strTarget);
                elemList[0].InnerXml = strValue;
                xdoc.Save(FilePath);
            }
            catch
            {
                AddXmlConfig(strTarget, strValue);
            }
        }
        /// <summary>
        /// 增加Xml节点
        /// </summary>
        /// <param name="strTarget"></param>
        /// <param name="strValue"></param>
        private static void AddXmlConfig(string strTarget, string strValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            XmlNode root = xmlDoc.SelectSingleNode("YongDian");
            XmlElement xe1 = xmlDoc.CreateElement(strTarget);
            xe1.InnerText = strValue;
            root.AppendChild(xe1);
            xmlDoc.Save(FilePath);
        }
        /// <summary>
        /// 刷新缓存
        /// </summary>
        public static string GetCatchParam(string Target)
        {
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(FilePath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(Target);
            try
            {
                return elemList[0].InnerText;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetMapPath(string FileName)
        {
            string StrPath = string.Format("/Config/{0}.config", FileName);

            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(StrPath);
            }
            else //非web程序引用
            {
                StrPath = StrPath.Replace("/", "\\");
                if (StrPath.StartsWith("\\"))
                {
                    StrPath = StrPath.Substring(StrPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StrPath);
            }
        }
    }
}