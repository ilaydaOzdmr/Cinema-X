using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema_X
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnMasterSearch_Click(object sender, EventArgs e)
        {
            string searchTermu = txtMasterSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTermu))
            {
                Response.Redirect($"UserHomePage.aspx?search={searchTermu}");
            }
        }

    }
}