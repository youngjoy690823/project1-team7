using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace DB_Project.Account
{
    public partial class Login : Page
    {
        Controller.Controller controller = new Controller.Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if(IsValid)
            {
                string Check = controller.CheckLogin(Email.Text, Password.Text);
                if(Check!="")
                {
                    Response.Redirect("HomePage.aspx?Permission="+Check + "&Account="+Email.Text);
                }
                else
                {
                    lbl_ErrorLog.Visible = true;
                }
            }
        }
    }
}