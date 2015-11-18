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

namespace EventsParty_WebApplication.Pages.AdminPages.SiteConfigurationPages
{
    public partial class CitiesPage : System.Web.UI.Page
    {
        static List<EventCountry> Countries;
        static List<EventRegion> Regions;
        static List<EventCity> Cities;
        DbStore store = new DbStore();
        IDataManager countryManager = new CountryManager();
        IDataManager regionManager = new RegionManager();
        IDataManager cityManager = new CityManager();
        Validator validator = new Validator();

        private void SetCountries()
        {
            ddlCountries.DataSource = Countries.OrderBy(c => c.Country);
            ddlCountries.DataValueField = "Id";
            ddlCountries.DataTextField = "Country";
            ddlCountries.DataBind();
        }

        private void SetRegions()
        {
            if (Countries.Count > 0)
            {
                if (ddlCountries.SelectedItem != null)
                {
                    var country = Countries.FirstOrDefault(c => c.Country == ddlCountries.SelectedItem.Text);
                    if (country != default(EventCountry))
                    {
                        if (country.Regions.Count == 0)
                        {
                            ddlRegions.DataSource = new List<EventRegion>();
                        }
                        else
                        {
                            ddlRegions.DataSource = country.Regions.OrderBy(r => r.RegionName);
                            ddlRegions.DataValueField = "Id";
                            ddlRegions.DataTextField = "RegionName";
                        }
                    }
                    ddlRegions.DataBind();
                }
            }
        }

        private void SetCities()
        {
            bool isSet = false;
            if (Regions.Count > 0)
            {
                if (ddlRegions.SelectedItem != null)
                {
                    var region = Regions.FirstOrDefault(r => r.RegionName == ddlRegions.SelectedItem.Text);
                    if (region != default(EventRegion))
                    {
                        if (region.Cities.Count > 0)
                        {
                            gvCities.DataSource = region.Cities.OrderBy(c => c.CityName);
                            isSet = true;
                        }
                    }
                }
            }
            if(!isSet)
            {
                gvCities.DataSource = new List<EventCity>();
            }
            gvCities.DataBind();
        }

        private void SetData()
        {
            Countries = store.GetAllCountries();
            Regions = store.GetAllRegions();
            Cities = store.GetAllCities(); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {
                SetData();

                SetCountries();
                SetRegions();
                SetCities();
            }
        }

        private void SetAddingMsg(string text, System.Drawing.Color color, Label lbMsg)
        {
            lbMsg.Text = text;
            lbMsg.ForeColor = color;
        }

        protected void btnSaveCountry_Click(object sender, EventArgs e)
        {
            if (validator.isValidText(tbNewCountry.Text))
            {
                if (!countryManager.isExist(tbNewCountry.Text))
                {
                    if (countryManager.Add(tbNewCountry.Text))
                    {
                        SetAddingMsg("Success", System.Drawing.Color.Green, lbAddCountryMsg);
                        SetData();
                        SetCountries();
                        SetRegions();
                        SetCities();
                    }
                    else
                    {
                        SetAddingMsg("DbError", System.Drawing.Color.Red, lbAddCountryMsg);
                    }
                }
                else
                {
                    SetAddingMsg("Exists", System.Drawing.Color.Red, lbAddCountryMsg);
                }
            }
            else
            {
                SetAddingMsg("Error", System.Drawing.Color.Red, lbAddCountryMsg);
            }
        }

        protected void btnSaveRegion_Click(object sender, EventArgs e)
        {
            if (validator.isValidText(tbNewRegion.Text))
            {
                if (!countryManager.isExist(tbNewRegion.Text))
                {
                    if (ddlCountries.SelectedItem != null)                   
                    {
                        if (regionManager.Add(tbNewRegion.Text, Convert.ToInt32(ddlCountries.SelectedItem.Value)))
                        {
                            SetAddingMsg("Success", System.Drawing.Color.Green, lbAddRegionMsg);
                            SetData();
                            SetRegions();
                            SetCities();
                        }
                        else
                        {
                            SetAddingMsg("DbError", System.Drawing.Color.Red, lbAddRegionMsg);
                        }
                    }
                    else
                    {
                        SetAddingMsg("Not Select", System.Drawing.Color.Red, lbAddRegionMsg);
                    }
                }
                else
                {
                    SetAddingMsg("Exists", System.Drawing.Color.Red, lbAddRegionMsg);
                }
            }
            else
            {
                SetAddingMsg("Error", System.Drawing.Color.Red, lbAddRegionMsg);
            }
        }

        protected void btnDeleteCountry_Click(object sender, EventArgs e)
        {
            if (ddlCountries.SelectedItem != null)
                if (countryManager.Delete(Convert.ToInt32(ddlCountries.SelectedItem.Value)))
                {
                    SetData();
                    SetCountries();
                    SetRegions();
                    SetCities();
                }
        }

