using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.ChartsViewModels;
using Fingerprint_Voting.Models.ResultsViewModels;
using Fingerprint_Voting.Models.UserStatusModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        public ActionResult Index()
        {
            return View();
        }
        // GET: Results/ResultsChart
        public ActionResult ResultsChart()
        {
            String uId = User.Identity.GetUserId(); // get logged in users Id 

            ResultsViewModel rVM = new ResultsViewModel(); // get all the user roles 


            ResultsDTO results = new ResultsDTO(); //


            List<ResultsDTO> resultsDTO = rVM.GetUsersRole();

            bool found = false; 
            foreach(var result in resultsDTO)
            {
                if(uId != result.UserId)
                {
                    found = true;
                }
            }
            CampaignViewModel camVM = new CampaignViewModel(); // get all the campaign
            List<CampaignDTO> campaign = new List<CampaignDTO>();
            campaign = camVM.GetAllCampaigns();

            ChartsViewModel cVM = new ChartsViewModel();
            RegisterViewModel userDetails = cVM.GetUserDetailsById(uId); // get signed in users details 

            List<string> usersCountryCampainglist = new List<string>();  // get the campaigns name from the list campaings model and add them to string list

            if (found == true) // if the user exist 
            {
                foreach(var cam in campaign)
                {
                    if(userDetails.Country == cam.Country)
                    {
                        if (!usersCountryCampainglist.Contains(cam.Description.ToString()))
                        {
                            usersCountryCampainglist.Add(cam.Description);
                        }
                    }
                }
            }
            //bool isEmpty = !results.Any();
            bool isEmpty = !usersCountryCampainglist.Any();

            if (!isEmpty) // check if the list is not null than add the values to the object , else redirect the user that there are no campaigns in the country 
            {
                results.Campagin = usersCountryCampainglist.First();
                results.Campaigns = usersCountryCampainglist;
                ViewBag.Campaign = usersCountryCampainglist.First(); // get the first country for the Campaigns 
                var collectedCampaign = usersCountryCampainglist.First(); // get the first country for the Campaigns 
                CampaignDashBoardList(collectedCampaign); // default draw of the data 
                return View("~/Views/Results/ResultsChart.cshtml", results); // return with the values to view.
            }
            else
            {
                return View("~/Views/Results/NotFound.cshtml"); // no ca
            }
           
            

        }
        public JsonResult CampaignDashBoardList(string campaign) // accepts campaign of the country and passes the list of data by JSON call
        {

            
            List<string> data = new List<string>(); // asigned the returned list to this and pass it to json parameter
            ChartsViewModel cVM = new ChartsViewModel();
            String uId = User.Identity.GetUserId(); // get logged in users Id 
            RegisterViewModel userDetails = cVM.GetUserDetailsById(uId); // get signed in users details 

            var country = userDetails.Country;
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