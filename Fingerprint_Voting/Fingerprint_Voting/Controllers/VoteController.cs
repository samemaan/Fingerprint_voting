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
            ViewBag.fingerPrint = voteVM.getUserFingerprint(userId);
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


                //var Description = paramVotesDTO.Description.Trim();

                //using (sqlconn)
                //{
                //    using (SqlCommand cmd = new SqlCommand("InsertIntoUserStatusTable", sqlconn))
                //    {
                //        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                //        cmd.Parameters.AddWithValue("@Description", Description);

                //        sqlconn.Open();
                //        cmd.ExecuteNonQuery();
                //        sqlconn.Close();

                //    }
                //}

                /**
                 * Create HTTP request
                 * render content of page
                 * make sure POST data present
                 * return status 200 OK
                 * 
                 */


                //return new HttpStatusCodeResult(HttpStatusCode.OK); //Redirect("~/Vote/Create");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View();
            }
        }
        #endregion


        // GET: /Vote/ThanksYou
        [Authorize]
        #region public ActionResult Create()
        public ActionResult ThanksYou()
        {
            ViewBag.Message = "Thanks For the Vote";

            var canId = Request["CandidateId"];
            var uId = Request["UserId"];
            var finprint = Request["UserFingerprint"];

            ViewBag.candidateID = canId;
            ViewBag.userID = uId;
            ViewBag.fingerprint = finprint;


            return View();
        }
        #endregion
    }
}