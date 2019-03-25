using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.ChartsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class GeochartController : Controller
    {
        [Authorize(Roles = "Administrator, Admin, Staff, Super Admin")]
        // GET: Geochart  
        public ActionResult Index()
        {
            CountryModel objProductModel = new CountryModel
            {
                CountrytData = new Country()
            };
            objProductModel.CountrytData = GetChartData();
            objProductModel.CountryTitle = "Country";
            objProductModel.RegisterdTitle = "Registerd";
            objProductModel.VoteTitle = "Votes";

            return View(objProductModel);
        }

        public Country GetChartData()
        {
            Country objproduct = new Country();
            /*Get the data from databse and prepare the chart record data in string form.*/
            ChartsViewModel cVM = new ChartsViewModel();
            List<RegisterViewModel> users = cVM.GetAllUsres(); // get the users
            List<CampaignDTO> campaigns = cVM.GetCampaigns();   // get all the campaigns
            List<VotesDTO> votes = cVM.GetAllVotes();  //  get the votes 
            
            objproduct.CountryName = "";
            foreach (var country in users) // get list of countries from registered users, if there are no campaigns still i want to see users region on GEO MAP
            {

                foreach (var item in users)
                {
                    if (country.Country == item.Country)
                    {
                        if (!objproduct.CountryName.Contains(country.Country.ToString()))
                        {
                            objproduct.CountryName += country.Country + ",";
                        }
                    }
                }
            }

            foreach (var country in campaigns) // get list of the countries from the campaigns 
            {

                foreach (var item in campaigns)
                {
                    if (country.Country == item.Country)
                    {
                        if (!objproduct.CountryName.Contains(country.Country.ToString()))
                        {
                            objproduct.CountryName += country.Country + ",";
                        }
                    }
                }
            }

            // remove the last comma from the string 
            var countries = objproduct.CountryName.TrimEnd(',', ' ');
            ViewBag.Test = countries;



            // get the population of each of the country thats registered in the system. 
            // get the total votes are give in the system from that country
            var countryarray = countries.Split(',');

            objproduct.Registerd = ""; 
            int countUsers = 0;
            int countVote = 0;
            int counCampaign = 0; 
            bool found = false;
            bool voteFound = false;
            bool camFound = false; 

            foreach (var country in countryarray)
            {
                foreach (var user in users)
                {


                    if (country.ToString() == user.Country.ToString()) // match the country related to the users and count the populations 
                    {
                        countUsers++;
                        found = true;
                        foreach (var vote in votes)
                        {
                            if(user.UserId == vote.UserId) // get the users vote in each country and county the total of the vote for particular country
                            {
                                countVote++;
                                voteFound = true; 
                            }
                        }
                        //foreach(var cam in campaigns)
                        //{
                        //    if(country == cam.Country)
                        //    {
                        //        counCampaign++;
                        //        camFound = true;
                        //    }
                        //}
                    }
                }
                if (found == true && voteFound == true/* & camFound == true*/) // if it's true than add the values to the string 
                {
                    
                    objproduct.Registerd += countUsers + ",";
                    objproduct.Vote += countVote + ",";
                    countUsers = 0;
                    countVote = 0; 
                }
                
            }
            //var countries = objproduct.CountryName.TrimEnd(',', ' ');
            var Registerd = objproduct.Registerd.TrimEnd(',', ' '); // remove the comman or space at the end of the line
            var Vote = objproduct.Vote.TrimEnd(',', ' ');


            //Test them at the View. 
            ViewBag.Test1 = Registerd;
            ViewBag.Test2 = Vote;



            // the following are just the value that will show system features on demo day!!
            objproduct.CountryName = "Afghanistan,Ireland,Pakistan,Netherlands,India";
            objproduct.Registerd = "11000000,1500000,1900000,2000000,1200000,1200000";
            objproduct.Vote = "1800000,1000000,1500000,1700000,1700000,1000000";

            return objproduct;
        }
    }
}