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
    public partial class Application : System.Web.UI.Page
    {
        public string _UserID, _AppName, _AppCardID, _AppPhone, _AppAddress, _AppEmail, _TypeList, _TypeContentList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie Cookie = HttpContext.Current.Request.Cookies["CNVP_HD_Users"];
                if (Cookie != null)
                {
                    try
                    {
                        string Str = Cookie.Value;
                        string StrDecrypTo = EncryptUtils.DecodeCookies(Str);
                        string[] StrInfo = StrDecrypTo.Split('|');
                        _UserID = StrInfo[0];
                        Data.AppliUsers bll = new Data.AppliUsers();
                        Model.AppliUsers model = bll.GetUserInfo(Convert.ToInt32(_UserID));
                        if (model != null)
                        {
                            _AppName = model.AppName;
                            _AppCardID = model.AppCardID;
                            _AppPhone = model.AppPhone;
                            _AppAddress = model.AppAddress;
                            _AppEmail = model.AppEmail;
                        }

                        Data.Type bll1 = new Data.Type();
                        List<Model.Type> model1 = bll1.GetAppList("");
                        if (model1 != null)
                        {
                            _TypeList = "<option value=\"0\">请选择申请事项</option>";
                            foreach(var item in model1)
                            {
                                _TypeList += "<option value=\"" + item.ID + "\">" + item.TypeName + "</option>";
                                _TypeContentList += item.TypeContent + "|";
                            }
                            _TypeContentList = _TypeContentList.Substring(0, _TypeContentList.Length - 1);
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    Response.Redirect("appsearch.aspx");
                    Response.End();
                }
                if (Request.Params["action"] == "reg")
                {
                    string AppType = Request.Form["app_type"];
                    string AppUserID = _UserID;
                    string AppPic = FileUpload();
                    string AppResult = Request.Form["app_result"];
                    string AppContent = Request.Form["AppContent"];
                    Model.Application model = new Model.Application();
                    Data.Application bll = new Data.Application();
                    model.AppType = AppType;
                    model.AppUserID = AppUserID;
                    model.AppPic = AppPic;
                    model.AppResult = AppResult;
                    model.PostTime = DateTime.Now;
                    model.AppContent = AppContent;
                    model.IsAudit = "0";

                    bll.AddApplication(model);
                    Response.Write("<script>alert('您的申请已提交，请等待管理员审核！');this.location.href='Application.aspx';</script>");
                    Response.End();
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