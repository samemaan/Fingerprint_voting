using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO.VoteViewModels;
using Fingerprint_Voting.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class VoteController : Controller
    {
      
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
            UserCampaign userCampaign = new UserCampaign();
            userCampaign = voteVM.GetUserCampaign(uId, candidateCampaign); 


            if(userCampaign == null)// check if the user campaing table is not empty 
            {
                ViewBag.result = "The campaing and user is not in the table"; 
                return View();

            }
            else // if the table has data 
            {
                if (UserCountry == CandidateCountry) //  the user should be the citizen in the country to vote for the candidate
                {
                    //  than you can vote, check if the user and campaign are not the same 
                    if (uId == userCampaign.UserId && candidateCampaign == userCampaign.CampaignID) // user can vote for multiple campaings but can vote for the second time in the same campaign
                    {
                        //ViewBag.result = "The user already Voted in this campaign";
                        return View("AlreadyVoted");
                    }
                    else // if the user did not vote than let him vote 
                    {
                        // check users age at the current time, the user may have grown up and have the age rule 

                        GetAgeCalculated gAge = new GetAgeCalculated();
                        int userAge = gAge.GetAge(userDOB);
                        if(userAge >= 18 )
                        {
                            ViewBag.result = userAge; 
                            return View();
                        }
                        else
                        {
                            return View("Rejected");
                        }
                        
                    }

                    //if (userStatusDescription == "Not Vote")
                    //{
                    //    ViewBag.userStatusDescription = "NOt Vote";
                    //}
                    //else if (userStatusDescription == "Age-rule")
                    //{
                    //    ViewBag.userStatusDescription = "You are Too Young For Voting";
                    //}
                    //else
                    //{
                    //    //ViewBag.userStatusId = userStatusId;
                    //    //ViewBag.userStatusDescription = userStatusDescription;
                    //    ViewBag.userStatusDescription = "you have already Voted";
                    //}

                    //return View();

                }
                else
                {
                    return View("Rejected");

                }

                

            }

        }
        #endregion
    }
}