using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel;
using EncryptPass;
using EventsParty_WebApplication.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ValidationLibrary;


namespace EventsParty_WebApplication.Pages
{
    public partial class SignUpPage : System.Web.UI.Page
    {
        const string xMark = "&#x2718;";
        const string checkMark = "&#x2714;";
        const string loginExists = " exists";

        Validator validator = new Validator();
        IUserManager manager = new DbUserManager();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                string pass = Encrypt.CalculateMD5Hash(tbPassword.Text);
                manager.CreateNewUser(
                    new EventCustomer{
                        Login = tbLogin.Text,
                        Password = pass,
                        Name = tbName.Text,
                        LastName = tbLastName.Text,
                        Mobile =  tbMobile.Text,
                        Email = tbEmail.Text,
                        Birthday = Convert.ToDateTime(tbBirthday.Text),
                        Country = tbCountry.Text,
                        Address = tbAddress.Text
                    });
                new PageManager().SetRoles(new DbUserManager(), tbLogin.Text);

                FormsAuthentication.SetAuthCookie(tbLogin.Text, false);
                string newRedirectUrl = "http://localhost:4308/Profile/Index";
                Response.Redirect(newRedirectUrl);
            }
        }

        private void SetRoles(DbUserManager userManager, string login)
        {
            IIdentity identity = new GenericIdentity(login);
            string[] roles = userManager.GetRoles(login);
            HttpContext.Current.User = new GenericPrincipal(identity, roles);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/LoginPage.aspx");
        }

        private bool isValid()
        {           
            if ((validator.isAvailableLogin(tbLogin.Text) ?
                    CorrectFieldLight(lbCheckLogin) :
                    IncorrectFieldLight(lbCheckLogin, loginExists)) &&
                (validator.isValidLogin(tbLogin.Text) ?
                    CorrectFieldLight(lbCheckLogin) : IncorrectFieldLight(lbCheckLogin)) &
                (validator.isValidPassword(tbPassword.Text) &&
                    validator.isEqualPasswordAndConfirm(tbPassword.Text, tbConfirm.Text) ?
                    CorrectFieldLight(lbCheckPassword) & CorrectFieldLight(lbCheckConfirm) :
                    IncorrectFieldLight(lbCheckPassword) & IncorrectFieldLight(lbCheckConfirm)) &
                (validator.isValidName(tbName.Text) ?
                    CorrectFieldLight(lbCheckName) : IncorrectFieldLight(lbCheckName)) &
                (validator.isValidName(tbLastName.Text) ?
                    CorrectFieldLight(lbCheckLastName) : IncorrectFieldLight(lbCheckLastName)) &
                (validator.isValidMobile(tbMobile.Text) ?
                    CorrectFieldLight(lbCheckMobile) : IncorrectFieldLight(lbCheckMobile)) &
                (validator.isValidEmail(tbEmail.Text) ?
                    CorrectFieldLight(lbCheckEmail) : IncorrectFieldLight(lbCheckEmail)) &
                (validator.isValidBirth(tbBirthday.Text) ?
                    CorrectFieldLight(lbCheckBirthday) : IncorrectFieldLight(lbCheckBirthday)) &
                (validator.isValidCountry(tbCountry.Text) ?
                    CorrectFieldLight(lbCheckCountry) : IncorrectFieldLight(lbCheckCountry)))
            {
                return true;
            }
            return false;
        }

        private bool IncorrectFieldLight(Label lb, string text = "")
        {
            lb.ForeColor = Color.OrangeRed;
            lb.Text = xMark + text;
            return false;
        }

        private bool CorrectFieldLight(Label lb, string text = "")
        {
            lb.ForeColor = Color.Green;
            lb.Text = checkMark + text;
            return true;
        }

        protected void tbLogin_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidLogin(tbLogin.Text))
            {
                if (validator.isAvailableLogin(tbLogin.Text))
                {
                    CorrectFieldLight(lbCheckLogin);
                    return;
                }
                IncorrectFieldLight(lbCheckLogin, loginExists);
            }
            else
            {
                IncorrectFieldLight(lbCheckLogin);
            }

        }

        protected void tbPassword_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidPassword(tbPassword.Text) && 
                validator.isEqualPasswordAndConfirm(tbPassword.Text, tbConfirm.Text))
            {
                CorrectFieldLight(lbCheckPassword);
                CorrectFieldLight(lbCheckConfirm);
            }
            else
            {
                IncorrectFieldLight(lbCheckPassword);
                IncorrectFieldLight(lbCheckConfirm);
            }
        }

        protected void tbName_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidName(tbName.Text))
            {
                CorrectFieldLight(lbCheckName);
            }
            else
            {
                IncorrectFieldLight(lbCheckName);
            }            
        }

        protected void tbLastName_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidName(tbLastName.Text))
            {
                CorrectFieldLight(lbCheckLastName);
            }
            else
            {
                IncorrectFieldLight(lbCheckLastName);
            }
        }

        protected void tbMobile_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidMobile(tbMobile.Text))
            {
                CorrectFieldLight(lbCheckMobile);
            }
            else
            {
                IncorrectFieldLight(lbCheckMobile);
            }
        }

        protected void tbEmail_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidEmail(tbEmail.Text))
            {
                CorrectFieldLight(lbCheckEmail);
            }
            else
            { 
                IncorrectFieldLight(lbCheckEmail);
            }
        }

        protected void tbBirthday_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidBirth(tbBirthday.Text))
            {
                CorrectFieldLight(lbCheckBirthday);
            }
            else
            {
                IncorrectFieldLight(lbCheckBirthday);
            }
        }

        protected void tbCountry_TextChanged(object sender, EventArgs e)
        {
            if (validator.isValidCountry(tbCountry.Text))
            {
                CorrectFieldLight(lbCheckCountry);
            }
            else
            {
                IncorrectFieldLight(lbCheckCountry);
            }
        }
        
    }
}