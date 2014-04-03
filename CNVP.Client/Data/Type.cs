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
    public class Type
    {
        #region "集团精品类别"
        /// <summary>
        /// 集团精品类别
        /// </summary>
        /// <param name="TypeID">精品类别序号</param>
        /// <returns></returns>
        public List<Model.Type> GetProductType(string TypeID)
        {
            string StrSql = "Select TypeID,FullName From " + DbConfig.Prefix + "Type Where AppID=1 And SonNum>0 And SonCount>0";
            if (!string.IsNullOrEmpty(TypeID))
            {
                StrSql += string.Format(" And ParID Like '{0}%'", TypeID);
            }
            StrSql += " Order By TypeID Asc,OrderID Asc";
            return DbHelper.ExecuteTable<Model.Type>(StrSql);
        }
        #endregion
        #region "获取当前位置"
        /// <summary>
        /// 获取当前位置
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public string GetRootPath(StringBuilder Str, string TypeID)
        {
            string StrSql = "Select TypeID,FullName,ParID From " + DbConfig.Prefix + "Type Where TypeID=@TypeID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@TypeID",TypeID)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                if (Dt.Rows[0]["ParID"].ToString() != "00001")
                {
                    GetRootPath(Str, Dt.Rows[0]["ParID"].ToString());
                }
                Str.Append("<a href=\"javascript:void()\" onclick=\"GetProductType('" + Dt.Rows[0]["ParID"] + "')\">" + Dt.Rows[0]["FullName"] + "</a>");
            }

            return Str.ToString();
        }
        #endregion
        #region "车型精品类别"
        /// <summary>
        /// 获取精品类别
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="CarID">车型序号</param>
        /// <returns></returns>
        public List<Model.Type> GetCarProductType(int AppID,int CarID)
        {
            List<Model.Type> model1 = new List<Model.Type>();
            string StrSql = "Select TypeID,FullName From " + DbConfig.Prefix + "Type Where SonNum>0 And SonCount>0 Order By TypeID Asc,OrderID Asc";
            List<Model.Type> model = DbHelper.ExecuteTable<Model.Type>(StrSql);
            foreach (Model.Type m in model)
            {
                if (IsExistCarProduct(AppID, CarID, m.TypeID))
                {
                    model1.Add(m);
                }
            }
            return model1;
        }
        /// <summary>
        /// 是否存在精品
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="CarID">车型序号</param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        private bool IsExistCarProduct(int AppID, int CarID, string TypeID)
        {
            bool flg = false;
            string StrSql = "Select * From " + DbConfig.Prefix + "CarProduct Where AppID=@AppID";
            if (CarID != 0)
            {
                StrSql += " And CarID=@CarID";
            }
            StrSql += string.Format(" And TypeID Like '{0}%'", TypeID);
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID),
                DbHelper.MakeParam("@CarID",CarID)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion
    }
}