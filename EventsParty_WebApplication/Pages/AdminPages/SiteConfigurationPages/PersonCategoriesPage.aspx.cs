using System.Reflection;
using DataAccess.Concrete;
using EventsParty_WebApplication.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel;
using ValidationLibrary;
using DataAccess.Abstract;

namespace EventsParty_WebApplication.Pages.AdminPages
{
    public partial class PersonCategoriesPage : System.Web.UI.Page
    {
        static List<EventPersonCategory> Categories;
        DbStore store = new DbStore();
        IDataManager categoryManager = new CategoryManager();
        Validator validator = new Validator();

        private void SetCategories()
        {
            Categories = store.GetAllPersonCategories();
            gvItems.DataSource = Categories;
            gvItems.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                SetCategories();
            }
        }

        private void SetAddingMsg(string text, System.Drawing.Color color)
        {
            lbMsg.Text = text;
            lbMsg.ForeColor = color;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validator.isValidTextWithDigits(tbNewType.Text))
            {
                if (categoryManager.Add(tbNewType.Text))
                {
                    SetAddingMsg("Successfully added", System.Drawing.Color.Green);
                    SetCategories();
                }
                else
                {
                    SetAddingMsg("Category already exists", System.Drawing.Color.Red);
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
                if (categoryManager.Delete(id))
                    {
                        SetCategories();
                    }
            }
        }

        public bool EditFieldValid(string text)
        {
            if (validator.isValidText(text))
            {
                if (!categoryManager.isExist(text))
                {
                    return true;
                }
            }
            return false;
        }

        public bool EditData(int actualId, string newValue)
        {
            if (categoryManager.Edit(actualId, newValue))
            {
                return true;
            }
            return false;
        }

        protected void btnEditSave_OnClick(object sender, EventArgs e)
        {
            if (EditFieldValid(tbEditNew.Text))
            {
                int editId = Categories.FirstOrDefault(t => t.Category == hf.Value).Id;
                EditData(editId, tbEditNew.Text);
                SetCategories();
            }
        }
    }
}