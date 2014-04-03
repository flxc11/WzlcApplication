using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using System.Xml;

namespace CNVP.Data
{
    public class SysLogs
    {
        #region "读取日志类型"
        /// <summary>
        /// 读取日志类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetLogsType()
        {
            XmlDocument Xml = new XmlDocument();
            Xml.Load(Public.GetMapPath("LogsType"));
            XmlNodeList RootList = Xml.SelectNodes("HongXu/LogType");
            DataTable _Dt = new DataTable("Result");
            _Dt.Columns.Add("Name", typeof(string));
            _Dt.Columns.Add("Value", typeof(string));
            foreach (XmlNode Xn in RootList)
            {
                DataRow _Rows = _Dt.NewRow();
                _Rows["Name"] = Xn.Attributes["Name"].InnerText;
                _Rows["Value"] = Xn.Attributes["Value"].InnerText;
                _Dt.Rows.Add(_Rows);
            }
            return _Dt;
        }
        #endregion
        #region "读取操作日志"
        /// <summary>
        /// 读取操作日志
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLogs()
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "SysLogs Order By LogsID Desc";
            return DbHelper.ExecuteTable(StrSql);
        }
        #endregion
        #region "增加操作日志"
        /// <summary>
        /// 增加操作日志
        /// </summary>
        /// <param name="model"></param>
        public void AddLogs(Model.SysLogs model)
        {
            string StrSql = "Insert Into " + DbConfig.Prefix + "SysLogs (AppID,UserID,LogType,LogTitle,LogContent,LogIP,LogTime) Values (@AppID,@UserID,@LogType,@LogTitle,@LogContent,@LogIP,@LogTime)";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@AppID",model.AppID),
                DbHelper.MakeParam("@UserID",model.UserID),
                DbHelper.MakeParam("@LogType",model.LogType),
                DbHelper.MakeParam("@LogTitle",model.LogTitle),
                DbHelper.MakeParam("@LogContent",model.LogContent),
                DbHelper.MakeParam("@LogIP",Public.GetUserIP()),
                DbHelper.MakeParam("@LogTime",DateTime.Now.ToString())
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
        #region "删除操作日志"
        /// <summary>
        /// 删除操作日志
        /// </summary>
        public void DelLogs(int LogsID)
        {
            string StrSql = "Delete From " + DbConfig.Prefix + "SysLogs Where LogsID=@LogsID And DATEDIFF(Day,LogTime,GETDATE())>=30";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@LogsID",LogsID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
        #region "清空操作日志"
        /// <summary>
        /// 清空操作日志
        /// </summary>
        public void DelAllLogs()
        {
            string StrSql = "Delete From " + DbConfig.Prefix + "SysLogs Where DATEDIFF(Day,LogTime,GETDATE())>=30";
            DbHelper.ExecuteNonQuery(StrSql);
        }
        #endregion
    }
}