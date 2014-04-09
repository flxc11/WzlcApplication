using CNVP.Framework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin
{
    public partial class AppReg : System.Web.UI.Page
    {
        public string _TypeList, _TypeContentList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Data.Type bll1 = new Data.Type();
                List<Model.Type> model1 = bll1.GetAppList("");
                if (model1 != null)
                {
                    _TypeList = "<option value=\"0\">请选择申请事项</option>";
                    foreach (var item in model1)
                    {
                        _TypeList += "<option value=\"" + item.ID + "\">" + item.TypeName + "</option>";
                        _TypeContentList += item.TypeContent + "|";
                    }
                    _TypeContentList = _TypeContentList.Substring(0, _TypeContentList.Length - 1);
                }

                if (Request.Params["Action"] == "reg")
                {
                    string AppName = Public.FilterSql(Request.Params["talname"]);
                    string AppCardID = Public.FilterSql(Request.Params["talidcard"]);
                    string AppPhone = Public.FilterSql(Request.Params["taltel"]);
                    string AppAddress = Public.FilterSql(Request.Params["taladdress"]);
                    string AppEmail = Request.Params["talemail"];


                    string AppType = Request.Form["app_type"];
                    string AppPic = FileUpload();
                    string AppResult = Request.Form["app_result"];
                    string AppContent = Request.Form["AppContent"];
                    string AppThings = Request.Form["AppType"];

                    Model.AppliUsers model2 = new Model.AppliUsers();
                    Data.AppliUsers bll2 = new Data.AppliUsers();
                    Data.UsersSmsData bll = new Data.UsersSmsData();

                    model2.AppName = AppName;
                    model2.AppCardID = AppCardID;
                    model2.AppPhone = AppPhone;
                    model2.AppEmail = AppEmail;
                    model2.PostTime = DateTime.Now;
                    model2.AppAddress = AppAddress;
                    if (bll.CheckLogin2(AppName, AppCardID))   //判断用户是否已经存在
                    {
                        Response.Write("<script>alert('此账号已存在，请直接登录！');this.location.href='Appsearch.aspx';</script>");
                        Response.End();
                    }
                    else
                    {
                        int AppUserID = bll2.AddAppUsers1(model2);
                        Model.Application model = new Model.Application();
                        Data.Application _appli = new Data.Application();
                        model.AppType = AppType;
                        model.AppUserID = AppUserID.ToString();
                        model.AppPic = AppPic;
                        model.AppResult = AppResult;
                        model.PostTime = DateTime.Now;
                        model.AppContent = AppContent;
                        model.AppThings = AppThings;
                        model.IsAudit = "0";

                        string _cnt = "您好！在鹿城区档案查调平台上，有1条在线申请需您处理！退订回复TD【鹿城档案地方志网】";
                        Data.Users _users = new Data.Users();
                        string _manage_phone = _users.GetUserphone();
                        Ajax _ajax = new Ajax();
                        if (_ajax.SendSms1(_manage_phone, _cnt) == "0")
                        {
                            string str = string.Format("{0}|{1}", AppUserID, AppName);
                            //创建登录授权
                            HttpCookie _Cookie = new HttpCookie("CNVP_HD_Users");
                            string StrEncrypTo = EncryptUtils.EncodeCookies(str);
                            _Cookie.Value = StrEncrypTo;
                            HttpContext.Current.Response.Cookies.Add(_Cookie);
                            _appli.AddApplication(model);
                            Response.Write("<script>alert('申请成功，管理员将会以短信回复申请结果，请耐心等待！');window.location.href='appli2.aspx';</script>");
                            Response.End();
                        }
                    }
                    

                    

                    

                }
            }
        }

        private string FileUpload()
        {
            string TempPath = "/UploadFile/App/" + DateTime.Now.ToString("yyyyMMdd");
            string str = string.Empty;
            string aa = string.Empty;
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var filepath = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    HttpPostedFile file = Request.Files[i];
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    aa += "\"" + file.ContentLength + "\"";
                    //if (!TalentFile.CheckFileExt("GIF|JPG|PNG|BMP", fileExtension.Replace(".", "")) && !string.IsNullOrEmpty(file.FileName))
                    //{
                    //    Response.Write("不允许上传" + fileExtension.Replace(".", "") + "类型的文件！");
                    //    Response.End();
                    //}
                    if (file.ContentLength > 0 && (!string.IsNullOrEmpty(file.FileName)))
                    {

                        if (!Directory.Exists(Server.MapPath(TempPath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(TempPath));
                        }
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(TempPath + "/" + filepath + fileExtension));
                        str += TempPath + "/" + filepath + fileExtension + "|$|";
                    }

                    Thread.Sleep(100);
                }
            }
            return str;
        }
    }
}