using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.Appli
{
    public partial class AppListSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Data.Type bll = new Data.Type();
                List<Model.Type> model = bll.GetAppList("");

                DropParName.Items.Add(new ListItem("所有类别", ""));

                foreach (Model.Type m in model)
                {
                    ListItem Item = new ListItem();
                    Item.Text = m.TypeName;
                    Item.Value = m.ID.ToString();
                    DropParName.Items.Add(Item);
                }

                Data.Users bll1 = new Data.Users();
                List<Model.Users> model1 = bll1.GetUserList();

                DropDownUser.Items.Add(new ListItem("所有人", ""));

                foreach (Model.Users m in model1)
                {
                    ListItem Item = new ListItem();
                    Item.Text = m.UserName;
                    Item.Value = m.UserName;
                    //Item.Value = m.UserID.ToString();
                    DropDownUser.Items.Add(Item);
                }
            }
        }
    }
}