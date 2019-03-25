using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.ChartsViewModels;
using Fingerprint_Voting.Models.UserStatusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers.Charts
{
    public class DynamicallyUpdateChartController : Controller
    {
        [Authorize(Roles = "Administrator, Admin, Staff, Super Admin")]
        // GET: DynamicallyUpdateChart
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, Admin, Staff, Super Admin")]
        // GET: DynamicallyUpdateChart
        public ActionResult CountryChart()
        {
            DynamicChartsModel dM = new DynamicChartsModel();
            //CandidateDTO candidateDTO = new CandidateDTO();
            CampaignViewModel camVM = new CampaignViewModel();
            // get all the campaigns in the list with thier ids 
            List<CampaignDTO> camp = new List<CampaignDTO>();
            camp = camVM.GetAllCampaigns();

            List<string> campaingCountrylist = new List<string>();  // get the campaigns name from the list campaings model and add them to string list

            foreach (var item in camp)
            {
                if (!campaingCountrylist.Contains(item.Country.ToString()))
                {
                    campaingCountrylist.Add(item.Country);
                }

            }
            campaingCountrylist.Sort();
            dM.Countries = campaingCountrylist; // string list of campaigns names, store them in the Campaigns list where declared in the model

            dM.Country = campaingCountrylist.First(); // get the first country for the Campaigns 
            var country = dM.Country;
            List<string> allCampaignsOfCountry = new List<string>();
            foreach (var cam in camp)
            {
                if (country == cam.Country)
                {
                    allCampaignsOfCountry.Add(cam.Description); // get all the campaigns related to the country 
                }
            }
            dM.Campaigns = allCampaignsOfCountry;
            dM.Campagin = allCampaignsOfCountry.First();

            CampaignDashBoardList(dM.Country, dM.Campagin); // default draw of the data 
            return View("~/Views/DynamicallyUpdateChart/CountryChart.cshtml", dM);

        }
        public JsonResult CountryDashboardList(string country) // accepts the country and returns the list of campaigns by the JSON call 
        {
            DynamicChartsModel dM = new DynamicChartsModel(); // assign country with this 
            CampaignViewModel camVM = new CampaignViewModel();
            // get all the campaigns in the list with thier ids 
            List<CampaignDTO> camp = new List<CampaignDTO>();
            camp = camVM.GetAllCampaigns();

            List<string> campaingCountrylist = new List<string>();  // get the campaigns name from the list campaings model and add them to string list

            foreach (var item in camp)
            {
                if (!campaingCountrylist.Contains(item.Country.ToString()))
                {
                    campaingCountrylist.Add(item.Country);
                }
            }

            dM.Countries = campaingCountrylist; // string list of campaigns names, store them in the Campaigns list where declared in the model
            List<string> allCampaignsOfCountry = new List<string>();
            foreach (var cam in camp)
            {
                if (country == cam.Country)
                {
                    allCampaignsOfCountry.Add(cam.Description); // get all the campaigns related to the country 
                }
            }
            dM.Campaigns = allCampaignsOfCountry;
            dM.Campagin = allCampaignsOfCountry.First();
            dM.Country = country;
            return Json(dM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CampaignDashBoardList(string country, string campaign) // accepts campaign of the country and passes the list of data by JSON call
        {

            ChartsViewModel cVM = new ChartsViewModel();
            List<string> data = new List<string>(); // asigned the returned list to this and pass it to json parameter

            try
            {
                // here you need to get the data to display it in the chart
                var response = cVM.GetAllTheDeTailsRelatedToEachCountry(country, campaign);

                if (!object.Equals(response, null))
                {
                    data = response;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}