using CNVP.Framework.Utils;
using CNVP.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.Appli
{
    public partial class TypeEdit : AdminPage
    {
        public string CurrentID, TypeName, TypeContent = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentID = Request.Params["ID"];
                if (string.IsNullOrEmpty(CurrentID) || !Public.IsNumber(CurrentID))
                {
                    CurrentID = "0";
                }
                Data.Type bll = new Data.Type();
                DataTable Dt = bll.GetTypeInfo(Convert.ToInt32(CurrentID));
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    TypeName = Dt.Rows[0]["TypeName"].ToString();
                    TypeContent = Dt.Rows[0]["TypeContent"].ToString();
                }

                if (Request.Params["Action"] == "EditInfo")
                {
                    string ID = Request.Params["ID"];
                    string TypeName = Request.Params["TypeName"];
                    string TypeContent = Request.Params["TypeContent"];

                    Data.Type bll1 = new Data.Type();
                    Model.Type model1 = new Model.Type();

                    model1.ID = Convert.ToInt32(ID);
                    model1.TypeName = TypeName;
                    model1.TypeContent = TypeContent;
                    bll.EditTypeInfo(model1);

                    Response.Write("<script>var win = parent || window;win.LG.closeAndReloadParent(null, 'TypeList');</script>");
                    Response.End();
                }
            }
        }
    }
}