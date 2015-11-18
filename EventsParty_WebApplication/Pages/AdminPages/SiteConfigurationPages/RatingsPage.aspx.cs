using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel;
using EventsParty_WebApplication.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ValidationLibrary;

namespace EventsParty_WebApplication.Pages.AdminPages
{
    public partial class RatingsPage : System.Web.UI.Page
    {
        static List<EventRating> Ratings;
        DbStore store = new DbStore();
        IDataManager ratingManager = new RatingManager();
        Validator validator = new Validator();

        private void SetRatings()
        {
            Ratings = store.GetAllRatings();
            gvItems.DataSource = Ratings;
            gvItems.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                SetRatings();
            }
        }

        private void SetAddingMsg(string text, System.Drawing.Color color)
        {
            lbMsg.Text = text;
            lbMsg.ForeColor = color;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validator.isValidText(tbNewType.Text))
            {
                if (ratingManager.Add(tbNewType.Text))
                {
                    SetAddingMsg("Successfully added", System.Drawing.Color.Green);
                    SetRatings();
                }
                else
                {
                    SetAddingMsg("Rating already exists", System.Drawing.Color.Red);
                }
            }
            else
            {
                SetAddingMsg("Incorrect type", System.Drawing.Color.Red);
            }
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (ratingManager.Delete(id))
                {
                    SetRatings();
                }
            }
        }

        public bool EditFieldValid(string text)
        {
            if (validator.isValidText(text))
            {
                if (!ratingManager.isExist(text))
                {
                    return true;
                }
            }
            return false;
        }

        public bool EditData(int actualId, string newValue)
        {
            if (ratingManager.Edit(actualId, newValue))
            {
                return true;
            }
            return false;
        }

        protected void btnEditSave_OnClick(object sender, EventArgs e)
        {
            if (EditFieldValid(tbEditNew.Text))
            {
                int editId = Ratings.FirstOrDefault(t => t.Name == hf.Value).Id;
                EditData(editId, tbEditNew.Text);
                SetRatings();
            }
        }
    }
}