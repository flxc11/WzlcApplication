using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using CNVP.UI;
using CNVP.Framework.Utils;
using CNVP.Config;
using CNVP.Framework.Helper;

namespace CNVP.Admin
{
    public partial class FilesList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Files");

            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetFilesType":
                    GetFilesType();
                    break;
                case "GetFilesList":
                    GetFilesList();
                    break;
                case "DelFiles":
                    DelFiles();
                    break;
            }
        }
        #region "获取资源类型"
        /// <summary>
        /// 获取资源类型
        /// </summary>
        private void GetFilesType()
        {
            StringBuilder Str = new StringBuilder();
            Data.FileSources bll = new Data.FileSources();
            DataTable Dt = bll.GetFilesType();
            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"Name\":\"" + Row["Name"].ToString() + "\",\"Value\":\"" + Row["Value"].ToString() + "\"},");
            }
            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("[{\"Name\":\"资源类型\",\"children\":[" + ReturnStr + "]}]");
            Response.End();
        }
        #endregion
        #region "获取资源列表"
        /// <summary>
        /// 获取资源列表
        /// </summary>
        private void GetFilesList()
        {
            string PageNo = Request.Params["Page"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            string PageSize = Request.Params["PageSize"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageSize = "20";
            }
            string SortName = Request.Params["SortName"];
            if (string.IsNullOrEmpty(SortName))
            {
                SortName = "FilesID";
            }
            string SortOrder = Request.Params["SortOrder"];
            if (string.IsNullOrEmpty(SortOrder))
            {
                SortOrder = "Desc";
            }

            int RecordCount, PageCount;
            string StrWhere = "" + DbConfig.Prefix + "Type As B INNER JOIN " + DbConfig.Prefix + "FileSources As A ON B.TypeID=A.TypeID Where 1=1";

            string SearchType = Request.Params["SearchType"];
            string Keyword = Request.Params["Keyword"];
            if (!string.IsNullOrEmpty(Keyword))
            {
                switch (SearchType)
                {
                    case "FileName":
                        StrWhere += string.Format(" And A.FilesName Like '%{0}%'", Keyword);
                        break;
                    case "FullName":
                        StrWhere += string.Format(" And B.FullName Like '%{0}%'", Keyword);
                        break;
                }
            }
            string FilesType = Request.Params["FilesType"];
            if ((!string.IsNullOrEmpty(FilesType)) && (Public.IsNumber(FilesType)))
            {
                StrWhere += " And A.FilesType=" + FilesType; 
            }

            StringBuilder Str = new StringBuilder();
            DataTable Dt = DbHelper.ExecutePage("A.*,B.FullName", StrWhere, SortName, "Order By ID " + SortOrder + "", Convert.ToInt32(PageNo), Convert.ToInt32(PageSize), out RecordCount, out PageCount);

            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"FilesID\":" + Row["FilesID"] + ",\"FilesType\":\""+ Row["FilesType"] +"\",\"TypeID\":\"" + Row["TypeID"] + "\",\"FilesName\":\"" + Row["FilesName"] + "\",\"FilesUrl\":\"" + Row["FilesUrl"] + "\",\"PostTime\":\"" + Public.GetDateTime(Row["PostTime"].ToString()) + "\",\"FullName\":\"" + Public.FilterJson(Row["FullName"].ToString()) + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + RecordCount + "\"}");
            Response.End();
        }
        #endregion
        #region "删除文件资源"
        /// <summary>
        /// 删除文件资源
        /// </summary>
        private void DelFiles()
        {
            string ID = Request.Params["ID"];
            foreach (string a in ID.Split(','))
            {
                string FilesUrl = string.Empty;
                Data.FileSources bll = new Data.FileSources();
                Model.FileSources model = bll.GetFilesInfo(Convert.ToInt32(a));
                if (model != null)
                {
                    FilesUrl = model.FilesUrl;
                }

                //执行删除操作
                bll.DelFileSources(Convert.ToInt32(a));
            }
            Response.Write("{\"msgCode\":\"0\",\"msgStr\":\"恭喜，资源文件删除操作成功。\"}");
            Response.End();
        }
        #endregion
    }
}