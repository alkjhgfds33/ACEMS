using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["id"] = null;
            Session["passwd"] = null;
            Response.Redirect("default.aspx");
        }
        catch (Exception ex)
        {
        }
    }
}