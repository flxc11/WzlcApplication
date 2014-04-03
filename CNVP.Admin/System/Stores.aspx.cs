using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Data;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.Model;
using CNVP.UI;
using System.Text;

namespace CNVP.Admin
{
    public partial class Stores : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Stores");

            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetAppList":
                    GetAppList();
                    break;
                case "GetAppState":
                    GetAppState();
                    break;
                case "GetRsaKeys":
                    GetRsaKeys();
                    break;
                case "GetAppInfo":
                    GetAppInfo();
                    break;
                case "AddAppInfo":
                    AddAppInfo();
                    break;
                case "EditAppInfo":
                    EditAppInfo();
                    break;
                case "AppIsLock":
                    AppIsLock();
                    break;
                case "DelAppInfo":
                    DelAppInfo();
                    break;
            }
        }
        #region "获取商家信息"
        /// <summary>
        /// 获取商家信息
        /// </summary>
        private void GetAppList()
        {
            StringBuilder Str = new StringBuilder();

            Data.AppList bll = new Data.AppList();
            List<Model.AppList> model = bll.GetAppList();
            foreach (Model.AppList m in model)
            {
                Str.Append("{\"AppID\":" + m.AppID + ",\"AppName\":\"" + m.AppName + "\",\"AppUrl\":\"" + m.AppUrl + "\",\"AppAddRess\":\"" + m.AppAddRess + "\",\"AppTelPhone\":\"" + m.AppTelPhone + "\",\"AppServiceTelPhone\":\"" + m.AppServiceTelPhone + "\",\"AppSosTelPhone\":\"" + m.AppSOSTelPhone + "\",\"AppFaxNumber\":\"" + m.AppFaxNumber + "\",\"AppPubKey\":\"" + m.AppPubKey + "\",\"AppPriKey\":\"" + m.AppPriKey + "\",\"IsLock\":\"" + m.IsLock + "\",\"PostTime\":\"" + Public.GetDate(m.PostTime) + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + model.Count + "\"}");
            Response.End();
        }
        #endregion
        #region "获取门店状态"
        /// <summary>
        /// 获取门店状态
        /// </summary>
        private void GetAppState()
        {
            Response.Write("[{\"text\":\"启用状态(可允许数据同步)\",\"id\":1},{\"text\":\"禁用状态(不允许数据同步)\",\"id\":0}]");
            Response.End();
        }
        #endregion
        #region "获取通讯密钥"
        /// <summary>
        /// 获取通讯密钥
        /// </summary>
        private void GetRsaKeys()
        {
            RSAHelper.RSAKey keyPair = RSAHelper.GetRASKey();
            Response.Write("{\"PubKey\":\"" + keyPair.PublicKey + "\",\"PriKey\":\"" + keyPair.PrivateKey + "\"}");
            Response.End();
        }
        #endregion
        #region "获取门店信息"
        /// <summary>
        /// 获取门店信息
        /// </summary>
        private void GetAppInfo()
        {
            string AppID=Request.Params["AppID"];
            if (string.IsNullOrEmpty(AppID) || (!Public.IsNumber(AppID)))
            {
                AppID = "0";
            }
            Data.AppList bll = new Data.AppList();
            Model.AppList model = bll.GetAppInfo(Convert.ToInt32(AppID));
            if (model!=null)
            {
                Response.Write("{\"IsError\":false,\"Message\":\"加载成功\",\"Data\":{\"AppID\":\"" + model.AppID + "\",\"AppName\":\"" + model.AppName + "\",\"AppUrl\":\"" + model.AppUrl + "\",\"AppAddRess\":\"" + model.AppAddRess + "\",\"AppTelPhone\":\"" + model.AppTelPhone + "\",\"AppServiceTelPhone\":\"" + model.AppServiceTelPhone + "\",\"AppSOSTelPhone\":\"" + model.AppSOSTelPhone + "\",\"AppFaxNumber\":\"" + model.AppFaxNumber + "\",\"AppPubKey\":\"" + model.AppPubKey + "\",\"AppPriKey\":\"" + model.AppPriKey + "\",\"IsLock\":\"" + model.IsLock + "\",\"PostTime\":\"" + Public.GetDateTime(model.PostTime) + "\"}}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"找不到该记录\",\"Data\":{}}");
            }
            Response.End();
        }
        #endregion
        #region "增加门店信息"
        /// <summary>
        /// 增加门店信息
        /// </summary>
        private void AddAppInfo()
        {
            string AppName = Request.Params["AppName"];
            string AppUrl = Request.Params["AppUrl"];
            string AppAddRess = Request.Params["AppAddRess"];
            string AppTelPhone = Request.Params["AppTelPhone"];
            string AppServiceTelPhone = Request.Params["AppServiceTelPhone"];
            string AppSOSTelPhone = Request.Params["AppSOSTelPhone"];
            string AppFaxNumber = Request.Params["AppFaxNumber"];
            string AppPubKey = Request.Params["AppPubKey"];
            string AppPriKey = Request.Params["AppPriKey"];
            string AllowIP = Request.Params["AllowIP"];
            string IsLock = Request.Params["IsLock"];

            Data.AppList bll = new Data.AppList();
            if (!bll.IsExist(AppName))
            {
                Model.AppList model = new Model.AppList();
                model.AppName = AppName;
                model.AppUrl = AppUrl;
                model.AppAddRess = AppAddRess;
                model.AppTelPhone = AppTelPhone;
                model.AppServiceTelPhone = AppServiceTelPhone;
                model.AppSOSTelPhone = AppSOSTelPhone;
                model.AppFaxNumber = AppFaxNumber;
                model.AppPriKey = AppPriKey;
                model.AppPubKey = AppPubKey;
                model.IsLock = Convert.ToInt32(IsLock);
                bll.AddAppInfo(model);

                Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"门店名称已经存在，请换个名称再进行添加操作\"}");
            }
            Response.End();
        }
        #endregion
        #region "编辑门店信息"
        /// <summary>
        /// 编辑门店信息
        /// </summary>
        private void EditAppInfo()
        {
            string AppID = Request.Params["AppID"];
            string AppName = Request.Params["AppName"];
            string AppUrl = Request.Params["AppUrl"];
            string AppAddRess = Request.Params["AppAddRess"];
            string AppTelPhone = Request.Params["AppTelPhone"];
            string AppServiceTelPhone = Request.Params["AppServiceTelPhone"];
            string AppSOSTelPhone = Request.Params["AppSOSTelPhone"];
            string AppFaxNumber = Request.Params["AppFaxNumber"];
            string AppPubKey = Request.Params["AppPubKey"];
            string AppPriKey = Request.Params["AppPriKey"];
            string AllowIP = Request.Params["AllowIP"];
            string IsLock = Request.Params["IsLock"];

            Data.AppList bll = new Data.AppList();
            if (!bll.IsExist(Convert.ToInt32(AppID), AppName))
            {
                Model.AppList model = new Model.AppList();
                model.AppID = Convert.ToInt32(AppID);
                model.AppName = AppName;
                model.AppUrl = AppUrl;
                model.AppAddRess = AppAddRess;
                model.AppTelPhone = AppTelPhone;
                model.AppServiceTelPhone = AppServiceTelPhone;
                model.AppSOSTelPhone = AppSOSTelPhone;
                model.AppFaxNumber = AppFaxNumber;
                model.AppPriKey = AppPriKey;
                model.AppPubKey = AppPubKey;
                model.IsLock = Convert.ToInt32(IsLock);
                bll.EditAppInfo(model);

                Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"门店名称已经存在，请换个名称再进行添加操作\"}");
            }
            Response.End();
        }
        #endregion
        #region "门店状态修改"
        /// <summary>
        /// 门店状态修改
        /// </summary>
        private void AppIsLock()
        {
            string IsLock = Request.Params["IsLock"];
            string AppID = Request.Params["AppID"];

            if (string.IsNullOrEmpty(IsLock) || (!Public.IsNumber(IsLock)))
            {
                IsLock = "0"; 
            }

            foreach (string a in AppID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.AppList bll = new Data.AppList();
                    bll.EditIsLock(Convert.ToInt32(a), Convert.ToInt32(IsLock));
                }
            }
            Response.End();
        }
        #endregion
        #region "删除门店信息"
        /// <summary>
        /// 删除门店信息
        /// </summary>
        private void DelAppInfo()
        {
            string AppID = Request.Params["AppID"];
            foreach (string a in AppID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.AppList bll = new Data.AppList();
                    bll.DelAppInfo(Convert.ToInt32(a));
                }
            }
            Response.End();
        }
        #endregion
    }
}