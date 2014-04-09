using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace CNVP.Client.Data
{
    public class Product
    {
        #region "读取精品信息"
        /// <summary>
        /// 读取精品信息
        /// </summary>
        /// <param name="TypeID">类别序号</param>
        /// <returns></returns>
        public Model.Type GetProductInfo(string TypeID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "Type Where TypeID=@TypeID";
            IDataParameter[] Param=new IDataParameter[]{
                DbHelper.MakeParam("@TypeID",TypeID)
            };
            return DbHelper.ExecuteReader<Model.Type>(StrSql, Param);
        }
        #endregion
        #region "读取精品车型"
        /// <summary>
        /// 读取精品车型
        /// </summary>
        /// <param name="TypeID">精品序号</param>
        /// <returns></returns>
        public string GetCarName(string TypeID)
        {
            string CarName = "所有车型";
            string StrSql="Select A.CarID,A.BrandID,A.TypeID,B.CarName,C.BrandName From ("+ DbConfig.Prefix +"CarProduct AS A INNER JOIN "+ DbConfig.Prefix +"CarType AS B ON A.CarID=B.CarID) INNER JOIN "+ DbConfig.Prefix+"CarBrand AS C ON B.BrandID=C.BrandID Where A.TypeID=@TypeID";
            IDataParameter[] Param=new IDataParameter[]{
                DbHelper.MakeParam("@TypeID",TypeID)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                CarName = Dt.Rows[0]["CarName"].ToString();
            }
            return CarName;
        }
        #endregion
        #region "获取精品分类"
        /// <summary>
        /// 获取精品分类
        /// </summary>
        /// <param name="TypeID">类别序号</param>
        /// <returns></returns>
        public string GetTypeName(string TypeID)
        {
            string TypeName = "暂未归类";
            string StrSql = "Select FullName From " + DbConfig.Prefix + "Type Where TypeID=@TypeID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@TypeID",TypeID)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                TypeName = Dt.Rows[0]["FullName"].ToString();
            }
            return TypeName;
        }
        #endregion
        #region "读取精品图片"
        /// <summary>
        /// 读取精品图片
        /// </summary>
        /// <returns></returns>
        public List<Model.FileSources> GetProductPic(string TypeID,int FilesType)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "FileSources Where TypeID=@TypeID And FilesType=@FilesType Order By OrderID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@TypeID",TypeID),
                DbHelper.MakeParam("@FilesType",FilesType)
            };
            return DbHelper.ExecuteTable<Model.FileSources>(StrSql, Param);
        }
        #endregion
        #region "获取套餐精品"
        /// <summary>
        /// 获取套餐精品
        /// </summary>
        /// <param name="GroupID">套餐序号</param>
        /// <returns></returns>
        public DataTable GetGroupProduct(string GroupID)
        {
            string StrSql = "Select A.GroupID,B.* From " + DbConfig.Prefix + "GroupProduct AS A LEFT OUTER JOIN " + DbConfig.Prefix + "Type AS B ON A.TypeID = B.TypeID Where A.GroupID=@GroupID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@GroupID",GroupID)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            return Dt;
        }
        /// <summary>
        /// 获取套餐商品
        /// </summary>
        /// <param name="GroupID">套餐序号</param>
        /// <param name="PageNo">当前页码</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <param name="PageCount">分页数</param>
        /// <returns></returns>
        public DataTable GetGroupProduct(string GroupID, int PageNo, int PageSize, out int RecordCount, out int PageCount)
        {
            string StrWhere = "" + DbConfig.Prefix + "GroupProduct AS A LEFT OUTER JOIN " + DbConfig.Prefix + "Type AS B ON A.TypeID = B.TypeID Where A.GroupID=@GroupID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@GroupID",GroupID)
            };
            DataTable Dt = DbHelper.ExecutePage("A.GroupID,B.*", StrWhere, "B.ID", "Order By A.OrderID Asc", PageNo, PageSize, out RecordCount, out PageCount, Param);
            return Dt;
        }
        #endregion
        #region "获取车型精品"
        /// <summary>
        /// 获取车型精品
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="CarID">车型序号</param>
        /// <param name="TypeID">精品类别</param>
        /// <param name="PageNo">当前页码</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <param name="PageCount">分页数</param>
        /// <returns></returns>
        public DataTable GetCarProduct(int AppID, int CarID, string TypeID, int PageNo, int PageSize, out int RecordCount, out int PageCount)
        {
            string StrWhere = "" + DbConfig.Prefix + "Type As B INNER JOIN " + DbConfig.Prefix + "CarProduct AS A ON A.TypeID=B.TypeID Where A.AppID=@AppID";
            //根据车型筛选
            if (CarID != 0)
            {
                StrWhere += " And A.CarID=@CarID";
            }
            //根据类别筛选
            if (!string.IsNullOrEmpty(TypeID))
            {
                StrWhere += " And B.TypeID Like '" + TypeID + "%'";
            }
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID),
                DbHelper.MakeParam("@CarID",CarID)
            };
            DataTable Dt = DbHelper.ExecutePage("A.CarID,A.AppID,B.TypeID,B.ParID,B.UserCode,B.FullName,B.EntryCode,B.PyCode,B.BrandName,B.ImagesUrl", StrWhere, "B.ID", "Order By B.TypeID Asc,A.OrderID Asc", PageNo, PageSize, out RecordCount, out PageCount, Param);
            return Dt;
        }
        #endregion
        #region "获取所有精品"
        /// <summary>
        /// 获取所有精品
        /// </summary>
        /// <param name="TypeID">精品类别</param>
        /// <param name="CarID">车型序号</param>
        /// <param name="PageNo">当前页码</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <param name="PageCount">分页数</param>
        /// <returns></returns>
        public DataTable GetTypeProduct(string TypeID, int CarID,int PageNo,int PageSize,out int RecordCount,out int PageCount)
        {
            string StrWhere = "" + DbConfig.Prefix + "CarProduct As B RIGHT OUTER JOIN " + DbConfig.Prefix + "Type As A ON A.TypeID=B.TypeID Where A.SonNum =0";
            if (!string.IsNullOrEmpty(TypeID))
            {
                StrWhere += string.Format(" And ParID Like '{0}%'", TypeID);
            }
            if (CarID != 0)
            {
                StrWhere += string.Format(" And B.CarID={0}", CarID);
            }
            DataTable Dt = DbHelper.ExecutePage("A.*,B.CarID,B.BrandID", StrWhere, "A.ID", "Order By A.OrderID Asc", PageNo, PageSize, out RecordCount, out PageCount);
            return Dt;
        }
        #endregion
        #region "获取通用精品"
        /// <summary>
        /// 获取通用精品
        /// </summary>
        /// <param name="TypeID">精品类别</param>
        /// <param name="PageNo">当前页码</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <param name="PageCount">分页数</param>
        /// <returns></returns>
        public DataTable GetProductList(string TypeID,int PageNo,int PageSize,out int RecordCount,out int PageCount)
        {
            string StrWhere = "" + DbConfig.Prefix + "CarProduct As B RIGHT OUTER JOIN " + DbConfig.Prefix + "Type As A ON A.TypeID=B.TypeID";
            if (!string.IsNullOrEmpty(TypeID))
            {
                StrWhere += string.Format(" And ParID Like '{0}%'", TypeID);
            }
            DataTable Dt = DbHelper.ExecutePage("A.*,B.CarID,B.BrandID", StrWhere, "A.ID", "Order By A.OrderID Asc", PageNo, PageSize, out RecordCount, out PageCount);
            return Dt;
        }
        #endregion
    }
}