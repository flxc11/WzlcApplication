using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Cache;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using System.Reflection;
using System.Data.OleDb;

namespace CNVP.Admin
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Action = Request.Params["Action"];
                switch (Action)
                {
                    case "ClearCache":
                        ClearCache();
                        break;
                    case "SendSms":
                        SendSms();
                        break;
                }
                Response.End();
            }
        }

        #region 创建服务
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <returns></returns>
        public static cn.b2m.eucp.sdkhttp.SDKService CreateService()
        {
            cn.b2m.eucp.sdkhttp.SDKService _Service = new cn.b2m.eucp.sdkhttp.SDKService();
            return _Service;
        }
        #endregion

        #region "清除全部缓存"
        /// <summary>
        /// 清除全部缓存
        /// </summary>
        private void ClearCache()
        {
            BaseCache Cache = BaseCache.GetCacheService();
            foreach (FieldInfo Field in typeof(CacheKeys).GetFields())
            {
                string CacheValues = Field.GetValue(null).ToString();
                Cache.RemoveObject(CacheValues);
            }
            Response.Write("{msgCode:1,msgStr:'对象缓存清除操作成功。'}");
            Response.End();
        }
        #endregion
        #region "执行导入操作"
        private void Import()
        {
            string FileName = Server.MapPath("~/UploadFile/Type.xls");
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=Excel 8.0;";
            //链接Excel
            OleDbConnection cnnxls = new OleDbConnection(strConn);
            //读取Excel里面有 表Sheet1
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from [p$]", cnnxls);
            DataSet ds = new DataSet();
            //将Excel里面有表内容装载到内存表中！
            oda.Fill(ds);
            DataTable dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string StrSql = "Insert Into " + DbConfig.Prefix + "A8Type (TypeID,ParID,UserCode,FullName,EntryCode,PyCode) Values (@TypeID,@ParID,@UserCode,@FullName,@EntryCode,@PyCode)";
                IDataParameter[] Param = new IDataParameter[] { 
                    DbHelper.MakeParam("TypeID",dt.Rows[i]["TypeID"].ToString()),
                    DbHelper.MakeParam("ParID",dt.Rows[i]["ParID"].ToString()),
                    DbHelper.MakeParam("UserCode",dt.Rows[i]["UserCode"]),
                    DbHelper.MakeParam("FullName",dt.Rows[i]["FullName"]),
                    DbHelper.MakeParam("EntryCode",dt.Rows[i]["EntryCode"]),
                    DbHelper.MakeParam("PyCode",dt.Rows[i]["PyCode"])
                };
                DbHelper.ExecuteNonQuery(StrSql, Param);
            }
        }
        #endregion

        private void SendSms()
        {
            string userPhone = Public.FilterSql(Request.Params["UserPhone"]);
            string[] SmsPhone = new string[] { userPhone };
            string SerialNum = "3SDK-EMY-0130-JCXMM";
            string key = "cnvp";
            long unknow = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            string smsCode = Rand.Number(6);
            string SmsContent = "您好！您在鹿城档案地方志网上在线申请的验证码为 " + smsCode + "，有效期为半个小时，请及时登录！退订回复TD【鹿城档案地方志网】";
            Data.UsersSmsData bll = new Data.UsersSmsData();
            Model.UsersSmsModel model = new Model.UsersSmsModel();
            model.UserID = 0;
            model.UserPhone = userPhone;
            model.SmsTitle = smsCode;
            model.SmsContent = SmsContent;
            model.PostTime = DateTime.Now.ToString();
            bll.AddUserSms(model);
            Response.Write("{\"rslt\":\"" + Ajax.CreateService().sendSMS(SerialNum, key, "", SmsPhone, SmsContent, "", "", 5, unknow) + "\"}");
            Response.End();
        }
    }
}