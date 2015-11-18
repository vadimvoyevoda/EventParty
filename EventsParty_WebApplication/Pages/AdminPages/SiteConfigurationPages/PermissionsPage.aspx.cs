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
    public partial class PermissionsPage : System.Web.UI.Page
    {
        static List<EventPermission> Permissions;
        DbStore store = new DbStore();
        IDataManager permissionManager = new PermissionManager();
        Validator validator = new Validator();

        private void SetPermissions()
        {
            Permissions = store.GetAllPermissions();
            gvItems.DataSource = Permissions;
            gvItems.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                SetPermissions();
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
                if (permissionManager.Add(tbNewType.Text))
                {
                    SetAddingMsg("Successfully added", System.Drawing.Color.Green);
                    SetPermissions();
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
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (permissionManager.Delete(id))
                    {
                        SetPermissions();
                    }
            }
        }

        public bool EditFieldValid(string text)
        {
            if (validator.isValidText(text))
            {
                if (!permissionManager.isExist(text))
                {
                    return true;
                }
            }
            return false;
        }

        public bool EditData(int actualId, string newValue)
        {
            if (permissionManager.Edit(actualId, newValue))
            {
                return true;
            }
            return false;
        }

        protected void btnEditSave_OnClick(object sender, EventArgs e)
        {
            if (EditFieldValid(tbEditNew.Text))
            {
                int editId = Permissions.FirstOrDefault(t => t.Type == hf.Value).Id;
                EditData(editId, tbEditNew.Text);
                SetPermissions();
            }
        }
    }
}