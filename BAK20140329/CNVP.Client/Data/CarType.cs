using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace CNVP.Client.Data
{
    public class CarType
    {
        #region "获取门店车型列表"
        /// <summary>
        /// 获取门店所有车型
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <returns></returns>
        public DataTable GetAllCarType(int AppID)
        {
            string StrSql = "Select A.OrderID,A.AppID,B.CarID,B.AppID,B.CarName,B.CarImages From "+ DbConfig.Prefix +"AppCar As A INNER JOIN "+ DbConfig.Prefix + "CarType As B ON A.CarID=B.CarID Where A.AppID=@AppID Order By A.OrderID Asc";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID)
            };
            return DbHelper.ExecuteTable(StrSql, Param);
        }
        #endregion
    }
}