        protected void btnDeleteRegion_Click(object sender, EventArgs e)
        {
            if (ddlRegions.SelectedItem.Text != null)
                if (regionManager.Delete(Convert.ToInt32(ddlRegions.SelectedItem.Value)))
                {
                    SetData();
                    SetRegions();
                    SetCities();
                }
        }

        protected void btnSaveCity_Click(object sender, EventArgs e)
        {
            if (validator.isValidText(tbNewCity.Text))
            {
                if (ddlCountries.SelectedItem != null && ddlRegions.SelectedItem!=null)
                {
                    if (!cityManager.isExist(tbNewCity.Text, Convert.ToInt32(ddlRegions.SelectedItem.Value)))
                    {
                        if (cityManager.Add(tbNewCity.Text, Convert.ToInt32(ddlRegions.SelectedItem.Value)))
                        {
                            SetAddingMsg("Success", System.Drawing.Color.Green, lbAddCityMsg);
                            SetData();
                            SetCities();
                        }
                        else
                        {
                            SetAddingMsg("DbError", System.Drawing.Color.Red, lbAddCityMsg);
                        }
                    }
                    else
                    {
                        SetAddingMsg("Exists", System.Drawing.Color.Red, lbAddCityMsg);
                    }
                }
                else
                {
                    SetAddingMsg("Not Select", System.Drawing.Color.Red, lbAddCityMsg);
                }
            }
            else
            {
                SetAddingMsg("Error", System.Drawing.Color.Red, lbAddCityMsg);
            }
        }

        protected void btnEditCountry_Click(object sender, EventArgs e)
        {
            if (ddlCountries.SelectedItem!=null)
            {
                if (validator.isValidText(tbEditC.Text))
                {
                    if (!countryManager.isExist(tbEditC.Text))
                    {
                        if (countryManager.Edit(Convert.ToInt32(ddlCountries.SelectedItem.Value), tbEditC.Text))
                        {
                            SetData();
                            SetCountries();
                            SetRegions();
                            SetCities();
                            SetAddingMsg("Success", System.Drawing.Color.Green, lbEditCMsg);
                        }
                        else
                        {
                            SetAddingMsg("Error", System.Drawing.Color.Red, lbEditCMsg);
                        }
                    }
                    else
                    {
                        SetAddingMsg("Exists", System.Drawing.Color.Red, lbEditCMsg);
                    }
                }
                else
                {
                    SetAddingMsg("Incorrect", System.Drawing.Color.Red, lbEditCMsg);
                }
            }
            else
            {
                SetAddingMsg("Not selected", System.Drawing.Color.Red, lbEditCMsg);
            }
        }

        protected void btnEditRegion_Click(object sender, EventArgs e)
        {
            if (ddlRegions.SelectedItem != null)
            {
                if (validator.isValidText(tbEditR.Text))
                {
                    if (!regionManager.isExist(tbEditR.Text, Convert.ToInt32(ddlCountries.SelectedItem.Value)))
                    {
                        if (regionManager.Edit(Convert.ToInt32(ddlRegions.SelectedItem.Value), tbEditR.Text))
                        {
                            SetData();
                            SetRegions();
                            SetCities();
                            SetAddingMsg("Success", System.Drawing.Color.Green, lbEditRMsg);
                        }
                        else
                        {
                            SetAddingMsg("Error", System.Drawing.Color.Red, lbEditRMsg);
                        }
                    }
                    else
                    {
                        SetAddingMsg("Exists", System.Drawing.Color.Red, lbEditRMsg);
                    }
                }
                else
                {
                    SetAddingMsg("Incorrect", System.Drawing.Color.Red, lbEditRMsg);
                }
            }
            else
            {
                SetAddingMsg("Not selected", System.Drawing.Color.Red, lbEditRMsg);
            }
        }

        protected void gvCities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                cityManager.Delete(Convert.ToInt32(id));
            }
        }

        protected void ddlRegions_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetCities();
        }

        protected void ddlCountries_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetRegions();
            SetCities();
        }
        
        protected void btnEditSave_OnClick(object sender, EventArgs e)
        {
            if (validator.isValidText(tbEditNew.Text))
            {
                if (!cityManager.isExist(tbEditNew.Text, Convert.ToInt32(ddlRegions.SelectedItem.Value)))
                {
                    int editId = Cities.FirstOrDefault(t => t.CityName == hf.Value).Id;
                    if (cityManager.Edit(editId, tbEditNew.Text))
                    {
                        SetData();
                        SetCities();
                    }
                }
            }
        }
    }
}