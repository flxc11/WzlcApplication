using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;
using CNVP.Framework.Utils;

namespace CNVP.Admin
{
    public partial class AdminInfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TxtUserName.Text = Public.GetStrUpper(UserName);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string UserPass = TxtUserPass.Text.Trim();
            string UserPass1 = TxtNewPass1.Text.Trim();

            Data.Users bll = new Data.Users();
            if (bll.CheckUserPass(UserName, EncryptUtils.MD5(UserPass)))
            {
                bll.EditUserPass(UserName, EncryptUtils.MD5(UserPass1));
                MessageUtils.Write("<script language=\"javascript\" type=\"text/javascript\">alert('恭喜，您的密码修改操作成功。');top.removeTB();</script>");
            }
            else
            {
                MessageUtils.ShowPre("原始登录密码不正确，请重新输入。");
            }
        }
    }
}