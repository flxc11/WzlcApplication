using CNVP.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin
{
    public partial class AppReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["action"] == "reg")
                {
                    string AppName = Request.Form["talname"];
                    string AppCardID = Request.Form["talidcard"];
                    string AppPhone = Request.Form["taltel"];
                    string AppAddress = Request.Form["taladdress"];
                    string AppEmail = Request.Form["talemail"];
                    string CheckCode = Request.Form["CheckCode"];
                    Data.UsersSmsData bll = new Data.UsersSmsData();
                    if (bll.CheckLogin2(AppName, AppCardID))
                    {
                        Response.Write("<script>alert('此账号已存在，请直接登录！');this.location.href='Appsearch.aspx';</script>");
                        Response.End();
                    }
                    else
                    {
                        if (bll.CheckLogin(AppPhone, CheckCode))
                        {
                            Model.AppliUsers model = new Model.AppliUsers();
                            Data.AppliUsers bll1 = new Data.AppliUsers();
                            model.AppName = AppName;
                            model.AppCardID = AppCardID;
                            model.AppPhone = AppPhone;
                            model.AppEmail = AppEmail;
                            model.PostTime = DateTime.Now;
                            model.AppAddress = AppAddress;

                            bll1.AddAppUsers(model);
                            HttpCookie _Cookie = new HttpCookie("CNVP_HD_Users");
                            string StrEncrypTo = EncryptUtils.EncodeCookies(AppCardID);
                            _Cookie.Value = StrEncrypTo;
                            HttpContext.Current.Response.Cookies.Add(_Cookie);
                            Response.Write("<script>alert('注册成功！');this.location.href='Appli2.aspx';</script>");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("<script>alert('验证码错误，请重新输入！');this.location.href='AppReg.aspx';</script>");
                            Response.End();
                        }
                    }                    
                }
            }
        }
    }
}