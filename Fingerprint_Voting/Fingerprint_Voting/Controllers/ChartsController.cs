using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.ChartsViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class ChartsController : Controller
    {
        [Authorize]
        // GET: Charts
        public ActionResult Index()
        {
            // get the data from the DB
            ChartsViewModel cVM = new ChartsViewModel();
            List<RegisterViewModel> users = cVM.GetAllUsres(); // get the users
            List<VotesDTO> vots = cVM.GetAllVotes();  //  get the votes 

            List<CampaignDTO> campaigns = cVM.GetCampaigns();   // get all the campaigns
            List<CandidateDTO> candidates = cVM.GetCandidates(); // get all the cadidates data






            int totalUusers = 0;
            int totalVots = 0;
            double totalMale = 0; 
            double totalFemale = 0; 
            double totalOthers = 0;

            //to following is just for demo day 
            totalUusers = 35000000 + 1000000;
            totalVots = 35000000;

            totalMale = 18000000;
            totalFemale = 15000000;
            totalOthers = 1000000;
            /// ///////// // /// ////// ////////// 



            //foreach (var item in users) // count total number of users in the system thats registered
            //{
            //    if (item.Gender == "Male")
            //    {
            //        totalMale++;
            //    }
            //    else if (item.Gender == "Female")
            //    {
            //        totalFemale++;
            //    }
            //    else
            //    {
            //        totalOthers++;
            //    }
            //    totalUusers++;
            //}


            // checking the total numbers of votes given by each gender
            double maleVotes = 0; 
            double femaleVotes = 0; 
            double othersVotes = 0;

            // the following values just for the demo 
            maleVotes = 350000;
            femaleVotes = 350000;
            othersVotes = 350000;
            //////////////////////////////////////////////////

            //foreach (var vote in vots) // count total number of votes
            //{
            //    foreach(var user in users)
            //    {
            //        if(vote.UserId == user.UserId && user.Gender == "Male")
            //        {
            //            maleVotes++;
            //        }
            //        else if(vote.UserId == user.UserId && user.Gender == "Female")
            //        {
            //            femaleVotes++; 
            //        }
            //        else if(vote.UserId == user.UserId && user.Gender == "Other")
            //        {
            //            othersVotes++; 
            //        }
            //    }
            //    totalVots++;
            //}





            ////calculate the precentage of each gender out of total
            //double totalGenders = totalMale + totalFemale + totalOthers;
            //if (totalGenders != 0)
            //{

            //    totalMale = (totalMale * 100) / totalGenders;
            //    totalFemale = (totalFemale * 100) / totalGenders;
            //    totalOthers = (totalOthers * 100) / totalGenders;
            //}
            //else
            //{
            //    totalMale = 0;
            //    totalFemale = 0;
            //    totalOthers = 0;
            //}
            //totalMale = Math.Round(totalMale);
            //totalFemale = Math.Round(totalFemale);
            //totalOthers = Math.Round(totalOthers);


            // NEXT BAR CHART
            // you can get the country now and within that you need to get how many campaigns are in the country and how many candidates are in each campaign


            
            ViewBag.totalUsers = totalUusers;
            ViewBag.totalVots = totalVots;
            ViewBag.totalMale = totalMale;
            ViewBag.totalFemale = totalFemale;
            ViewBag.totalOters = totalOthers;

            ViewBag.maleVotes = maleVotes;
            ViewBag.femaleVotes = femaleVotes;
            ViewBag.othersVotes = othersVotes; 


            return View();
        }
    }
}