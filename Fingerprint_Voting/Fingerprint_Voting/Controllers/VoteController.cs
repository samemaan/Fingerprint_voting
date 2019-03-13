using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO.VoteViewModels;
using Fingerprint_Voting.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class VoteController : Controller
    {
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        [Authorize]
        // GET: Vote
        public ActionResult Index()
        {

            CandidatesViewModel candidateVM = new CandidatesViewModel();
            List<CandidateDTO> candidates = candidateVM.GetAllCandidates();


            return View(candidates);
        }


        // POST: /Vote/Scan_FingerForeVote
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        #region public ActionResult Scan_FingerForeVote(VotesDTO paramUserStatusDTO)


        public ActionResult Scan_FingerForeVote(VotesDTO paramVotesDTO)
        {
            Candidate candidate = null;

            try
            {
                if (paramVotesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    // get logged in user id
                    String userId = User.Identity.GetUserId();
                    // get user fingerprint 
                    VoteViewModel voteVM = new VoteViewModel();
                    ViewBag.fingerprint = voteVM.GetUserFingerprint(userId);
                    // passing user id and finger print to the frontend
                    ViewBag.userId = userId;

                    var canId = paramVotesDTO.CandidateId;

                    ViewBag.candidateID = canId;
                    VoteViewModel vvM = new VoteViewModel();

                    candidate = vvM.GetCandidateDetailsById(canId);

                    //Get the candidate details
                    ViewBag.Name = candidate.Name;
                    ViewBag.Surname = candidate.Surname;
                    ViewBag.Gender = candidate.Gender;
                    ViewBag.Picture = candidate.CandidatePic;
                    ViewBag.CampaignID = candidate.CampaignID;


                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View();
            }



        }
        #endregion

        // GET: /Vote/ThankYou
        [Authorize]
        #region public ActionResult ThankYou()
        public ActionResult ThankYou()
        {
            ViewBag.Message = "Thanks For the Vote";
            var canId = Request["CandidateId"];
            // get logged in user id
            String uId = User.Identity.GetUserId();
            //var finprint = Request["UserFingerprint"];
            ViewBag.candidateID = canId;
            ViewBag.userID = uId;
            //ViewBag.fingerprint = finprint;
            // get user status for Voting 
            var userStatusId = "";
            var userDOB = "";
            var userStatusDescription = "";
            // get user DOB and Status in this array
            VoteViewModel voteVM = new VoteViewModel();
            UserDetails userDetails = voteVM.GetUserDetails(uId);
            userStatusId = userDetails.UserStatusId;
            userStatusId = userStatusId.ToString();
            // use DOB 
            userDOB = userDetails.DOB;

            // check if the candidate country is same as the voters country 
            var CandidateCountry = "";
            var UserCountry = "";

            CandidatesViewModel candidatesViewModel = new CandidatesViewModel();
            CandidateDTO canDTO = new CandidateDTO();
            canDTO = candidatesViewModel.GetCandidateDetailsById(canId);
            // candidates Country
            CandidateCountry = canDTO.Country;
            var candidateCampaign = canDTO.CampaignID;
            ViewBag.candidateCampaign = candidateCampaign;

            // users Country
            UserCountry = voteVM.GetLoggedUserCountry(uId);

            userStatusDescription = voteVM.GetUserStatusDescription(userStatusId);

            ViewBag.UserCountry = UserCountry;
            ViewBag.CandidateCountry = CandidateCountry;

            // get user and campaign 
            //UserCampaign userCampaign = new UserCampaign();

            //userCampaign = voteVM.GetUserCampaign(uId, candidateCampaign);

            List<UserCampaign> userCampaign = voteVM.GetUserCampaign();// get all the list of the user id and campaing that voted before
            



            if (UserCountry == CandidateCountry) //  the user should be the citizen in the country to vote for the candidate
            {
                // check users age at the current time, the user may have grown up and have the age rule 

                GetAgeCalculated gAge = new GetAgeCalculated();
                int userAge = gAge.GetAge(userDOB);
                if (userAge >= 18)
                {
                    ViewBag.result = userAge;

                    if (userCampaign != null)// check if the user campaing table is not empty 
                    {
                        bool userVotedfound = false;
                        foreach(var item in userCampaign)
                        {
                            if (uId == item.UserId && candidateCampaign == item.CampaignID)
                            {
                                userVotedfound = true;
                            }
                            else
                            {
                                userVotedfound = false;
                            }
                        }
                        //  than you can vote, check if the user and campaign are not the same 
                        if (userVotedfound) // user can vote for multiple campaings but can vote for the second time in the same campaign
                        {
                            //ViewBag.result = "The user already Voted in this campaign";
                            return View("AlreadyVoted");
                        }
                        else // if the user did not vote than let the user vote 
                        {
                            voteVM.InsertDataIntoVoteTable(uId, candidateCampaign, canId); // add the data to Vote table 
                            return View();// return thank you to the user
                        }
                    }
                    else// if the table has no data user did not vote on any campaign than let the user vote 
                    {
                        voteVM.InsertDataIntoVoteTable(uId, candidateCampaign, canId); // add the data to Vote table
                        return View();// return thank you to the user
                    }
                }
                else
                {
                    return View("Rejected");
                }
            }
            else
            {
                return View("Rejected");

            }
        }
        #endregion
    }
}