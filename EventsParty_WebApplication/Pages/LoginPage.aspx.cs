using DataAccess.Concrete;
using EncryptPass;
using EventsParty_WebApplication.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ValidationLibrary;

namespace EventsParty_WebApplication.Pages
{
    public partial class LoginPage : System.Web.UI.Page
    {
        static string action;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                action = Request.QueryString["publish"];
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var validator = new Validator();
            if (validator.isValidLogin(tbLogin.Text)
                && validator.isValidPassword(tbPass.Text))
            {
                var userManager = new DbUserManager();
                string pass = Encrypt.CalculateMD5Hash(tbPass.Text);
                if (!validator.isAvailableLogin(tbLogin.Text) &&
                    userManager.IsValidUser(tbLogin.Text, pass))
                {
                    new PageManager().SetRoles(userManager, tbLogin.Text);
                    FormsAuthentication.SetAuthCookie(tbLogin.Text, cbRememberMe.Checked);
                    
                    //If Admin
                    if (userManager.GetRoles(tbLogin.Text)[0] == new PermissionManager().GetPermission("Admin").Type)
                    {
                        Response.Redirect("~/Pages/AdminPages/StartPage.aspx");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(action))
                        {
                            string newRedirectUrl = "http://localhost:4308/Profile/Index";
                            Response.Redirect(newRedirectUrl);
                        }
                        else
                        {
                            string newRedirectUrl = "http://localhost:4308/Event/SaveEvent";
                            Response.Redirect(newRedirectUrl);
                        }
                    }
                }
                else
                {
                    lbMsg.Text = LogInErrors.AuthorizationError;
                }
            }
            else
            {
                lbMsg.Text = LogInErrors.IncorrectLoginOrPassword;
            }
        }
        
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SignUpPage.aspx");
        }
    }
}