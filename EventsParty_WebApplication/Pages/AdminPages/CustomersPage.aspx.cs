using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Concrete;
using System.IO;
using EventsParty_WebApplication.Concrete;
using DataModel;
using DataAccess.Abstract;
using Newtonsoft.Json;

namespace EventsParty_WebApplication.Pages.AdminPages
{
    public partial class UsersList : System.Web.UI.Page
    {
        static List<EventCustomer> Customers { get; set; }
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                var store = new DbStore();

                if (ddlRatings.DataSource == null)
                {
                    ddlRatings.DataSource = store.GetAllRatings();
                    ddlRatings.DataValueField = "Id";
                    ddlRatings.DataTextField = "Name";
                    ddlRatings.DataBind();
                    ddlPermissions.DataSource = store.GetAllPermissions();
                    ddlPermissions.DataValueField = "Id";
                    ddlPermissions.DataTextField = "Type";
                    ddlPermissions.DataBind();
                }

                Customers = store.GetAllUsers();
                gvUsers.DataSource = Customers;
                gvUsers.DataBind();
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {            
            var foundUsers = Customers
                .Where(c => c.Login.Contains(tbSearch.Text) || 
                    c.Name.Contains(tbSearch.Text) || 
                    c.LastName.Contains(tbSearch.Text)).ToList();
            if (cbRatingFilter.Checked)
            {
                foundUsers = foundUsers.Where(u => u.Rating.Name == ddlRatings.SelectedItem.Text).ToList();
            }
            if (cbPermissionFilter.Checked)
            {
                foundUsers = foundUsers.Where(u => u.Permissions.Type == ddlPermissions.SelectedItem.Text).ToList();
            }
            gvUsers.DataSource = foundUsers;
            gvUsers.DataBind();
            lbSearchInfo.Text = "found " + foundUsers.Count + " items";
        }

        public string GetImage(object img)
        {
            return new PageManager().GetImage(img, Server.MapPath("~"));
        }
    }
}