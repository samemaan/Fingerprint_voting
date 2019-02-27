using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class VoteController : Controller
    {
      
        // GET: Vote
        public ActionResult Index()
        {
            
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            List<CandidateDTO> candidates = candidateVM.GetAllCandidates();

            String userId = User.Identity.GetUserId();



            ViewBag.userId = userId; 

            return View(candidates);
        }
    }
}