using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using System.Xml;

namespace CNVP.Data
{
    public class FileSources
    {
        #region "获取资源类型"
        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetFilesType()
        {
            XmlDocument Xml = new XmlDocument();
            Xml.Load(Public.GetMapPath("FilesType"));
            XmlNodeList RootList = Xml.SelectNodes("HongXu/FilesType");
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
        #region "读取资源信息"
        /// <summary>
        /// 读取资源信息
        /// </summary>
        /// <param name="FilesID">资源序号</param>
        /// <returns></returns>
        public Model.FileSources GetFilesInfo(int FilesID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "FileSources Where FilesID=@FilesID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@FilesID",FilesID)
            };
            return DbHelper.ExecuteReader<Model.FileSources>(StrSql, Param);
        }
        /// <summary>
        /// 读取资源信息
        /// </summary>
        /// <param name="TypeID">精品序号</param>
        /// <returns></returns>
        public Model.FileSources GetFilesInfo(string TypeID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "FileSources Where TypeID=@TypeID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@TypeID",TypeID)
            };
            return DbHelper.ExecuteReader<Model.FileSources>(StrSql, Param);
        }
        #endregion
        #region "读取资源列表"
        /// <summary>
        /// 读取资源列表
        /// </summary>
        /// <param name="TypeID">精品类别</param>
        /// <param name="FilesType">文件类型</param>
        /// <returns></returns>
        public List<Model.FileSources> GetFilesList(string TypeID,int FilesType)
        {
            string StrSql = "Select FilesID,FilesType,FilesName,FilesUrl From " + DbConfig.Prefix + "FileSources Where TypeID=@TypeID And FilesType=@FilesType Order By OrderID Asc";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@TypeID",TypeID),
                DbHelper.MakeParam("@FilesType",FilesType)
            };
            return DbHelper.ExecuteTable<Model.FileSources>(StrSql, Param);
        }
        #endregion
        #region "删除文件资源"
        /// <summary>
        /// 删除文件资源
        /// </summary>
        /// <param name="FilesID">文件序号</param>
        public void DelFileSources(int FilesID)
        {
            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //读取文件信息
            string FilesUrl = GetFilesInfo(Convert.ToInt32(FilesID)).FilesUrl;

            //删除资源文件
            string FilesSql = "Delete From " + DbConfig.Prefix + "FileSources Where FilesID=@FilesID";
            IDataParameter[] FilesParam = new IDataParameter[] { 
                DbHelper.MakeParam("@FilesID",FilesID)
            };
            StrSql.Add(FilesSql);
            Param.Add(FilesParam);

            //删除本地文件
            FileUtils.DeleteFile(HttpContext.Current.Server.MapPath(FilesUrl));

            #region "增加操作日志"
            Data.AppList bll = new Data.AppList();
            List<Model.AppList> model = bll.GetAppList(1);
            foreach (Model.AppList m in model)
            {
                //删除资源文件
                string TacticsSql = "Insert Into " + DbConfig.Prefix + "Tactics (AppID,Action,SqlInfo,FilePath,Priority,IsUpdate,PostTime) Values (@AppID,@Action,@SqlInfo,@FilePath,@Priority,@IsUpdate,@PostTime)";
                IDataParameter[] TacticsParam = new IDataParameter[] { 
                    DbHelper.MakeParam("@AppID",m.AppID),
                    DbHelper.MakeParam("@Action","UpdateSql"),
                    DbHelper.MakeParam("@SqlInfo",DbHelper.AnalyzeParam(FilesSql,FilesParam)),
                    DbHelper.MakeParam("@FilePath",""),
                    DbHelper.MakeParam("@Priority",1),
                    DbHelper.MakeParam("@IsUpdate",0),
                    DbHelper.MakeParam("@PostTime",DateTime.Now.ToString())
                };
                StrSql.Add(TacticsSql);
                Param.Add(TacticsParam);

                //删除本地文件
                TacticsSql = "Insert Into " + DbConfig.Prefix + "Tactics (AppID,Action,SqlInfo,FilePath,Priority,IsUpdate,PostTime) Values (@AppID,@Action,@SqlInfo,@FilePath,@Priority,@IsUpdate,@PostTime)";
                TacticsParam = new IDataParameter[] { 
                    DbHelper.MakeParam("@AppID",m.AppID),
                    DbHelper.MakeParam("@Action","DeleteFile"),
                    DbHelper.MakeParam("@SqlInfo",""),
                    DbHelper.MakeParam("@FilePath",FilesUrl),
                    DbHelper.MakeParam("@Priority",1),
                    DbHelper.MakeParam("@IsUpdate",0),
                    DbHelper.MakeParam("@PostTime",DateTime.Now.ToString())
                };
                StrSql.Add(TacticsSql);
                Param.Add(TacticsParam);
            }
            #endregion

            DbHelper.ExecuteSqlTran(StrSql, Param);
        }
        #endregion



        #region 增加一张图片
        /*public void AddImg(Model.FileSources m)
        {
            string StrSql = string.Format("insert into {0}FileSources (AppID,AdminID,FilesType,FilesName,FilesUrl,IsLock,PostTime,TypeID,OrderID)values(@AppID,@AdminID,@FilesType,@FilesName,@FilesUrl,@IsLock,@PostTime,@TypeID,@OrderID);", DbConfig.Prefix);
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",m.AppID),
                DbHelper.MakeParam("@AdminID",m.AdminID),
                DbHelper.MakeParam("@FilesType",m.FilesType),
                DbHelper.MakeParam("@FilesName",m.FileName),
                DbHelper.MakeParam("@FilesUrl",m.FilesUrl),
                DbHelper.MakeParam("@IsLock",1),
                DbHelper.MakeParam("@PostTime",DateTime.Now),
                DbHelper.MakeParam("@TypeID",m.TypeID),
                DbHelper.MakeParam("@OrderID",m.OrderID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }*/
        #endregion 


        #region 更新图片排序值
        public void EditSort(int FilesID,int SortID)
        {
            string StrSql = string.Format("update {0}FileSources set OrderID=@OrderID where FilesID=@FilesID;", DbConfig.Prefix);
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@OrderID",SortID),
                DbHelper.MakeParam("@FilesID",FilesID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

        #region 根据图片url删除图片数据
        public void DeleteImg(string FilesUrl)
        {
            string StrSql = string.Format("delete from {0}FileSources where FilesUrl=@FilesUrl;", DbConfig.Prefix);
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@FilesUrl",FilesUrl)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion 
    }
}