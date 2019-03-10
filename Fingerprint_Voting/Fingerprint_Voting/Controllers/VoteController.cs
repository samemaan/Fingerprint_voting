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
                    //var uId = paramVotesDTO.UserId;
                    //var finprint = Request["UserFingerprint"]; 

                    ViewBag.candidateID = canId;
                    //ViewBag.userID = uId;
                    //ViewBag.fingerprint = finprint; 

                    VoteViewModel vvM = new VoteViewModel();

                    candidate = vvM.GetCandidateDetailsById(canId);

                    //Get the candidate details
                    ViewBag.Name = candidate.Name; 
                    ViewBag.Surname = candidate.Surname;
                    ViewBag.Gender = candidate.Gender;
                    ViewBag.Picture = candidate.CandidatePic; 

                    
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
            //var uId = Request["UserId"];
            // get logged in user id
            String uId = User.Identity.GetUserId();

            var finprint = Request["UserFingerprint"];

            ViewBag.candidateID = canId;
            ViewBag.userID = uId;
            ViewBag.fingerprint = finprint;

            var userStatusId = "";
            var userStatusDescription = ""; 
            VoteViewModel voteVM = new VoteViewModel();
            userStatusId = voteVM.GetUserStatusId(uId);
            userStatusId = userStatusId.ToString();
            // check if the candidate country is same as the voters country 
            var CandidateCountry = "";
            var UserCountry = ""; 

            CandidatesViewModel candidatesViewModel = new CandidatesViewModel();
            CandidateDTO canDTO = new CandidateDTO();
            canDTO = candidatesViewModel.GetCandidateDetailsById(canId);
            // candidates Country
            CandidateCountry = canDTO.Country;
            // users Country
            VoteViewModel V = new VoteViewModel();
            UserCountry = V.GetLoggedUserCountry(uId); 

            VoteViewModel voteV = new VoteViewModel();
            userStatusDescription = voteV.GetUserStatusDescription(userStatusId);

            ViewBag.UserCountry = UserCountry; 
            ViewBag.CandidateCountry = CandidateCountry; 

            if (UserCountry == CandidateCountry)
            {
                if (userStatusDescription == "Not Vote")
                {
                    ViewBag.userStatusDescription = "NOt Vote";


                }
                else if (userStatusDescription == "Age-rule")
                {
                    ViewBag.userStatusDescription = "You are Too Young For Voting";
                }
                else
                {
                    //ViewBag.userStatusId = userStatusId;
                    //ViewBag.userStatusDescription = userStatusDescription;
                    ViewBag.userStatusDescription = "you have already Voted";
                }

                return View();

            }
            else
            {
                return View("Error");
                
            }
           
        }
        #endregion
    }
}