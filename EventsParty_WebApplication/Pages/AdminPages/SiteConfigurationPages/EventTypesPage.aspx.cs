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
using ValidationLibrary;

namespace EventsParty_WebApplication.Pages.AdminPages
{
    public partial class EventTypesPage : System.Web.UI.Page
    {
        static List<EventType> Types;
        DbStore store = new DbStore();
        IDataManager typeManager = new TypeManager();
        Validator validator = new Validator();

        private void SetTypes()
        {
            Types = store.GetAllEventTypes();
            gvItems.DataSource = Types;
            gvItems.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                SetTypes();
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
                if (typeManager.Add(tbNewType.Text))
                {
                    SetAddingMsg("Successfully added", System.Drawing.Color.Green);
                    SetTypes();
                }
                else
                {
                    SetAddingMsg("Permission already exists", System.Drawing.Color.Red);
                }
            }
            else
            {
                SetAddingMsg("Incorrect type", System.Drawing.Color.Red);
            }
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "DeleteRow":
                    int id = Convert.ToInt32(e.CommandArgument);
                    if (typeManager.Delete(id))
                    {
                        SetTypes();
                    }
                    break;
                case "ShowColors":
                    iColorPicker.Attributes["class"] = "show";
                    hideCP.Attributes["class"] = "show";
                    upColor.Update();
                    break;
                case "ChooseColor":
                    string color = iColorPicker.Value;
                    iColorPicker.Attributes["class"] = "hidden";
                    hideCP.Attributes["class"] = "hidden";
                    upColor.Update();
                    int Id = Convert.ToInt32(e.CommandArgument);
                    if ((typeManager as TypeManager).SetColor(Id, color))
                    {
                        SetTypes();
                        upGrid.Update();
                    }                    
                    break;
            } 
        }

        public bool EditFieldValid(string text)
        {
            if (validator.isValidText(text))
            {
                if (!typeManager.isExist(text))
                {
                    return true;
                }
            }
            return false;
        }

        public bool EditData(int actualId, string newValue)
        {
            if (typeManager.Edit(actualId, newValue))
            {
                return true;
            }
            return false;
        }

        protected void btnEditSave_OnClick(object sender, EventArgs e)
        {
            if (EditFieldValid(tbEditNew.Text))
            {
                int editId = Types.FirstOrDefault(t=>t.Type == hf.Value).Id;
                EditData(editId, tbEditNew.Text);
                SetTypes();
            }
        }

        protected void hideCP_Click(object sender, EventArgs e)
        {
            iColorPicker.Attributes["class"] = "hidden";
            hideCP.Attributes["class"] = "hidden";
            upColor.Update();
        }

    }
}