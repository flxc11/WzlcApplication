using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Data;
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
                    Response.Redirect("appli1.aspx");
                    Response.End();
                }
                if (Request.Params["action"] == "reg")
                {
                    string AppType = Request.Form["app_type"];
                    string AppUserID = _UserID;
                    string AppPic = FileUpload();
                    string AppResult = Request.Form["app_result"];
                    string AppContent = Request.Form["AppContent"];
                    string AppThings = Request.Form["AppType"];
                    Model.Application model = new Model.Application();
                    Data.Application bll = new Data.Application();
                    model.AppType = AppType;
                    model.AppUserID = AppUserID;
                    model.AppPic = AppPic;
                    model.AppResult = AppResult;
                    model.PostTime = DateTime.Now;
                    model.AppContent = AppContent;
                    model.AppThings = AppThings;
                    model.IsAudit = "0";

                    DataTable dt = DbHelper.ExecuteTable("select UserName, UserMobile from HX_Users Where UserName='admin'");
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string userPhone = dt.Rows[0]["UserMobile"].ToString();
                            //string _content = "您好，鹿城档案地方志网的管理员，在 " + DateTime.Now.ToString("yyyy-MM-dd") + " ,有一条新的申请，请及时处理！退订回复TD【鹿城档案地方志网】";
                            //string rlt = SendSms1(userPhone, _content);
                            Response.Write("<script src='Lib/jquery/jquery-1.8.0.min.js'></script>");
                            Response.Write("<script>");
                            Response.Write("$.ajax({type:'post',dataType:'json',url:'Ajax.aspx?Action=Sms1&Time=' + (new Date().getTime()),data:{UserPhone:" + userPhone + "},error:function(){},success:function(d){ if (d.rslt == 0){");
                            bll.AddApplication(model);
                            Response.Write("alert('您的申请已提交，请等待管理员审核！');window.location.href='Application.aspx';");
                            
                            Response.Write("}else{");
                            Response.Write("alert('申请提交有误，请重新提交！');window.location.href='Application.aspx';");
                            Response.Write("}");
                            Response.Write("}})");
                            Response.Write("</script>");
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