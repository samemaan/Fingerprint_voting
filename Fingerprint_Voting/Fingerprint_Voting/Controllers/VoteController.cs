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
            // get logged in user id
            String userId = User.Identity.GetUserId();
            // get user fingerprint 
            VoteViewModel voteVM = new VoteViewModel();
            ViewBag.fingerPrint = voteVM.GetUserFingerprint(userId);
            // passing user id and finger print to the frontend
            ViewBag.userId = userId; 

            return View(candidates);
        }


        // GET: /Vote/Scan_FingerForeVote
        [Authorize]
        #region public ActionResult Scan_FingerForeVote()
        public ActionResult Scan_FingerForeVote()
        {
            VotesDTO votesDTO = new VotesDTO();
            return View(votesDTO);


        }
        #endregion

        // POST: /Vote/Scan_FingerForeVote
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        #region public ActionResult Scan_FingerForeVote(VotesDTO paramUserStatusDTO)


        public ActionResult Scan_FingerForeVote(VotesDTO paramVotesDTO)
        {
            var canId = paramVotesDTO.CandidateId;
            var uId = paramVotesDTO.UserId;
            var finprint = Request["UserFingerprint"]; 

            ViewBag.candidateID = canId;
            ViewBag.userID = uId;
            ViewBag.fingerprint = finprint; 


            try
            {
                if (paramVotesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        #region public ActionResult Create()
        public ActionResult ThankYou()
        {
            ViewBag.Message = "Thanks For the Vote";

            var canId = Request["CandidateId"];
            var uId = Request["UserId"];
            var finprint = Request["UserFingerprint"];

            ViewBag.candidateID = canId;
            ViewBag.userID = uId;
            ViewBag.fingerprint = finprint;

            var userStatusId = "";
            var userStatusDescription = ""; 
            VoteViewModel voteVM = new VoteViewModel();
            userStatusId = voteVM.GetUserStatusId(uId);
            userStatusId = userStatusId.ToString();


            VoteViewModel voteV = new VoteViewModel();
            userStatusDescription = voteV.GetUserStatusDescription(userStatusId);
            if (userStatusDescription == "Not Vote")
            {
                ViewBag.userStatusDescription = "NOt Vote";

            }
            else if(userStatusDescription == "Age-rule")
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
        #endregion
    }
}