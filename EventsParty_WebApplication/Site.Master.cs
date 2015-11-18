using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventsParty_WebApplication
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCustomersList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/CustomersPage.aspx");
        }

        protected void btnPermission_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/SiteConfigurationPages/PermissionsPage.aspx");
        }

        protected void btnRating_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/SiteConfigurationPages/RatingsPage.aspx");
        }

        protected void btnCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/SiteConfigurationPages/PersonCategoriesPage.aspx");
        }

        protected void btnType_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/SiteConfigurationPages/EventTypesPage.aspx");
        }

        protected void btnCity_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/SiteConfigurationPages/CitiesPage.aspx");
        }

        protected void btnBanList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/BanCustomersPage.aspx");
        }

        protected void btnDeleted_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AdminPages/DeletedCustomersPage.aspx");
        }

        protected void btnLogOff_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Pages/LoginPage.aspx");
        }
    }
}