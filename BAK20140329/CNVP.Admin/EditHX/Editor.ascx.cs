using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.EditHX
{
    public partial class Editor : System.Web.UI.UserControl
    {
        protected string _txt, _fid;
        public override string ID
        {
            set { _fid = value; }
        }
        public string Value
        {
            set { _txt = Server.HtmlEncode(value); }
            get { return GetSafeStr1(Request.Form[_fid]); }
        }
        string GetSafeStr1(string str)
        {
            if (str == null || str == "") return "";
            return str.Replace("'", "&#39;").Replace("<%", "&lt;").Replace("<script", "&lt;").Replace("</script>", "&lt;script&gt;");
        }
    }
}