using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel;
using EventsParty_WebApplication.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventsParty_WebApplication.Pages.AdminPages.SiteConfigurationPages
{
    public partial class DeletedCustomersInfoPage : System.Web.UI.Page
    {
        static EventCustomer CurrentUser;
        IUserManager manager = new DbUserManager();
        DbStore store = new DbStore();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {
                var id = Convert.ToInt32(Request.QueryString["id"]);
                CurrentUser = store.GetDeletedUsers().FirstOrDefault(c => c.Id == id);
                LoadData();
            }
        }

        public void LoadData()
        {
            imgAvatar.ImageUrl = GetImage();
            lbLogin.Text = CurrentUser.Login;
            lbName.Text = CurrentUser.Name;
            lbLastName.Text = CurrentUser.LastName;
            lbRating.Text = CurrentUser.Rating.Name;
            lbMobile.Text = CurrentUser.Mobile;
            lbEmail.Text = CurrentUser.Email;
            lbBirthday.Text = CurrentUser.Birthday.ToLongDateString();
            lbCountry.Text = CurrentUser.Country;
            lbAddress.Text = CurrentUser.Address;
            try
            {
                lbOrgEventCount.Text = CurrentUser.OrganizedEvents.Count.ToString();
            }
            catch (Exception e)
            {
                lbOrgEventCount.Text = "0";
            }

            ddlPermission.DataSource = store.GetAllPermissions();
            ddlPermission.DataValueField = "Id";
            ddlPermission.DataTextField = "Type";
            ddlPermission.DataBind();
            ddlPermission.Items.FindByText(CurrentUser.Permissions.Type).Selected = true;


            if (CurrentUser.Permissions.Type == "Admin")
            {
                btnDel.Enabled = false;
                btnDel.CssClass = "disableClass";
            }
        }

        public string GetImage()
        {
            return new PageManager().GetImage(CurrentUser.Photo, Server.MapPath("~"));
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/AdminPages/DeletedCustomersPage.Aspx");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            manager.ReturnUser(CurrentUser.Id);
            Response.Redirect("/Pages/AdminPages/CustomersPage.Aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlPermission.SelectedItem.Value);
            if (CurrentUser.Permissions.Id != id)
            {
                manager.ChangePermission(CurrentUser.Id, id);
            }
        }
    }
}